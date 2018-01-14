using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.context.api;


namespace game
{
	public class Shape : View {
		public GameObject go;

		public Vector3 GOcenter;
		public Vector3 touchPosition;
		public Vector3 offset;
		public Vector3 newGOcenter;

		[Inject(ContextKeys.CONTEXT_DISPATCHER)]
		public IEventDispatcher dispatcher{ get; set;}

		RaycastHit2D hit;
		public bool dragging = false;

		internal void Create(Vector3 startPoint, Transform parent) {
			var shapes = Resources.LoadAll ("");
			var s = shapes [UnityEngine.Random.Range (0, shapes.Length)];
			var rotate = Quaternion.Euler (new Vector3 (0, 0, UnityEngine.Random.Range (0, 3) * 90));
			go = (GameObject)Instantiate( s ,startPoint, rotate, parent);
		}

		void TouchDetector(TouchPhase touch, Vector3 position){
			switch (touch) {
			case TouchPhase.Began:				
				#if UNITY_EDITOR
				var ray = Camera.main.ScreenPointToRay(position);
				var touches = Physics2D.RaycastAll(ray.origin,ray.direction);
				#else
				var ray = Camera.main.ScreenPointToRay (position);
				var touches = Physics2D.CircleCastAll (ray.origin, 0.3f, ray.direction);
				#endif
				if (touches.Length > 0) {
					hit = touches[0];
					go = hit.collider.gameObject;
					GOcenter = go.transform.position;
					touchPosition = Camera.main.ScreenToWorldPoint (position);
					offset = touchPosition - GOcenter;
					dragging = true;
					go.transform.localScale = new Vector3(1,1);
				}
				break;
			case TouchPhase.Moved:
				if (dragging) {
					touchPosition = Camera.main.ScreenToWorldPoint (position);
					newGOcenter = touchPosition - offset;
					go.transform.position = new Vector3 (newGOcenter.x, newGOcenter.y, GOcenter.z);
				}
				break;
			case TouchPhase.Ended:
				if (dragging) {
					dispatcher.Dispatch ("DROP", go);
					go.transform.localScale = new Vector3(0.8f,0.8f);
				}
				dragging = false;
				break;
			}
		}

		void Update(){
			#if UNITY_EDITOR
			if (Input.GetMouseButtonDown(0)) TouchDetector(TouchPhase.Began,Input.mousePosition);
			if (Input.GetMouseButton(0)) TouchDetector(TouchPhase.Moved, Input.mousePosition);
			if (Input.GetMouseButtonUp(0)) 
				TouchDetector(TouchPhase.Ended,Input.mousePosition);
			#else
			foreach (var touch in Input.touches) TouchDetector(touch.phase,touch.position);
			#endif
		}
	}
}