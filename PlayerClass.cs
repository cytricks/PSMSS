	using System;
	using System.Collections.Generic;

//	using System.Threading;
	using Sce.PlayStation.Core.Graphics;
	using Sce.PlayStation.Core.Environment;
	using Sce.PlayStation.Core.Input;

	using Sce.PlayStation.Core;
	
	using Sce.PlayStation.HighLevel.GameEngine2D;
	using Sce.PlayStation.HighLevel.GameEngine2D.Base;
	
	using Sce.PlayStation.Core.Imaging;
namespace SpaceShooter
{
	public class PlayerClass
	{
		
		public Vector2 position;
		Vector2 directionVector;
		Vector2 temp;
		public float RotationAngle;
		float currentAngle;
		float xSpeed = 5;
		float ySpeed = 5;
		public Vector2 bounds;
		Texture2D shipTexture;
		public int shottype = 0;
	
		public PlayerClass ()
		{
			position.X = 480.0f;
			position.Y = 272.0f;
			shipTexture = new Texture2D("/Application/textures/Ship1.png", false);
		//	shipTexture1 = new Texture2D("/Application/textures/tri.png", false);
		//	shipTexture = new Texture2D("/Application/spaceship.png", false);
		//	shipTexture1 = new Texture2D("/Application/spaceship1.png", false);
			shipTexture.SetWrap(TextureWrapMode.ClampToEdge);
			currentAngle = 0; 
			directionVector = new Vector2(0,-1);
		///	flip=false;
		//	timer = new Timer();
		//	timer.Reset();
			
		}
		
		public void Move( float X, float Y ,Vector2 RightStick){
		
			position.Y-=Y*ySpeed;
			position.X+=X*xSpeed;
			
			if(position.Y < 0)
				position.Y=0;
			
			if(position.X < 0)
				position.X=0;
			
			if(position.Y > bounds.Y)
				position.Y=bounds.Y;
			
			if(position.X > bounds.X)
				position.X=bounds.X;
			
			
			temp = new Vector2(RightStick.X, RightStick.Y);
			
			
			Vector2 CompareVector = new Vector2(0,-1);
			
			if(!directionVector.IsUnit(1)){
				directionVector = directionVector.Divide(directionVector.Length());
			}
			
			if(!RightStick.IsUnit(1)){
				RightStick = RightStick.Divide(RightStick.Length());
			}
			
			directionVector = directionVector.Add(RightStick);
			directionVector = directionVector.Divide(2);
			
			
			
//			if(RightStick.X < .1 && RightStick.X > -.1)
//				RightStick.X = 0;
//			
//			if(RightStick.Y < .1 && RightStick.Y > -.1)
//				RightStick.Y = 0;
			
				
			if(RightStick.X != 0 || RightStick.Y != 0) {
				RotationAngle = CompareVector.Angle(directionVector);//CompareVector.Angle(RightStick);
			}
			else {
				//RotationAngle = 0;
			}
			
			
			
			
//			if(RotationAngle < 0 && currentAngle-RotationAngle >0 )
//				currentAngle -= .1f;
//			else if(RotationAngle < 0)
//				currentAngle += .1f;
//			
//			if(RotationAngle > 0 && currentAngle-RotationAngle <0 )
//				currentAngle += .1f;
//			else if(RotationAngle > 0)
//				currentAngle -= .1f;
			
		}
		
		public void Render(Scene scene, int width, int height){
		
			Image img1 = new Image(ImageMode.Rgba, new ImageSize(width/5,height/10),
	                         new ImageColor(255,0,0,0));
	   		img1.DrawText(RotationAngle.ToString()+" "+currentAngle.ToString(), 
	                new ImageColor(0,0,255,255),
	                new Font(FontAlias.System,20,FontStyle.Regular),
	                new ImagePosition(0,0));
	  
	   		Texture2D texture1 = new Texture2D(width/5,height/10,false,
	                                     PixelFormat.Rgba);
	   		texture1.SetPixels(0,img1.ToBuffer());
	   		img1.Dispose();                                  
	
			
	   		TextureInfo ti1 = new TextureInfo();
			
//			if(timer.Milliseconds()>100){
//				flip = !flip;
//				timer.Reset();
//			}
			
			
	  		//if(flip) {
				ti1.Texture = shipTexture;
				//flip= !flip;
		//	}
		//	else {
			
		//		ti1.Texture = shipTexture1;
				//flip = !flip;
				
		//	}
			
		//	else
		//		ti1.Texture = texture1;
	   
	   		SpriteUV sprite1 = new SpriteUV();
	   		sprite1.TextureInfo = ti1;
	   
	   		sprite1.Quad.S = ti1.TextureSizef.Multiply(.3f);
	   		sprite1.CenterSprite();
	   		sprite1.Position = position;// scene.Camera.CalcBounds().Center;
	   			
			sprite1.Rotate(-RotationAngle);//*Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Pi/180);
			
	   		scene.AddChild(sprite1);
			
		}
		
		public bool isFirable(Vector2 RightStick){
			
			if(Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Rad2Deg(RightStick.Angle(directionVector))<10 && Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Rad2Deg(RightStick.Angle(directionVector))>-10)
				return true;
			else 
				return false;
			
		}
	}
}

