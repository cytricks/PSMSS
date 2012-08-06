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
	interface EnemyClass
	{
		
		void Update();
		void Render(Scene scene);
		Vector2 getPos();
		void setPos(Vector2 v);
		void setX(float x);
		int getframeofdeath();
		void setTexture(Texture2D tex, float mult);
		bool isOutofBounds();
		bool isOn();
		void setOn(bool on);
		
	}
}

