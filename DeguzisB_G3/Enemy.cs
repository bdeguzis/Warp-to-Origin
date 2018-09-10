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
	public abstract class Enemy
	{
		private Texture2D tex;
		private Sprite sprite;
		private GraphicsContext graphics;
		private bool isAlive;
		
		public Enemy (GraphicsContext g, Texture2D t, int x, int y)
		{
			graphics = g;
			tex = t;
			sprite = new Sprite (graphics, tex);
			sprite.Position.X = x;
			sprite.Position.Y = y;
			isAlive = true;
			sprite.Scale = new Vector2 (.8f, .8f);
			
		}
		
		public Rectangle Extents 
		{
			get{ return new Rectangle (PosX, PosY, sprite.Width, sprite.Height);}
		}
		
		//Attribute for isAlive
		public bool IsAlive 
		{
			get{ return isAlive;}
			set{ isAlive = value;}
		}
		
		//Attribute for the enemy texture
		public Texture2D Tex
		{
			get { return tex; }
		}
		
		//Attribute for the enemy's x position 
		public float PosX 
		{
			get { return sprite.Position.X; }
			set { sprite.Position.X = value; }
		}
		
		//Attribute for the enemy's y position
		public float PosY 
		{
			get { return sprite.Position.Y; }
			set { sprite.Position.Y = value; }
		}
		
		//Shooting method to be overriden in sub classes
		public abstract void HandleEShooting ();
		
		//Update method to be overriden in sub classes
		public abstract void Update ();

		public void Render ()
		{
			sprite.Render ();
		}

	}
}
