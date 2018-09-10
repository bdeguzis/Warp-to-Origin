//Name: Brian Deguzis
//Date: 4/27/14
//Project: Game 3

using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace DeguzisB_G3
{
	public class SpreadGun : Weapon
	{
		private float speed;
		private int diag;
		
		public SpreadGun (GraphicsContext gc, Texture2D t, Vector3 pos, int d, float pr) :base(gc,t,pos,pr)
		{
			speed = 5;
			diag = d;
		}
		
		public override void Update ()
		{
			
			Position += speed * Direction;
			Rotation = 0;
			if (diag == 1) 
			{
				Y += speed;
				Rotation = .05f;
			}
			if (diag == -1) 
			{
				Y -= speed;
				Rotation = -.05f;
			}
		}
	}
}

