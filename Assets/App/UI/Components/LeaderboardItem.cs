using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeaderboardItem : MonoBehaviour {

	public Text player1Text;
	public Text player2Text;
	public Text scoreText;

	public string player1 {
		get {
			return player1Text.text;
		}
		set {
			player1Text.text = value;
		}
	}

	public string player2 {
		get {
			return player2Text.text;
		}
		set {
			player2Text.text = value;
		}
	}

	public string score {
		get {
			return scoreText.text;
		}
		set {
			scoreText.text = value;
		}
	}

}
