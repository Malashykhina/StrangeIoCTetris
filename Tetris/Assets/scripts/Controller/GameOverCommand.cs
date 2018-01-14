using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.context.api;

namespace game
{
	public class GameOverCommand : EventCommand
	{
		public override void Execute()
		{
			Debug.Log ("GameOver");
		}
	}
}