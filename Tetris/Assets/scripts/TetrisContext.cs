using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;

namespace game
{
	public class TetrisContext : MVCSContext
	{

		public TetrisContext (MonoBehaviour view) : base(view)
		{
		}

		public TetrisContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
		{
		}

		protected override void mapBindings()
		{
			injectionBinder.Bind<ITetrisModel>().To<TetrisModel>().ToSingleton();
			commandBinder.Bind("GAMEOVER").To<GameOverCommand>();
			commandBinder.Bind("DROP").To<DropCommand>();
			commandBinder.Bind("NEWROUND").To<NewRoundCommand>();

			commandBinder.Bind(ContextEvent.START).To<StartCommand>().Once();
		}
	}
}