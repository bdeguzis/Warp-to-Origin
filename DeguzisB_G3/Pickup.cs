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
	public class Pickup
	{
		private GraphicsContext graphics;
		private Sprite s;
		private bool isAlive;
		
		public Rectangle Extents
		{
			get{return new Rectangle (X,Y,s.Width,s.Height);}
		}
		
		public float Y
		{
			get{return s.Position.Y;}
			set{s.Position.Y = value;}
		}
		
		public float X
		{
			get{return s.Position.X;}
			set{s.Position.X = value;}
		}
		
		public bool IsAlive
		{
			get{return isAlive;}
			set{isAlive = value;}
		}
		
		public Pickup (GraphicsContext g,Texture2D pu,int x, int y)
		{
			graphics = g;
			s = new Sprite(g,pu);
			s.Position.X = x;
			s.Position.Y = y;
			isAlive = true;
			s.Scale = new Vector2(.65f,.65f);
		}
		
		
		public void Render()
		{
			s.Render();
		}
	}
}

