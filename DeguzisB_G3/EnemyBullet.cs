//Name: Brian Deguzis
//Date: 4/27/14
//Project: Game 3

using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using System.Diagnostics;

namespace DeguzisB_G3
{
	public class EnemyBullet: Weapon
	{
		private int speed;
		private int temp;
		private int angle;
		private Random gen;
		
		public EnemyBullet (GraphicsContext gc, Texture2D t, Vector3 pos, float pr, int num) :base(gc,t,pos,pr)
		{
			speed = 4;
			angle = num;
			gen = new Random ();
			temp = gen.Next (0, 2);
			//If the enemy is random, the speed is set to a random value.
			if (angle == 4) 
			{
				if (temp == 0)
					speed = gen.Next (3, 6);
				if (temp == 1)
					speed = gen.Next (-5, -2);
			}
		}
		
		public override void Update ()
		{
			//Updates bullet movement based on which enemy it is.
			X -= speed;
			if (angle == 1)
				Y += (speed - 3);
			if (angle == 3)
				Y -= (speed - 3);	
			if (angle == 4)
				Y += speed;
		}
	}
}

