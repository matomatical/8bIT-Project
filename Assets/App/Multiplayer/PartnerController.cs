using UnityEngine;
using System.Collections;

public class PartnerController : MonoBehaviour {
    // Holds all the player Sprites
    public Sprite[] partnerSprites;
	
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetPartnerNumber(int playerNum)
    {
        GetComponent<SpriteRenderer>().sprite = partnerSprites[playerNum - 1];
    }

    public void SetPartnerInformation(float posX, float posY, float velX, float velY)
    {
        transform.position = new Vector3(posX, posY, 0);
        // Not doing anything with velocity.... for now :)
    }
}
