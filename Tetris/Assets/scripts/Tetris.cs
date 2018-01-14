using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
namespace game
{
	public class Tetris : ContextView
	{

		void Awake()
		{
			context = new TetrisContext(this);
		}
	}
}