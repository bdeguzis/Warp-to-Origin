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
	public abstract class  Weapon
	{
       
		private Sprite sprite;
		private float rotation;
		private Vector3 direction;
		private bool isAlive;
         
		public Weapon (GraphicsContext g, Texture2D t, Vector3 pos, float pr)
		{
			sprite = new Sprite (g, t);
			rotation = pr;
			sprite.Position = pos;
			//direction is used to determine the starting place for weapons which depend of player rotation
			direction = new Vector3 ((float)Math.Cos (rotation), (float)Math.Sin (rotation), 0);
			isAlive = true;
			sprite.Scale = new Vector2 (.7f, .7f);
		}
		
		public Rectangle Extents 
		{
			get{ return new Rectangle (X, Y, sprite.Width, sprite.Height);}
		}
		
		//Property for isAlive
		public bool IsAlive 
		{
			get{ return isAlive;}
			set{ isAlive = value;}
		}
		
		//Property for the weapon's x coordinate
		public float X 
		{
			get { return sprite.Position.X; }
			set { sprite.Position.X = value;}
		}
		
		//Property for the weapon's y coordinate
		public float Y 
		{
			get { return sprite.Position.Y; }
			set { sprite.Position.Y = value;}
		}
		
		//Property for the weapon's full position
		public Vector3 Position 
		{
			get{ return sprite.Position;}
			set{ sprite.Position = value;}
		}
		
		//Property for the weapon's rotation
		public float Rotation 
		{
			get{ return sprite.Rotation;}
			set{ sprite.Rotation = value;}
		}
		
		//Property for the weapon's direction
		public Vector3 Direction 
		{
			get{ return direction;}
		}
		
		//Method to be overriden in the sub classes
		public abstract void Update ();

		public void Render ()
		{
			sprite.Render ();
		}
	}
}
