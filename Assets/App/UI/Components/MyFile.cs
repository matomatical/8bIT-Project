using System.IO;
using UnityEngine;
using System.Collections;

namespace xyz._8bITProject.cooperace.leaderboard {
	class MyFile : MonoBehaviour {
		void Start() {
			Write ("testFile.txt", "Hello Woorld");

			Read("testFile.txt");

		}

		// Write a text/string into folder 'Application.persistantDataPath', which is 
		// a public directory on IOS/Android device
		static void Write(string filename, string text) {
			StreamWriter w;
			FileInfo f;
			f = new FileInfo (Application.persistentDataPath + "//" + filename);

			if (!f.Exists) {
				w = f.CreateText ();
			} else {
				f.Delete ();
				w = f.CreateText ();
			}
			w.WriteLine (text);
			w.Close ();
		}

		// Read text contents from a file
		static void Read(string filename) {
			StreamReader r = File.OpenText(Application.persistentDataPath + "//" + "testFile.txt");
			string data = r.ReadToEnd ();
			r.Close ();
		}
	}
}