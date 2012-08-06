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
	public class SimpleDead
	{
		List<Texture2D> list;
		public static Vector2 position;
		public int count;
		Timer timer = new Timer();
		Vector2 bounds;
		public bool On = false;
		
		public void setPos(Vector2 pos){
		
			position=pos;
			
		}
		
		public SimpleDead (List<Texture2D> l, Vector2 pos, Vector2 bon)
		{
			count = 0;
			position = pos;
			list = l;
			bounds = bon;
		
			
			
		}
		
		public bool isOutofBounds(){
		
			if(position.X<0 | position.Y < 0| position.X > bounds.X | position.Y > bounds.Y)
				return true;
			else
				return false;
		}
		 
		public void Render(Scene scene){
			
			if(timer.Milliseconds() > 25){
				count++;
				timer.Reset();
			}
			
			
			
			TextureInfo ti1 = new TextureInfo();
			
			if(count > 11)
				count=11;
			
			ti1.Texture = list[count];
		
	   		SpriteUV sprite1 = new SpriteUV();
	   		sprite1.TextureInfo = ti1;
	   
	   		sprite1.Quad.S = ti1.TextureSizef.Multiply(0.7f);
	   		sprite1.CenterSprite();
	   		sprite1.Position = position;// scene.Camera.CalcBounds().Center;
	   		
	   		scene.AddChild(sprite1);
		}
	}
}

