using System;
using strange.extensions.command.impl;
using UnityEngine;
using strange.extensions.context.api;

namespace game
{
	public class DropCommand: EventCommand
	{
		[Inject]
		public ITetrisModel tetrisModel { get; set;}

		[Inject(ContextKeys.CONTEXT_VIEW)]
		public GameObject camera{ get; set;}

		public override void Execute()
		{
			camera.GetComponent<SoundEffects>().playDropAudio();
			var go = (GameObject)evt.data;
			if (tetrisModel.checkPosition (go.transform)) {
				foreach (Transform child in go.transform) {
					if(child.name=="Block"){
						var pos = tetrisModel.roundVec2 (child.position);
						var no = GameObject.Instantiate (child.gameObject, pos, child.rotation);
						tetrisModel.grid [(int)pos.x, (int)pos.y] = no.transform;
					}
				}
				GameObject.Destroy (go);
				go.transform.parent = null;
				tetrisModel.deleteFullRows ();
				if(!tetrisModel.canBePlaced){
					dispatcher.Dispatch("NEWROUND"); 
				}
				if (!tetrisModel.canBePlaced){
					dispatcher.Dispatch("GAMEOVER"); 
				}
			} else {
				go.transform.position = go.transform.parent.position; 
			}

		}	
	}
}