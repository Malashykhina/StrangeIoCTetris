using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace game
{
	public class NewRoundCommand: EventCommand
	{
		[Inject]
		public ITetrisModel tetrisModel { get; set;}


		public override void Execute()
		{
			var rr = GameObject.Find ("View");
			var shp = rr.GetComponent<Shape>();
			if(tetrisModel.ShapePosition1.childCount==0 && tetrisModel.ShapePosition2.childCount==0 && tetrisModel.ShapePosition3.childCount==0){
				shp.Create(tetrisModel.ShapePosition1.position,tetrisModel.ShapePosition1 );
				shp.Create(tetrisModel.ShapePosition2.position,tetrisModel.ShapePosition2 );
				shp.Create(tetrisModel.ShapePosition3.position,tetrisModel.ShapePosition3 );
				Release ();
			}
		}
	}
}