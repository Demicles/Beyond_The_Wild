using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour {

    public string areaToLoad;
    public string areaTransitionName;

    public AreaEnter theEntrance;

    public float waitToLoad = 1f;
    private bool shouldLoadAfterWait;

    // Use this for initialization
    void Start () {
        theEntrance.transitionName = areaTransitionName;
    }
	
	// Update is called once per frame
	void Update () {
        if (shouldLoadAfterWait)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0)
            {
                shouldLoadAfterWait = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shouldLoadAfterWait = true;
            GameManager.instance.fadingBetweenAreas = true;

            UIFade.instance.FadeIn();
            PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }

}
