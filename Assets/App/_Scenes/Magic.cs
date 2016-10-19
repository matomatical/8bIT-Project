using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace {
	
	public class Magic {

		public class Scenes {
			public static string MAIN_MENU  = "Main GUI";
			public static string POSTGAME   = "PostGameMenu";
			public static string GAME_SCENE = "Game Scene";
		}

		public class Tags {
			public static string PLAYER = "Player";
			public static string GROUND = "Ground";
		}

		public class LayerNames {
			public static string COLLISION = "Collision";
			public static string OBSTACLE  = "Obstacle";
			public static string PLAYER    = "Player";
		}

		public class Layers {
			public static LayerMask COLLISION = 256;  // layer 8
			public static LayerMask OBSTACLE  = 512;  // layer 9
			public static LayerMask PLAYER    = 1024; // layer 10
		}

		public class SortingLayers {
			public static string BACKGROUND	= "Background";
			public static string BEFORE_BGR	= "Before Background";
			public static string DEFAULT	= "Default";
			public static string FOREGROUND	= "Foreground";
			public static string HUD		= "HUD";
		}
	}
}