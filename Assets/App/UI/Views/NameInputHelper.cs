using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using xyz._8bITProject.cooperace.persistence;

public class NameInputHelper : MonoBehaviour {
	const string VALID_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
	int[] charIndices;
	public Text[] textUI;
	private const string filename = "playerName.txt";


	void Start() {
		charIndices = new int[3];

		// read in existing name
		string name = PersistentStorage.Read(filename);
		 
		// if it's valid name
		if (name.Length >= 3) {
			for (int i = 0; i < 3; i++) {
				if (VALID_CHARS.IndexOf (name [i]) == -1) {
					charIndices [i] = 0;
				} else {
					charIndices [i] = VALID_CHARS.IndexOf (name [i]);
				}
			}
		}

		UpdateTextUI ();


	}

	public void Increment(int index) {
		charIndices [index]++;
		wrapAroundIndex (index);
		UpdateTextUI ();
	}

	public void Decrement(int index) {
		charIndices [index]--;
		wrapAroundIndex (index);
		UpdateTextUI ();
	}

	/// <summary>
	/// Go around all valid characters. 
	/// When the last character is reached, it starts from the beginning.
	/// </summary>
	/// <param name="index">index</param>
	void wrapAroundIndex(int index) {
		if (charIndices [index] >= VALID_CHARS.Length) {
			charIndices [index] = 0;
		} else if (charIndices[index] < 0) {
			charIndices [index] = VALID_CHARS.Length - 1;
		}
	}

	/// <summary>
	/// Convert 3 text UI elements to a string
	/// </summary>
	/// <returns>name</returns>
	public string GetName() {

		return textUI[0].text + textUI[1].text + textUI[2].text;

	}

	/// <summary>
	/// Updates the text UI to the most recent update
	/// </summary>
	public void UpdateTextUI() {
		textUI[0].text = VALID_CHARS[charIndices[0]].ToString();
		textUI[1].text = VALID_CHARS[charIndices[1]].ToString();
		textUI[2].text = VALID_CHARS[charIndices[2]].ToString();
	}

	/// <summary>
	/// Save the file, and go back to mainmenu
	/// </summary>
	public void ConfirmButton() {
		PersistentStorage.Write (filename, GetName ());
	}
}
