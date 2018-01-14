using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace game
{
	public class StartCommand : EventCommand
	{
		[Inject]
		public ITetrisModel tetrisModel { get; set;}

		public override void Execute()
		{	

			var go = new GameObject{name = "View"};
			go.AddComponent<Shape> ();
			dispatcher.Dispatch("NEWROUND");
			Release ();
		}
	}
}