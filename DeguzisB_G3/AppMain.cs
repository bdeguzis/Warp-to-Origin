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
using System.IO;
using Sce.PlayStation.Core.Audio;

namespace DeguzisB_G3
{
	public class AppMain
	{
		private static GraphicsContext graphics;
		private static Player p;
		private static bool running;
		private enum GameState {Menu, Playing, Paused, Dead, Instructions, Credits, HighScore};
		private static GameState currentState;
		private enum GameWeapons
		{
			Missles,
			Lasers,
			Spread
		};
		private static GameWeapons currentWeapon;
		private static Texture2D eTex,pTex,mTex,lTex,sTex,powTex,ebTex,
					   menuTex,backTex,instTex,scoreTex,creditsTex,pauseTex,deadTex,eblueTex,egreenTex;
		private static int counter;
		private static List<Enemy> enemies;
		private static List<Pickup> pickups;
		private static List<Weapon> weapons;
		private static List<EnemyBullet> ebullets;
		private static int screenheight;
		private static int enemyCounter;
		private static Random gen;
		private static Vector4 red, green, blue;
		private static Stopwatch timer;
		private static long startTime, timeDelta, stopTime, enemyTimeCounter;
		private static bool hasLaser, hasMissle, hasSpread, spreadNext, laserNext;
		private static Pickup p1, p2;
		private static Vector3 weaponOffset;
		private static Background background, menuBackground, instBackground, 
					   scoreBackground, creditsBackground, pauseBackground, deadBackground;
		private static BackgroundPlanets backplanets;
		private static int playerScore;
		private static List<int> highScoreNums;
		private static List<string> highScoreNames;
		private static string[] highscores;
		private static MenuDisplay menuDisplay;
		private static bool updateScore, newHighScore, updateScoreS;
		private static string alphabet;
		private static char currentChar;
		private static int currentInd, indSave;
		private static string enterName;
		private static BgmPlayer bgmplay, bgmplay2;
		private static SoundPlayer missleSP;
		private static SoundPlayer explosionSP;
		private static SoundPlayer laserSP;
		
		public static void Main (string[] args)
		{
			Initialize ();
			
			//Main Game Loop
			while (running == true) 
			{
				startTime = timer.ElapsedMilliseconds;
				SystemEvents.CheckEvents ();
				Update ();
				Render ();
				stopTime = timer.ElapsedMilliseconds;
				timeDelta = stopTime - startTime;
			}
		}
		
		public static void Initialize ()
		{
			//Initializing variables
			graphics = new GraphicsContext ();
			eTex = new Texture2D ("/Application/Assets/enemyship.png", false);
			egreenTex = new Texture2D("/Application/Assets/enemyshipgreen.png", false);
			eblueTex = new Texture2D("/Application/Assets/enemyshipblue.png", false);
			pTex = new Texture2D ("/Application/Assets/starshipsmall.png", false);
			mTex = new Texture2D ("/Application/Assets/missle2.png", false);
			lTex = new Texture2D ("/Application/Assets/laser.png", false);
			sTex = new Texture2D ("/Application/Assets/laser.png", false);
			powTex = new Texture2D ("/Application/Assets/powerupspreadsmall.png", false);
			ebTex = new Texture2D ("/Application/Assets/enemybullet.png", false);
			menuTex = new Texture2D("/Application/Assets/gamestart.png", false);
			backTex = new Texture2D("/Application/Assets/game1back_2.png", false);
			instTex = new Texture2D("/Application/Assets/instructions.png", false);
			scoreTex = new Texture2D("/Application/Assets/highscores.png",false);
			creditsTex = new Texture2D("/Application/Assets/credits.png", false);
			pauseTex = new Texture2D("/Application/Assets/pause.png",false);
			deadTex = new Texture2D("/Application/Assets/dead.png", false);
			running = true;
			screenheight = graphics.Screen.Height;
			red = new Vector4 (1.0f, 1.0f, 1.0f, 1.0f);
			green = new Vector4 (0.0f, 1.0f, 0.0f, 1.0f);
			blue = new Vector4 (0.0f, 0.0f, 1.0f, 1.0f);
			gen = new Random ();
			timer = new Stopwatch ();
			timer.Start ();
			startTime = 0;
			stopTime = 0;
			highscores = new string[5];
			weaponOffset = new Vector3 (50, 10, 0);
			highScoreNums = new List<int>();
			highScoreNames = new List<string>();
			currentState = GameState.Menu;
			menuBackground = new Background(graphics, menuTex);
			instBackground = new Background(graphics, instTex);
			scoreBackground = new Background(graphics, scoreTex);
			creditsBackground = new Background(graphics, creditsTex);
			pauseBackground = new Background(graphics, pauseTex);
			deadBackground = new Background(graphics, deadTex);
			menuDisplay = new MenuDisplay(graphics);
			updateScore = false;
			updateScoreS = false;
			alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			currentChar = alphabet[0];
			currentInd = 0;
			Bgm bgm = new Bgm("/Application/Sounds/gamebgm.mp3");
			bgmplay = bgm.CreatePlayer();
			bgmplay.Loop = true;
			bgmplay.Play();
			Sound missleSound;
			missleSound = new Sound("/Application/Sounds/misslelaunch.wav");
			missleSP = missleSound.CreatePlayer();
			Sound explosionSound;
			Sound laserSound;
			explosionSound = new Sound("/Application/Sounds/explosion.wav");
			explosionSP = explosionSound.CreatePlayer();
			laserSound = new Sound("/Application/Sounds/se_tan00.wav");
			laserSP = laserSound.CreatePlayer();
		}
		
		//Attribute for timeDelta so other classes can access it
		public static long TimeDelta 
		{
			get{ return timeDelta;}
		}
		
		//Attribute for the list of Enemy Bullets so the enemy classes can access it
		public static List<EnemyBullet> Ebullets 
		{
			get{ return ebullets;}
			set{ ebullets = value;}
		}
		
		//Resets the game in order to make a fresh, new game
		public static void NewGame()
		{
			//Disposes of menu music, and plays game music
			bgmplay.Dispose();
			Bgm bgm2 = new Bgm("/Application/Sounds/RomanticFall.mp3");
			bgmplay2 = bgm2.CreatePlayer();
			bgmplay2.Loop = true;
			bgmplay2.Play();
			background = new Background(graphics,backTex);
			backplanets = new BackgroundPlanets(graphics);
			enemyTimeCounter = 0;
			weapons = new List<Weapon> ();
			enemies = new List<Enemy> ();
			pickups = new List<Pickup> ();
			ebullets = new List<EnemyBullet> ();
			p = new Player (graphics, 100, 100, pTex);
			counter = -1;
			currentWeapon = GameWeapons.Missles;
			enemyCounter = enemies.Count;	
			timeDelta = 0;
			hasLaser = false;
			hasMissle = true;
			hasSpread = false;
			p1 = new Pickup (graphics, powTex, gen.Next (100, 700), gen.Next (100, 500));
			pickups.Add (p1);
			p2 = new Pickup (graphics, powTex, gen.Next (100, 700), gen.Next (100, 500));
			pickups.Add (p2);
			spreadNext = false;
			laserNext = false;
			playerScore = 0;
			enterName = "";
			newHighScore = false;
		}
		
		public static void Update ()
		{
			//Switch for updating the proper state
			switch(currentState)
			{
			case GameState.Menu : UpdateMenu(); break;
			case GameState.Dead : UpdateDead(); break;
			case GameState.Paused : UpdatePaused(); break;
			case GameState.Playing : UpdatePlaying(); break;
			case GameState.Credits : UpdateCredits(); break;
			case GameState.Instructions : UpdateInstructions(); break;
			case GameState.HighScore : UpdateHighScores(); break;
			}
		}
		
		public static void UpdatePlaying()
		{
			
			var gamePadData = GamePad.GetData (0);
			if (p.IsAlive == true) 
			{
				if ((gamePadData.ButtonsDown & GamePadButtons.R) != 0) 
				{
					HandleShooting ();
				}
				
				//Switches Weapons
				if ((gamePadData.ButtonsDown & GamePadButtons.L) != 0) 
				{
					currentWeapon++;
					if (currentWeapon > GameWeapons.Spread)
						currentWeapon = GameWeapons.Missles;
						
				}
		
			}
			if ((gamePadData.ButtonsDown & GamePadButtons.Start) != 0)
			{
				currentState = GameState.Paused;
			}
			backplanets.Update();
			background.Update();
			HandleWeapons ();
			HandleEnemies ();
			HandleEnemyBullets ();
			ShipPickupCollision ();
			CheckForCollisions ();
			
			
			
			foreach (EnemyBullet eb in ebullets)
				eb.Update (); 
			
			if (ShipEnemyCollision () == true)
				p.IsAlive = false;
			
			if (ShipEnemyCollision() == true || ShipEnenmyBulletCollision() == true)
			{
				currentState = GameState.Dead;
			}
			//If you press Z or Select the game will close
			if ((gamePadData.ButtonsDown & GamePadButtons.Select) != 0) 
			{
				running = false;	
			}
			
			p.Update (gamePadData);
		}
		
		public static void UpdateCredits()
		{
			var gamePadData = GamePad.GetData (0);
			if ((gamePadData.ButtonsDown & GamePadButtons.Select) != 0)
			{
				currentState = GameState.Menu;
			}
		}
		
		public static void UpdateDead()
		{
			var gamePadData = GamePad.GetData (0);
			if ((gamePadData.ButtonsDown & GamePadButtons.Start) != 0)
			{
				currentState = GameState.HighScore;
				//Disposes of game music and plays menu music
				bgmplay2.Dispose();
				Bgm bgm = new Bgm("/Application/Sounds/gamebgm.mp3");
				bgmplay = bgm.CreatePlayer();
				bgmplay.Loop = true;
				bgmplay.Play();
			}
			updateScore = true;
	
		}
		
		public static void UpdatePaused()
		{
			var gamePadData = GamePad.GetData (0);
			if ((gamePadData.ButtonsDown & GamePadButtons.Start) != 0)
			{
				currentState = GameState.Playing;
			}
			if ((gamePadData.ButtonsDown & GamePadButtons.Select) != 0)
			{
				currentState = GameState.Menu;
				//Disposes of game music and plays menu music
				bgmplay2.Dispose();
				Bgm bgm = new Bgm("/Application/Sounds/gamebgm.mp3");
				bgmplay = bgm.CreatePlayer();
		        bgmplay.Loop = true;
				bgmplay.Play();
			}
		}
		
		public static void UpdateHighScores()
		{
			char[] delims = { ',' };
			int tempn = 0;
			string temps = "";
			try{
			StreamReader sr = new StreamReader("/Documents/highscores.txt");
			
			while (sr.EndOfStream != true)
			{
				string data_line = sr.ReadLine();
				highscores = data_line.Split(delims);
				highScoreNums.Add((int.Parse(highscores[0])));
				highScoreNames.Add(highscores[1]);
			}
			
			//Checks and updates highscores	
			for (int i = 0; i < 5; i++)
				if (playerScore > highScoreNums[i] && updateScore == true)
				{
					tempn = highScoreNums[i];
				 	highScoreNums[i] = playerScore;
					highScoreNums.Insert(i+1,tempn);
					indSave = i;
					updateScore = false;
					updateScoreS = true;
					newHighScore = true;
				}
			
			StreamWriter sw = new StreamWriter("/Documents/highscores.txt");
			for (int j = 0; j < 5; j++)
				sw.WriteLine(highScoreNums[j] + "," + highScoreNames[j]);
			
			sw.Close();
				}
			//If no scores exist, then empty scores are generated
			catch (FileNotFoundException)
				{   
				
				StreamWriter sww = new StreamWriter("/Documents/highscores.txt");
				for (int f = 0; f < 5; f++)
				{
					sww.WriteLine("0,AAA");
					highScoreNums.Add(0);
					highScoreNames.Add("AAA");
				}
				sww.Close();
				}
			
			var gamePadData = GamePad.GetData (0);
			if ((gamePadData.ButtonsDown & GamePadButtons.Select) != 0)
			{
				currentState = GameState.Menu;
			}
			
			if ((gamePadData.ButtonsDown & GamePadButtons.L) != 0)
			{
				currentState = GameState.Credits;
			}
			
			//If there is a new highscore, then traverse through the alphabet and enter initials
			if (newHighScore == true)
			{
				currentChar = alphabet[currentInd];
				if ((gamePadData.ButtonsDown & GamePadButtons.Circle) != 0)
				{
					currentInd++;
					if (currentInd > 25)
						currentInd = 0;
				}
				
				if ((gamePadData.ButtonsDown & GamePadButtons.Square) != 0)
				{
					currentInd--;
					if (currentInd < 0)
						currentInd = 24;
				}
				
				if ((gamePadData.ButtonsDown & GamePadButtons.Cross ) != 0 && enterName.Length < 3)
				{
					enterName += currentChar;	
				}
				if (enterName.Length >= 3)
				{
					for (int i = 0; i < 5; i++)
						if (playerScore > highScoreNums[i] && updateScoreS == true)
							{	
								temps = highScoreNames[i];
								highScoreNames[indSave] = enterName;
								highScoreNames.Insert(i+1,temps);
								updateScoreS = false;
							}
					newHighScore = false;
				}
			}
		}
		
		
		public static void UpdateMenu()
		{
			var gamePadData = GamePad.GetData (0);
			//Starts a new game when the start button is pressed
			if ((gamePadData.ButtonsDown & GamePadButtons.Start) != 0)
			{
				NewGame();
				currentState = GameState.Playing;
			}
			//Ends the game when the select button is pressed
			if ((gamePadData.ButtonsDown & GamePadButtons.Select) != 0)
				running = false;
			
			//Displays the Instructions if the R Button is pressed
			if ((gamePadData.ButtonsDown & GamePadButtons.R) != 0)
				currentState = GameState.Instructions;
			
			if ((gamePadData.ButtonsDown & GamePadButtons.L) != 0)
				currentState = GameState.HighScore;
		}
		
		public static void UpdateInstructions()
		{
			var gamePadData = GamePad.GetData (0);
			if ((gamePadData.ButtonsDown & GamePadButtons.Select) != 0)
			{
				currentState = GameState.Menu;
			}
		}
		
		private static bool ShipEnemyCollision ()
		{
			foreach (Enemy e in enemies) 
			{	
				Rectangle enemyExtents = e.Extents;
				Rectangle playerExtents = p.Extents;
				if (Overlaps (enemyExtents, playerExtents))
					return true;
			}
			return false;
		}
		
		private static bool ShipEnenmyBulletCollision ()
		{
			foreach (EnemyBullet eb in ebullets) 
			{	
				Rectangle enemybExtents = eb.Extents;
				Rectangle playerExtents = p.Extents;
				if (Overlaps (enemybExtents, playerExtents))
					return true;
			}
			return false;
		}
		
		private static void ShipPickupCollision ()
		{
			foreach (Pickup pu in pickups) 
			{	
				Rectangle pickupExtents = pu.Extents;
				Rectangle playerExtents = p.Extents;
				if (Overlaps (pickupExtents, playerExtents) && pu.IsAlive == true) 
				{
					NewPickup ();
					pu.IsAlive = false;
				}
			}
		}
		
		//Collision Detection
		private static bool Overlaps (Rectangle r1, Rectangle r2)
		{
			if (r1.X + r1.Width < r2.X)
				return false;
			if (r1.X > r2.X + r2.Width)
				return false;
			if (r1.Y + r1.Height < r2.Y)
				return false;
			if (r1.Y > r2.Y + r2.Height)
				return false;
			
			return true;
		}
		
		private static void CheckForCollisions ()
		{
			for (int w = 0; w < weapons.Count; w++) 
			{
				for (int e = 0; e < enemies.Count; e++)
				{
					Rectangle weaponExtents = weapons [w].Extents;
					Rectangle enemyExtents = enemies [e].Extents;
					if (Overlaps (weaponExtents, enemyExtents) == true) 
					{
						weapons [w].IsAlive = false;
						enemies [e].IsAlive = false;
						playerScore += 5;
						explosionSP.Volume = .5f;
						explosionSP.Play();
					}
				}
			}
			for (int e = enemies.Count -1; e >=0; e--) 
			{
				if (enemies [e].IsAlive == false)
					enemies.RemoveAt (e);
			}
			for (int w = weapons.Count -1; w >=0; w--) 
			{
				if (weapons [w].IsAlive == false)
					weapons.RemoveAt (w);
			}
			
		}
		
		public static void HandleEnemyBullets ()
		{
			for (int i = ebullets.Count - 1; i >= 0; i--) 
			{
				if (ebullets [i].X < -100)
					ebullets [i].IsAlive = false;
				
				if (ShipEnenmyBulletCollision () == true && p.IsAlive != false) 
				{
					p.IsAlive = false;
					ebullets [i].IsAlive = false;
				}
				
				if (ebullets [i].IsAlive == false)
					ebullets.RemoveAt (i);
			}
		}
		//Depending on the selected weapon, and the weapons in the player's inventory, the corresponding weapon will be fired.
		public static void HandleShooting ()
		{
			switch (currentWeapon) 
			{
			case(GameWeapons.Missles):
				{	
					if (hasMissle == true) 
					{
						missleSP.Volume = .35f;
				 	 	missleSP.Play();
						Missle m = new Missle (graphics, mTex, (p.Position + weaponOffset), p.Rotation);
						weapons.Add (m);
						break;
					}
					currentWeapon = GameWeapons.Lasers;
					HandleShooting ();
					break;
				}
			case(GameWeapons.Lasers):
				{
					if (hasLaser == true) 
					{
						laserSP.Volume = .25f;
						laserSP.Play();
						Laser l = new Laser (graphics, lTex, (p.Position + weaponOffset), p.Rotation);
						weapons.Add (l);
						break;
					}    
					currentWeapon = GameWeapons.Spread;
					HandleShooting ();
					break;
				}
			case(GameWeapons.Spread):
				{
					if (hasSpread == true) 
					{
						laserSP.Volume = .25f;
						laserSP.Play();
						SpreadGun sp = new SpreadGun (graphics, sTex, (p.Position + weaponOffset), counter, p.Rotation);
						weapons.Add (sp);
						counter ++;
						if (counter > 1)
							counter = -1;
						break;
					}
					currentWeapon = GameWeapons.Missles;
					HandleShooting ();
					break;
				}
			}
		}
		
		private static void HandleEnemies ()
		{	
			enemyCounter = enemies.Count;
			enemyTimeCounter += timeDelta;
			int randEnemy = gen.Next (0, 3);
			int offset;
			offset = gen.Next (-265, 225);
			foreach (Enemy e in enemies) 
			{
				e.Update ();
			}
			
			for (int i = enemies.Count - 1; i >= 0; i--) 
			{
				if (enemies [i].PosX < -100)
					enemies.RemoveAt (i);
			}
			
			timeDelta = startTime - (timer.ElapsedMilliseconds * 100);
			//A random enemy of the three types is chosen and made 
			if (enemies.Count <= 5 && enemyTimeCounter > 500) 
			{
				enemyTimeCounter -= 500;
				
				switch (randEnemy) 
				{
				case 0:
					enemies.Add (new Normal_Enemy (graphics, eTex, graphics.Screen.Rectangle.Width + 50,
					                                     graphics.Screen.Rectangle.Height / 2 + offset, ebTex));
					break;
				case 1:
					enemies.Add (new Bouncing_Enemy (graphics, egreenTex, graphics.Screen.Rectangle.Width + 50,
					                                      graphics.Screen.Rectangle.Height / 2 + offset, screenheight, ebTex)); 
					break;
				case 2:
					enemies.Add (new Random_Enemy (graphics, eblueTex, graphics.Screen.Rectangle.Width + 50,
					                                    graphics.Screen.Rectangle.Height / 2 + offset, screenheight, ebTex)); 
					break;
				}
			}
		}
		
		private static void HandleWeapons ()
		{
			foreach (Weapon w in weapons) 
			{
				w.Update ();
			}
			for (int i = weapons.Count - 1; i >= 0; i--) 
			{
				if (weapons [i].X > graphics.Screen.Rectangle.Width) 
				{
					weapons.RemoveAt (i);
				}
			}
		}
		
		//Allows a new random weapon to be added to the player's inventory
		public static void NewPickup ()
		{
			int randpick = gen.Next (0, 2);
			if (spreadNext == true)
				hasSpread = true;
			if (laserNext == true)
				hasLaser = true;
			switch (randpick) 
			{
			case 0:
				hasLaser = true;
				spreadNext = true;
				break;
			case 1:
				hasSpread = true;
				laserNext = true;
				break;
			}
			
		}
		
		//Draws all of the assets to the screen
		public static void Render ()
		{
			graphics.Clear ();
			graphics.SetClearColor (0.0f, 0.0f, 0.0f, 0.0f);
			switch(currentState)
			{
				case GameState.Playing : RenderPlaying(); break;
				case GameState.Dead : RenderDead(); break;
				case GameState.Menu : RenderMenu(); break;
				case GameState.Paused : RenderPaused(); break;
				case GameState.Credits : RenderCredits(); break;
				case GameState.Instructions : RenderInstructions(); break;
				case GameState.HighScore : RenderHighscores(); break;
			}
			// Present the screen
			graphics.SwapBuffers ();
		}
		
		public static void RenderCredits()
		{
			creditsBackground.Render();
		}
		
		public static void RenderDead()
		{
			deadBackground.Render();
			menuDisplay.Clear();
			menuDisplay.Announcement = "Score: " + playerScore;
			menuDisplay.Render();
		}
		
		public static void RenderMenu()
		{
			menuBackground.Render();
		}
		
		public static void RenderPaused()
		{
			pauseBackground.Render();
		}
		
		public static void RenderInstructions()
		{
			instBackground.Render();
		}
		
		
		public static void RenderHighscores()
		{
			scoreBackground.Render();
			menuDisplay.Clear();
			//Displays highscores, or a blank set if none exist
			try{
			menuDisplay.Announcement = highScoreNums[0] +" "+ highScoreNames[0];
			menuDisplay.Announcement2 = highScoreNums[1] +" "+ highScoreNames[1];
			menuDisplay.Announcement3 = highScoreNums[2] +" "+ highScoreNames[2];
			menuDisplay.Announcement4 = highScoreNums[3] +" "+ highScoreNames[3];
			menuDisplay.Announcement5 = highScoreNums[4] +" "+ highScoreNames[4];
			}
			catch
			{
			menuDisplay.Announcement = "0 AAA";
			menuDisplay.Announcement2 = "0 AAA";
			menuDisplay.Announcement3 = "0 AAA";
			menuDisplay.Announcement4 = "0 AAA";
			menuDisplay.Announcement5 = "0 AAA";
			}
			if (newHighScore == true)
				menuDisplay.Announcement6 = "Enter Initials: "+currentChar + " " + enterName;
			else
				menuDisplay.Announcement6 = "";
			menuDisplay.Render();
		}
		
		public static void RenderPlaying()
		{
			background.Render();
			backplanets.Render();
			if (p.IsAlive == true)
				p.Render ();
			foreach (Weapon w in weapons) 
			{
				w.Render ();
			}
			foreach (Enemy e in enemies) 
			{
				e.Render ();
			}
			foreach (Pickup pu in pickups) 
			{
				if (pu.IsAlive == true)
					pu.Render ();
			}
			foreach (EnemyBullet eb in ebullets) 
			{
				eb.Render ();
			}
						
		}
	}
}
