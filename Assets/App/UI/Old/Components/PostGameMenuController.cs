using UnityEngine;
using System.Collections;
using GooglePlayGames;
using xyz._8bITProject.cooperace.persistence;

namespace xyz._8bITProject.cooperace.ui {
	public class PostGameMenuController : MonoBehaviour {


		public void SaveRecording(){
			if(SceneManager.newRecording!=null){
				PersistentStorage.Write("Recording/"+SceneManager.levelToLoad+".crr", SceneManager.newRecording);
			}
		}
	}	
}
