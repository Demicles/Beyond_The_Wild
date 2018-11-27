using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnter : MonoBehaviour {

    public string transitionName;

    // Use this for initialization
    void Start () {
        if (transitionName == PlayerController.instance.areaTransitionName)
        {
            PlayerController.instance.transform.position = transform.position;
        }

        UIFade.instance.FadeOut();
        GameManager.instance.fadingBetweenAreas = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
