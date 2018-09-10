//Name: Brian Deguzis
//Date: 4/27/14
//Project: Game 3

using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Audio;
using System.Diagnostics;

namespace DeguzisB_G3
{
	public class Player
	{
		private Sprite s;
		private GraphicsContext graphics;
		private int speed;
		private float rot;
		private Rectangle extents;
		private bool isAlive;
		
		public bool IsAlive 
		{
			get{ return isAlive;}
			set{ isAlive = value;}
		}
		
		public Rectangle Extents 
		{
			get{ return new Rectangle (X, Y, s.Width, s.Height);}
		}
		
		public Player (GraphicsContext gc, int x, int y, Texture2D t)
		{
			//Properties for the ships
			graphics = gc;
			Texture2D tex = t;
			s = new Sprite (graphics, t);
			speed = 5;
			s.Position.X = x;
			s.Position.Y = y;
			s.Scale = new Vector2 (.5f, .5f);
			isAlive = true;
		}
		
		
		//Property for the Player's X coordinate
		public float X 
		{
			get { return s.Position.X; }
			set { s.Position.X = value;}
		}
		
		//Property for the Player's Y coordinate
		public float Y 
		{
			get { return s.Position.Y; }
			set { s.Position.Y = value;}
		}
		
		//Property for the Player's full position
		public Vector3 Position 
		{
			get{ return s.Position;}
			set{ s.Position = value;}
		}
		
		//Property for the Player's full position 
		public float Rotation 
		{
			get{ return rot;}
			set{ rot = value;}
		}
		
		public void Update (GamePadData gamePadData)
		{
			//Checks for keypresses and adjusts the ship accordingly
			rot = Rotation;
			if ((gamePadData.Buttons & GamePadButtons.Left) != 0) 
			{
				if (s.Position.X - speed >= 0)
					s.Position.X -= speed;
			}
			
			if ((gamePadData.Buttons & GamePadButtons.Right) != 0) 
			{
				if (s.Position.X + s.Width + speed < graphics.Screen.Rectangle.Width)
					s.Position.X += speed;
				
			}
			
			if ((gamePadData.Buttons & GamePadButtons.Up) != 0) 
			{
				if (s.Position.Y - speed >= 0)
					s.Position.Y -= speed;
			}
			
			if ((gamePadData.Buttons & GamePadButtons.Down) != 0) 
			{
				if (s.Position.Y + s.Height + speed <= graphics.Screen.Height)
					s.Position.Y += speed;
			}
			if ((gamePadData.Buttons & GamePadButtons.Square) != 0) 
			{
				rot -= .05f;	
			}
			if ((gamePadData.Buttons & GamePadButtons.Circle) != 0) 
			{
				rot += .05f;	
			}
			
			//If cross is held, then the player will slow down
			if ((gamePadData.Buttons & GamePadButtons.Cross) != 0) 
			{
				speed = 2;	
			}
			else speed = 4;
			
		}
		
		public void Render ()
		{
			s.Render ();
		}
	}
}

