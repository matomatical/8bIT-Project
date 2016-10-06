using System.IO;
using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.leaderboard {
	class MyFile : MonoBehaviour {
		void Start() {
			StreamWriter w;
			FileInfo f;
			f = new FileInfo (Application.persistentDataPath + "//" + "testFile.txt");

//			string aString[] = "Hello World!";
//			File.WriteAllLines(Application.persistentDataPath, aString);

			if (!f.Exists) {
				w = f.CreateText ();
			} else {
				f.Delete ();
				w = f.CreateText ();
			}
			w.WriteLine ("Hello World");
			w.Close ();
		}
	}
}