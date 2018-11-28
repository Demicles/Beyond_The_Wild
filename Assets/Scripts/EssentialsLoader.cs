using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour {

    public GameObject gameManag; // Containings stats
    public GameObject UIScreen; // Main UI; dialog
    public GameObject player; // Our player

    // Use this for initialization
    void Start () {
        if (GameManager.instance == null)
        {
            Instantiate(gameManag);
        }

        if (UIFade.instance == null)
        {
            Instantiate(UIScreen);
        }

        if (PlayerController.instance == null)
        {
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.instance = clone;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
