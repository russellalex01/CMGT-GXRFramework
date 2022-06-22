﻿using System;
using GXPEngine.Core;
using GXPEngine.OpenGL;
using GXPEngine.Framework;

namespace GXPEngine
{
	/// <summary>
	/// Implements a line with normal representation
	/// </summary>
	public class NLineSegment : LineSegment
	{
		private Arrow _normal;

		public bool gameLine;


		private string currentHealth;

		private int startingHealth;

		private int health;

		public NLineSegment(float pStartX, float pStartY, float pEndX, float pEndY, uint pColor = 0xffffffff, uint pLineWidth = 1)
			: this(new Vec2(pStartX, pStartY), new Vec2(pEndX, pEndY), pColor, pLineWidth)
		{
		}

		public NLineSegment(Vec2 pStart, Vec2 pEnd, uint pColor = 0xffffffff, uint pLineWidth = 1, bool gameLine = false)
			: base(pStart, pEnd, pColor, pLineWidth)
		{
			_normal = new Arrow(new Vec2(0, 0), new Vec2(0, 0), 40, 0xffff0000, 1);
			AddChild(_normal);

			this.gameLine = gameLine;
			if (gameLine)
            {
				startingHealth = Utils.Random(10, 11);
				health = startingHealth;
				currentHealth = health.ToString();
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														RenderSelf()
		//------------------------------------------------------------------------------------------------------------------------
		override protected void RenderSelf(GLContext glContext)
		{ 
			if (game != null)
			{
				recalculateArrowPosition();
				Gizmos.RenderLine(start.x, start.y, end.x, end.y, color, lineWidth);
			}
		}

		private void recalculateArrowPosition()
		{
			_normal.startPoint = (start + end) * 0.5f;
			_normal.vector = (end - start).Normal();
		}

		public virtual void OnHit()
        {
			Console.WriteLine("hit: " + health);
			if (health <= 1)
			{
				MyGame myGame = (MyGame)Game.main;
				if (myGame != null)
				{
					myGame.gameLines.Remove(this);

					myGame.RemoveChild(this);
				}
				this.Remove();
			}
			else
            {
				MyGame myGame = (MyGame)Game.main;
				if (myGame != null)
				{
					myGame.score++;
				}
				health--;
			}
		}
	}
}

