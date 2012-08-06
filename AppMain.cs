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
	public class AppMain
	{
		public static Scene scene;
		public static SurvivalMode survival;
		
		public static void Main (string[] args)
		{
			try{
			Init();
			while(true)
				survival.Render();
	   
			}
			catch(Exception e){
				Console.WriteLine(e.ToString());
			}
//			finally{
//				Main(null);
//			}
		}
		
		public static void Init(){
		
			Director.Initialize();
			scene = new Scene();
	   		scene.Camera.SetViewFromViewport();
			Director.Instance.RunWithScene(scene,true);
			survival = new SurvivalMode(scene);
			
			
		}
		
		
		
	}
}
