//Name: Brian Deguzis
//Date: 4/27/14
//Project: Game 3

using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.UI;

namespace DeguzisB_G3
{
	public class MenuDisplay
	{
		private Label announcement; 
		private Label announcement2; 
		private Label announcement3; 
		private Label announcement4;
		private Label announcement5;
		private Label announcement6; 
		private GraphicsContext graphics;
		
		//Property Announcement
		public string Announcement
		{
			get {return announcement.Text;}
			set {announcement.Text = value;}
		}
		
		//Property Announcement2
		public string Announcement2
		{
			get {return announcement2.Text;}
			set {announcement2.Text = value;}
		}
		
		//Property Announcement3
		public string Announcement3
		{
			get {return announcement3.Text;}
			set {announcement3.Text = value;}
		}
		
		//Property Announcement
		public string Announcement4
		{
			get {return announcement4.Text;}
			set {announcement4.Text = value;}
		}
		
		//Property Announcement
		public string Announcement5
		{
			get {return announcement5.Text;}
			set {announcement5.Text = value;}
		}
		
		//Property Announcement
		public string Announcement6
		{
			get {return announcement6.Text;}
			set {announcement6.Text = value;}
		}
		
		//Text based announcements that display on the screen
		public MenuDisplay (GraphicsContext gc)
		{
		    graphics = gc;
			UISystem.Initialize(graphics);
			Scene scene = new Scene();
			
			announcement = new Label();
			announcement.X = 0;
			announcement.Y = graphics.Screen.Rectangle.Height / 2 - announcement.TextHeight / 2;
			announcement.Width = graphics.Screen.Rectangle.Width;
			announcement.HorizontalAlignment = HorizontalAlignment.Center;
			announcement.Text = "";
			scene.RootWidget.AddChildLast(announcement);
			
			announcement2 = new Label();
			announcement2.X = 0;
			announcement2.Y = graphics.Screen.Rectangle.Height / 2 - announcement2.TextHeight / 2 + announcement.Height;
			announcement2.Width = graphics.Screen.Rectangle.Width;
			announcement2.HorizontalAlignment = HorizontalAlignment.Center;
			announcement2.Text = "";
			scene.RootWidget.AddChildLast(announcement2);
			
			announcement3 = new Label();
			announcement3.X = 0;
			announcement3.Y = graphics.Screen.Rectangle.Height / 2 - announcement3.TextHeight / 2 + (announcement2.Height*2);
			announcement3.Width = graphics.Screen.Rectangle.Width;
			announcement3.HorizontalAlignment = HorizontalAlignment.Center;
			announcement3.Text = "";
			scene.RootWidget.AddChildLast(announcement3);	
			
			announcement4 = new Label();
			announcement4.X = 0;
			announcement4.Y = graphics.Screen.Rectangle.Height / 2 - announcement4.TextHeight / 2 + (announcement3.Height*3);
			announcement4.Width = graphics.Screen.Rectangle.Width;
			announcement4.HorizontalAlignment = HorizontalAlignment.Center;
			announcement4.Text = "";
			scene.RootWidget.AddChildLast(announcement4);
			
			announcement5 = new Label();
			announcement5.X = 0;
			announcement5.Y = graphics.Screen.Rectangle.Height / 2 - announcement3.TextHeight / 2 + (announcement4.Height*4);
			announcement5.Width = graphics.Screen.Rectangle.Width;
			announcement5.HorizontalAlignment = HorizontalAlignment.Center;
			announcement5.Text = "";
			scene.RootWidget.AddChildLast(announcement5);
			
			announcement6 = new Label();
			announcement6.X = 0;
			announcement6.Y = graphics.Screen.Rectangle.Height / 2 - announcement6.TextHeight / 2 + (announcement5.Height*5);
			announcement6.Width = graphics.Screen.Rectangle.Width;
			announcement6.HorizontalAlignment = HorizontalAlignment.Center;
			announcement6.Text = "";
			scene.RootWidget.AddChildLast(announcement6);
			
			UISystem.SetScene(scene, null);			
		}
		
		//Method for clearing all annoucements
		public void Clear()
		{
			announcement.Text = "";
			announcement2.Text = "";
			announcement3.Text = "";
			announcement4.Text = "";
			announcement5.Text = "";
			announcement6.Text = "";
		}
		
		public void Render ()
		{
			UISystem.Render();
		}
		
		
	}
}

