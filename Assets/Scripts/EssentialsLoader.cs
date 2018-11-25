using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour {

    public GameObject UIScreen; // Main UI; dialog
    public GameObject player; // Our player
    public GameObject gameManag; // Containings stats

    // Use this for initialization
    void Start () {
        if (UIFade.instance == null)
        {
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }

        if (PlayerController.instance == null)
        {
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.instance = clone;
        }

        if (GameManager.instance == null)
        {
            Instantiate(gameManag);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
