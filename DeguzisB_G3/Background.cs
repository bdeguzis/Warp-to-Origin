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
	public class Background
	{
		private Sprite stars, stars2;
		private GraphicsContext graphics;
		private Texture2D t;
		
		public Texture2D Texture 
		{
			get {return t;}
			set {t = value;}
		}
		
		public Background (GraphicsContext gc, Texture2D tex)
		{
			//Properties for the background
			graphics = gc;
			t = tex;
			stars = new Sprite(graphics, t); 
			stars.Position.X = 0;
			stars.Position.Y = 0;
			stars2 = new Sprite(graphics, t); 
			stars2.Position.X = stars.Width;
			stars2.Position.Y = 0;
		}
	
	
		public void Update()
		{
			stars.Position.X --;
			stars2.Position.X --;	
			//Allows for infinite scrolling
			if (stars.Position.X < -stars.Width)
			{
				stars.Position.X = 0;
				stars2.Position.X = stars.Width;
			}
		}
		
		public void Render ()
		{
			stars.Render();
			stars2.Render();
		}
	}
}

