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
	public class Missle : Weapon
	{
		private float speed;
		
		public Missle (GraphicsContext gc, Texture2D t,Vector3 pos,float pr) :base(gc,t,pos,pr)
		{
			speed = 3;
		}
		
		public override void Update()
		{
			Position += speed * Direction;
			
		}
		
	}
}

