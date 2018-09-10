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
	public class Normal_Enemy : Enemy
	{
		private int speedX;
		private GraphicsContext graphics;
		private Texture2D ebTex;
		private long tD, shootTime, aliveTime;
		
		public Normal_Enemy (GraphicsContext g, Texture2D t, int x, int y, Texture2D ebt):base(g,t,x,y)
		{
			graphics = g;
			speedX = -2;
			ebTex = ebt;
			shootTime = 0;
			aliveTime = 0;
		}
		
		//Method for shooting
		public override void HandleEShooting ()
		{
			EnemyBullet eb = new EnemyBullet (graphics, ebTex, new Vector3 (PosX - 10, PosY, 0f), 0f, 1);
			EnemyBullet eb2 = new EnemyBullet (graphics, ebTex, new Vector3 (PosX - 10, PosY, 0f), 0f, 2);
			EnemyBullet eb3 = new EnemyBullet (graphics, ebTex, new Vector3 (PosX - 10, PosY, 0f), 0f, 3);
			AppMain.Ebullets.Add (eb);
			AppMain.Ebullets.Add (eb2);
			AppMain.Ebullets.Add (eb3);
		}
		
		public override void Update ()
		{
			PosX += speedX;
			tD = AppMain.TimeDelta;
			shootTime += tD;
			aliveTime += tD;
			//Waits until the enemy is part way into the screen to shoot
			if (PosX <= 700 && shootTime > 500) 
			{
				speedX = 0;
				HandleEShooting ();
				shootTime = 0;
			}
			if (aliveTime > 8000) 
			{
				speedX = -8;
			}
		}
	}
}

