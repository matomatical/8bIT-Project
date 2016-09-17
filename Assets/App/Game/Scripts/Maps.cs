using UnityEngine;

public class Maps {

	public static string[] maps = new string[] {
		"Test_Level_1",
		"Test_Level_2",
		"Test_Level_3"
	};

	public static GameObject Load(string name) {
		return Resources.Load("Maps/" + name) as GameObject;
	}

}