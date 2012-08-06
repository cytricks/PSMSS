using System;
using System.Collections.Generic;

using System.Threading;
using System.Threading.Tasks;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core;
	
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
	
using Sce.PlayStation.Core.Imaging;

namespace SpaceShooter
{
	public class SurvivalMode
	{
		public static Sce.PlayStation.HighLevel.GameEngine2D.Base.Timer timer;
		public static int counter;
		public static int FPS;
		public static PlayerClass player;
		public static Scene scene;
		static bool firable;
		static Texture2D ballTexture;
		static Texture2D ballTexture1;
		static Texture2D backGround;
		static Texture2D backGround1;
		static Texture2D enemy1;
		static Texture2D enemy2;
		static List<ShotClass> shots;
		static List<EnemyClass> enemies;
		static List<Texture2D> ex;
		static List<SimpleDead> dead;		
		static Random rand;
		static Vector2 uppercorner;
		static int score=0;
		static int highscore=0;
		static Image img;
		static Texture2D texturet;
		public static Sce.PlayStation.HighLevel.GameEngine2D.Base.Timer timer1;
		public static Sce.PlayStation.HighLevel.GameEngine2D.Base.Timer cleanuptimer;
		static float alpha = 0;
		static Sce.PlayStation.HighLevel.GameEngine2D.Base.Timer altimer = new Sce.PlayStation.HighLevel.GameEngine2D.Base.Timer();
		static Thread oThread,pThread; 
		static Sce.PlayStation.HighLevel.GameEngine2D.Base.Timer timercleanup = new Sce.PlayStation.HighLevel.GameEngine2D.Base.Timer();

		
		public SurvivalMode (Scene s)
		{
			scene = s;
			Init();
			
		}
		
		public static void Init(){
			
			ex = new List<Texture2D>();
			
			ex.Add(new Texture2D("/Application/textures/ex0.png", false));
			ex.Add(new Texture2D("/Application/textures/ex1.png", false));
			ex.Add(new Texture2D("/Application/textures/ex2.png", false));
			ex.Add(new Texture2D("/Application/textures/ex3.png", false));
			ex.Add(new Texture2D("/Application/textures/ex4.png", false));
			ex.Add(new Texture2D("/Application/textures/ex5.png", false));
			ex.Add(new Texture2D("/Application/textures/ex6.png", false));
			ex.Add(new Texture2D("/Application/textures/ex7.png", false));
			ex.Add(new Texture2D("/Application/textures/ex8.png", false));
			ex.Add(new Texture2D("/Application/textures/ex9.png", false));
			ex.Add(new Texture2D("/Application/textures/ex10.png", false));
			ex.Add(new Texture2D("/Application/textures/ex11.png", false));
			
			ballTexture = new Texture2D("/Application/textures/ss.png", false);
			ballTexture1 = new Texture2D("/Application/textures/^.png", false);			
			backGround = new Texture2D("/Application/textures/bg.png", false);
			backGround1 = new Texture2D("/Application/textures/bgw.png", false);
			
	   		player = new PlayerClass();
			player.bounds = new Vector2( Director.Instance.GL.Context.GetViewport().Width,
			                            Director.Instance.GL.Context.GetViewport().Height);
			
			cleanuptimer = new Sce.PlayStation.HighLevel.GameEngine2D.Base.Timer();
			cleanuptimer.Reset();
			timer = new Sce.PlayStation.HighLevel.GameEngine2D.Base.Timer();
			timer.Reset();
			timer1 = new Sce.PlayStation.HighLevel.GameEngine2D.Base.Timer();
			counter = 0;
			FPS=0;
			firable = true;

			
			rand = new Random();
			uppercorner  = new Vector2(scene.Camera.CalcBounds().Center.X-460,scene.Camera.CalcBounds().Center.Y+220);
			var width = Director.Instance.GL.Context.GetViewport().Width;
	   		var height = Director.Instance.GL.Context.GetViewport().Height;
			//init score
			img = new Image(ImageMode.Rgba, new ImageSize(width/3,height/10),
                         new ImageColor(255,255,0,0));
   			img.DrawText("Score: "+score.ToString()+"\nHighscore: "+highscore.ToString(), 
                new ImageColor(255,0,0,255),
                new Font(FontAlias.System,20,FontStyle.Regular),
                new ImagePosition(0,0));
  
  			texturet = new Texture2D(width/3,height/10,false,
                                     PixelFormat.Rgba);
			
   			texturet.SetPixels(0,img.ToBuffer());
   			img.Dispose();  
			
			
			
			dead = new List<SimpleDead>();
			for(int x = 0; x < 20; x++){
			
				dead.Add(new SimpleDead(ex,player.position,player.bounds));
				
			}
			
			shots = new List<ShotClass>();
			for(int x = 0; x < 100; x++){
			
				shots.Add(new ShotClass(player.position,Input2.GamePad0.AnalogRight,player.bounds));
				
			}
			
			
			enemies = new List<EnemyClass>();
			for(int x = 0; x < 30; x++){
			
				enemies.Add(new SimpleEnemy(player.bounds));
				
			}
			
			
			oThread = new Thread(new ThreadStart(Updating1));
			oThread.Start();
			pThread= new Thread( new ThreadStart(Updating2));
			pThread.Start();
	   	}
		
		public static void Updating1(){
			while(true)
				Update1();
			
		}
		public static void Updating2(){
			while(true)
				Update2();
		}
		
		public void Render(){
			
			if(alpha<.05)
				alpha=.05f;
			if(alpha>1)
				alpha=1;
			
			player.Move(Input2.GamePad0.AnalogLeft.X,Input2.GamePad0.AnalogLeft.Y,Input2.GamePad0.AnalogRight);
				
			Director.Instance.Update();
			scene.RemoveAllChildren(true);
			
	
			TextureInfo ti1 = new TextureInfo();
			
			if(player.shottype==0)
				ti1.Texture = backGround;
			else
				ti1.Texture = backGround1;
			
	   		SpriteUV sprite1 = new SpriteUV();
	   		sprite1.TextureInfo = ti1;
			
	   
	   		sprite1.Quad.S = ti1.TextureSizef;//.Multiply(.8f);
	   		sprite1.CenterSprite();
	   		sprite1.Position = scene.Camera.CalcBounds().Center;
	   
			sprite1.Color.A =alpha;
			
	   		scene.AddChild(sprite1);
		
			var width = Director.Instance.GL.Context.GetViewport().Width;
	   		var height = Director.Instance.GL.Context.GetViewport().Height;
			
			foreach (ShotClass item in shots){
				if(item.On && item.type==0)
					item.Render( ballTexture, scene);
				
				if(item.On && item.type==1)
					item.Render( ballTexture1, scene);			
			
			}
	
			foreach( EnemyClass enemy in enemies){
				
				if(enemy.isOn()){
					enemy.Update();
					enemy.Render(scene);
				}
				
			}

			
			player.Render(scene,width,height);
			
			for( int i =0; i < dead.Count-1; i++){
			
				if(dead[i].count >= 11){
					dead[i].On=false;
					
				}
				
				if(dead[i].On)
					dead[i].Render(scene);
				
			}
			
	   
//	   		Image img = new Image(ImageMode.Rgba, new ImageSize(width,height),
//	                         new ImageColor(255,0,0,0));
//	   		img.DrawText("Hello World", 
//	                new ImageColor(255,0,0,255),
//	                new Font(FontAlias.System,170,FontStyle.Regular),
//	                new ImagePosition(0,150));
//	  
//	   		Texture2D texture = new Texture2D(width,height,false,
//	                                     PixelFormat.Rgba);
//	   		texture.SetPixels(0,img.ToBuffer());
//	   		img.Dispose();    
//			
//	   		Image img1 = new Image(ImageMode.Rgba, new ImageSize(width/5,height/10),
//	                         new ImageColor(255,0,0,0));
//	   		img1.DrawText(scene.Camera.CalcBounds().Center.ToString(), 
//	                new ImageColor(0,0,255,255),
//	                new Font(FontAlias.System,20,FontStyle.Regular),
//	                new ImagePosition(0,0));
//	  
//	   		Texture2D texture1 = new Texture2D(width/5,height/10,false,
//	                                     PixelFormat.Rgba);
//	   		texture1.SetPixels(0,img1.ToBuffer());
//	   		img1.Dispose();                                  
//	
//			
//	   		TextureInfo ti1 = new TextureInfo();
//	   		ti1.Texture = texture1;
//	   
//	   		SpriteUV sprite1 = new SpriteUV();
//	   		sprite1.TextureInfo = ti1;
//	   
//	   		sprite1.Quad.S = ti1.TextureSizef;
//	   		sprite1.CenterSprite();
//	   		sprite1.Position = new Vector2(100,100);// scene.Camera.CalcBounds().Center;
//	   
//	   		scene.AddChild(sprite1);
//	   
//	   
//	   		TextureInfo ti = new TextureInfo();
//	   		ti.Texture = texture;
//	   
//	   		SpriteUV sprite = new SpriteUV();
//	   		sprite.TextureInfo = ti;
//	   
//	   		sprite.Quad.S = ti.TextureSizef;
//	   		sprite.CenterSprite();
//	   		sprite.Position = new Vector2(100,100);
//	   
	   		//scene.AddChild(sprite);
	   
			if(altimer.Milliseconds() > 800){
				alpha-=.1f;
				altimer.Reset();
			}

					
			counter++;
			
			if(timer1.Milliseconds() >= 1000){
				timer1.Reset();
				FPS=counter;
				counter=0;
			

			img = new Image(ImageMode.Rgba, new ImageSize(width/2,height/10),
                         new ImageColor(255,255,0,0));
   			img.DrawText("Score: "+score.ToString()+"\nHighscore: "+highscore.ToString()+ " " + (FPS).ToString(), 
                new ImageColor(255,0,0,255),
                new Font(FontAlias.System,20,FontStyle.Bold),
                new ImagePosition(0,0));
  
  			texturet = new Texture2D(width/2,height/10,false,
                                     PixelFormat.Rgba);
			
   			texturet.SetPixels(0,img.ToBuffer());
   			img.Dispose();  
			
			}
   
   			TextureInfo ti = new TextureInfo();
   			ti.Texture = texturet;
   
   			SpriteUV sprite = new SpriteUV();
   			sprite.TextureInfo = ti;
   
   			sprite.Quad.S = ti.TextureSizef;
   			//sprite.CenterSprite();
   			sprite.Position = uppercorner;
			scene.AddChild(sprite);
			
			Director.Instance.Render();
			Director.Instance.GL.Context.SwapBuffers();
			Director.Instance.PostSwap();
			
		
		}
		
		static void Reset(){
			
			foreach( EnemyClass enemy in enemies){
				enemy.setOn(false);
			}
					
			if(highscore<score)
				highscore=score;
					
			score=0;
			alpha=.05f;
	
			
		}
		
		public static void Update1(){
		
			for(int e = 0 ; enemies.Count-1 > e; e++){
				
				if(enemies[e].isOn() && player.position.Distance(enemies[e].getPos()) < 20){
				
					Reset();
					
				}
			}
			
			
			if(rand.Next(0,90000) <10){		
				
				foreach( EnemyClass s in enemies){
					
					if(!s.isOn()){
						s.setOn(true);
						
						Vector2 temp= new Vector2(rand.Next(0,960),rand.Next(0,544));
						while(temp.Distance(player.position)<100){
							temp= new Vector2(rand.Next(0,960),rand.Next(0,544));
						}
						s.setPos(temp);
						//s.rand = new System.Random();
						break;
					}
			
				}
			}
			
			if(Input2.GamePad0.L.Down)
				player.shottype=0;
			
			if(Input2.GamePad0.R.Down)
				player.shottype=1;
			
			
			if((Input2.GamePad0.AnalogRight.X != 0 || Input2.GamePad0.AnalogRight.Y != 0) && 
			    (Input2.GamePad0.AnalogRight.Y > 0.8 || Input2.GamePad0.AnalogRight.Y < -0.8 || 
			    Input2.GamePad0.AnalogRight.X < -0.8 || Input2.GamePad0.AnalogRight.X > 0.8)&& firable){
		
				foreach(ShotClass s in shots){
				
					if(!s.On){
						s.position = player.position;
						s.direction = Input2.GamePad0.AnalogRight;
						s.On=true;
						s.type = player.shottype;
						s.rotation = player.RotationAngle;
						break;
					}
				}
				
				timer.Reset();
				firable = false;
			}
			
			if(timer.Milliseconds()>300-200*alpha)
				firable = true;
			
		}
		
		public static void Update2(){
			
			foreach(ShotClass shot in shots){
        	 	shot.Update();                                                 
				if(shot!=null && shot.isOutofBounds()){
				
					shot.On=false;
				
				}
				for (int e = 0; e <= enemies.Count-1; e++){
				
				
					if(shot.On && enemies[e].isOn() && shot.position.Distance(enemies[e].getPos()) < 20){
					
						foreach (SimpleDead d in dead){
							
							if(!d.On){
								d.count=0;
								d.On=true;
								d.setPos(enemies[e].getPos());
								break;								
							}
						}
											
						alpha+=.03f;
						altimer.Reset();
						enemies[e].setOn(false);
						score+=100*(int)(alpha/.05);
						shot.On=false;
						
						
										
					}
					
				}	
				
			}
			
		}
		
	}
}

