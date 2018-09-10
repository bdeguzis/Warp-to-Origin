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
	public class Bouncing_Enemy : Enemy
	{
		private int speedx;
		private int speedy;
		private int screenHeight;
		private long tD, shootTime, aliveTime;
		private GraphicsContext graphics;
		private Texture2D ebTex;

		public Bouncing_Enemy (GraphicsContext g, Texture2D t, int x, int y, int scrh,Texture2D ebt):base(g,t,x,y)
		{
			speedx = -2;
			speedy = 5;
			screenHeight = scrh;
			graphics = g;
			ebTex = ebt;
		}
		
		//Method for shooting
		public override void HandleEShooting ()
		{
			EnemyBullet eb2 = new EnemyBullet (graphics, ebTex, new Vector3 (PosX - 10, PosY, 0f), 0f, 2);
			AppMain.Ebullets.Add (eb2);
		}
		
		public override void Update ()
		{
			PosX += speedx;
			PosY += speedy;
			//Keeps the enemy in bounds
			if (PosY < 0 || PosY > screenHeight - Tex.Height)
				speedy *= -1;
			tD = AppMain.TimeDelta;
			shootTime += tD;
			aliveTime += tD;
			if (PosX <= 1000 && shootTime > 500) 
			{
				HandleEShooting ();
				shootTime = 0;
			}
		}	
		
	}
}

