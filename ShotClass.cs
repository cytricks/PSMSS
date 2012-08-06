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
	public class ShotClass
	{
		public Vector2 position;
		public Vector2 direction;
		public Vector2 bounds;
		public bool On = false;
		public int type = 0;
		public float rotation;
		

		
		public ShotClass (Vector2 pos, Vector2 dir, Vector2 bond)
		{
			position = new Vector2(pos.X,pos.Y);
			
			direction = new Vector2( dir.X,-dir.Y);
			
			bounds = new Vector2(bond.X,bond.Y);
			
		
		
			
		}
		
		public void Update(){
		
			if(!direction.IsUnit(1))
				direction=direction/direction.Length();
			
			position.X+=direction.X*.4f;
			position.Y+=direction.Y*-.4f;
		
	
			
			
		}
		
		
		
		public bool isOutofBounds(){
		
			if(position.X<0 | position.Y < 0| position.X > bounds.X | position.Y > bounds.Y)
				return true;
			else
				return false;
		}
		
		public void Render( Texture2D texture, Scene scene){
		
			TextureInfo ti1 = new TextureInfo();
			
			//if(flip)
				ti1.Texture = texture;
		//	else
		//		ti1.Texture = texture1;
			
	   		SpriteUV sprite1 = new SpriteUV();
	   		sprite1.TextureInfo = ti1;
	   
	   		if(type==0)
			sprite1.Quad.S = ti1.TextureSizef.Multiply(.3f);
			
			if(type==1)
			sprite1.Quad.S = ti1.TextureSizef.Multiply(.05f);
			
	   		sprite1.CenterSprite();
	   		sprite1.Position = position;// scene.Camera.CalcBounds().Center;
	   		
	   		
			
			sprite1.Rotate(-rotation);//*Sce.PlayStation.HighLevel.GameEngine2D.Base.Math.Pi/180);
			
	   		scene.AddChild(sprite1);
			
		}
		
	}
}

