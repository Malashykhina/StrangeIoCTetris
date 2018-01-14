using System;
using UnityEngine;

namespace game
{
	public interface ITetrisModel
	{
		Transform[,] grid { get; set;}

		Transform ShapePosition1 { get; set;}
		Transform ShapePosition2 { get; set;}
		Transform ShapePosition3 { get; set;}
		bool insideBorder(Vector2 pos);
		Vector2 roundVec2(Vector2 v);
		void deleteFullRows() ;
		bool checkPosition(Transform go, Vector2 offset);
		bool checkPosition(Transform go);
		bool canBePlaced { get; }
	}
}