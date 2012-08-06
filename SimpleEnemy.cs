using System;
using System.Collections.Generic;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core.Imaging;

namespace SpaceShooter
{
	public class SimpleEnemy : EnemyClass
	{
		public Vector2 position;
		public Vector2 direction;
		Vector2 bounds;
		float rotationAngle;
		float multipier = 0.2f;
		public Texture2D texture;
		public static System.Random rand = new System.Random();
		Vector2 CompareVector;
		int frameofdeath = 0;
		public bool On = false;
		
		Timer timer = new Timer();
		
		public bool isOn(){
			return On;
		}
		
		public void setOn(bool on){
			On=on;
		}
		
		public void setPos(Vector2 v){
			position=v;
		}
		
		public SimpleEnemy (Vector2 bond)
		{
			//rand;
			
			
			
			position = new Vector2(rand.Next(0,960),rand.Next(0,544));
			
			direction = new Vector2( rand.Next(-101,101)/100,rand.Next(-101,101)/100);
			
			bounds = new Vector2(bond.X,bond.Y);
			
			if(texture==null)
				texture = new Texture2D("/Application/textures/tri.png", false);
			
			CompareVector = new Vector2(0,-1);
			rotationAngle = CompareVector.Angle(direction);
			
			
						
		}
		
		public void setTexture(Texture2D tex, float mult){
		
			texture = tex;
			multipier = mult;
			
		}
		
		public int getframeofdeath(){
			
			if(timer.Milliseconds() > 100){
				timer.Reset();
				frameofdeath++;
			}
			return frameofdeath-1;
		}
		
		public Vector2 getPos(){
			return position;
		}
		
		public void setX(float x){
			position.X=x;
		}
		
		public void Update(){
			
			position.X+=direction.X*4;
			position.Y+=direction.Y*4;	
			
			if(position.Y < 0)
				position.Y=0;
			
			if(position.X < 0)
				position.X=0;
			
			if(position.Y > bounds.Y)
				position.Y=bounds.Y;
			
			if(position.X > bounds.X)
				position.X=bounds.X;
			
			//Console.WriteLine(rand.Next(-100,100).ToString());
			
			if(rand.Next(0,100) < 10){
				
				 
				
				direction.X = rand.Next(-100,100)/100f;
				direction.Y = rand.Next(-100,100)/100f;
				
				direction.Y *=-1;
				
				rotationAngle = CompareVector.Angle(direction);
				direction.Y *=-1;
			}
			
			
		}
		public bool isOutofBounds(){
		
			if(position.X<0 | position.Y < 0| position.X > bounds.X | position.Y > bounds.Y)
				return true;
			else
				return false;
		}
		
		public void Render(Scene scene){
			
			TextureInfo ti1 = new TextureInfo();
			
			
			ti1.Texture = texture;
		
	   		SpriteUV sprite1 = new SpriteUV();
	   		sprite1.TextureInfo = ti1;
	   
	   		sprite1.Quad.S = ti1.TextureSizef.Multiply(multipier);
	   		sprite1.CenterSprite();
	   		sprite1.Position = position;// scene.Camera.CalcBounds().Center;
	   		
	   		
			
			sprite1.Rotate(-rotationAngle);//*Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Pi/180);
			
	   		scene.AddChild(sprite1);
			
		}
		
		
		
	}
}

