using System;
using UnityEngine;
using System.Linq;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.context.api;

namespace game
{

	public class TetrisModel: ITetrisModel
	{
		[Inject(ContextKeys.CONTEXT_DISPATCHER)]
		public IEventDispatcher dispatcher{ get; set;}

		public const int w = 10;
		public const int h = 10;
		public Transform[,] grid { get; set;}

		public Vector3 FirstCellCenter { get; set;}
		public Transform ShapePosition1 { get; set;}
		public Transform ShapePosition2 { get; set;}
		public Transform ShapePosition3 { get; set;}

		public TetrisModel () {
			grid = new Transform[10, 10];
			ShapePosition1 = GameObject.Find ("Shape1").transform;
			ShapePosition2 = GameObject.Find ("Shape2").transform;
			ShapePosition3 = GameObject.Find ("Shape3").transform;
		}

		public Vector2 roundVec2(Vector2 v) {
			return new Vector2(Mathf.Round(v.x),
				Mathf.Round(v.y));
		}

		public bool insideBorder(Vector2 pos) {
			return ((int)pos.x >= 0 &&
				(int)pos.x < w &&
				(int)pos.y >= 0 &&
				(int)pos.y <h);
		}

		public void deleteRow(int y) {
			for (int x = 0; x < w; ++x) {
				GameObject.Destroy(grid[x, y].gameObject);
				grid[x, y] = null;
			}
		}

		public bool isRowFull(int y) {
			for (int x = 0; x < w; ++x)
			{
				if (grid[x, y] == null)
					return false;
			}
			return true;
		}
		public void deleteFullRows() {
			for (int y = 0; y < h; ++y) 
			{
				if (isRowFull(y)) 
				{
					deleteRow(y);
				}
			}
		}

		public bool canBePlaced { get {
				return (ShapePosition1.childCount > 0 && figureCanBePlaced(ShapePosition1.GetChild(0))
					||ShapePosition2.childCount > 0  && figureCanBePlaced(ShapePosition2.GetChild(0))
					||ShapePosition3.childCount > 0  && figureCanBePlaced(ShapePosition3.GetChild(0)));
			}
		}

		private bool figureCanBePlaced(Transform  shapePosition){
				for (int x = 0; x < w; ++x) {
					for (int y = 0; y < h; ++y) {
					var position = new Vector2(x - shapePosition.parent.position.x , y - shapePosition.parent.position.y);
					if (checkPosition(shapePosition, position)) {
							return true;
						}
					}
				}
			 return false;
		}

		public bool checkPosition(Transform go){
			return checkPosition( go, new Vector2());
		}

		public bool checkPosition(Transform go, Vector2 offset){
			foreach (Transform child in go) {
				if(child.name=="Block"){
					Vector2 v = roundVec2(new Vector2 (child.position.x + offset.x, child.position.y + offset.y));
					if (!insideBorder(v) || grid[(int)v.x, (int)v.y] != null)
						return false;
				}
			}
			return true;
		} 

	}
}