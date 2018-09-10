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
	public class Random_Enemy : Enemy
	{
		
		private int speedx;
		private static Random gen;
		private int counter;
		private int randSpeed;
		private int screenHeight;
		private GraphicsContext graphics;
		private Texture2D ebTex;
		private long tD, shootTime,aliveTime;
		
		public Random_Enemy (GraphicsContext g, Texture2D t, int x, int y,int scrh,Texture2D ebt):base(g,t,x,y)
		{
			graphics = g;
			speedx = -2;
			gen = new Random();
			counter = 0;
			randSpeed = 0;
			screenHeight = scrh;
			ebTex = ebt;
			shootTime = 0;
			aliveTime = 0;
		}
		
		//Method for shooting
		public override void HandleEShooting()
		{
			EnemyBullet eb2 = new EnemyBullet(graphics, ebTex,new Vector3(PosX - 10, PosY,0f),0f,4);
			AppMain.Ebullets.Add(eb2);
		}
		
		
		public override void Update ()
		{
			tD = AppMain.TimeDelta;
			shootTime += tD;
			aliveTime += tD;
			//Randomizes the enemy's Y axis movement
			if (counter == 10)
			{
				randSpeed = gen.Next(-5,5);
				counter = 0;
			}
			PosX += speedx;
			//Keeps the enemy in bounds
			if (PosY < 0 || PosY > screenHeight-Tex.Height)
                randSpeed *= -1;
			PosY += randSpeed;
			counter++;
			if (PosX <= 1000 && shootTime > 500)
			{
				HandleEShooting();
				shootTime = 0;
			}
			
		}
		
		
		
	}
}

