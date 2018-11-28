using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public string[] questMarkerNames;
    public bool[] questMarkerCompleted;

    public static QuestManager instance;

	// Use this for initialization
	void Start () {
        instance = this;

        questMarkerCompleted = new bool[questMarkerNames.Length];
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(CheckQuestComplete("Quest test"));
            MarkQuestComplete("Quest test");
            MarkQuestIncomplete("Fight the trolls");
        }
	}

    public int GetQuestNumber(string quest)
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            if (questMarkerNames[i] == quest)
            {
                return i;
            }
        }

        Debug.LogError("Quest " + quest + " does not exist.");
        return 0;
    }

    public bool CheckQuestComplete(string quest)
    {

        int questNumber = GetQuestNumber(quest);

        if(questNumber != 0)
        {
            return questMarkerCompleted[questNumber];
        }

        return false;
    }

    public void MarkQuestComplete(string quest)
    {
        questMarkerCompleted[GetQuestNumber(quest)] = true;
        UpdateLocalQuestObjects();
    }

    public void MarkQuestIncomplete(string quest)
    {
        questMarkerCompleted[GetQuestNumber(quest)] = false;
        UpdateLocalQuestObjects();
    }

    public void UpdateLocalQuestObjects()
    {
        QuestObjectActivator[] questObj = FindObjectsOfType<QuestObjectActivator>();

        if(questObj.Length > 0)
        {
            for(int i = 0; i < questObj.Length; i++)
            {
                questObj[i].CheckCompletion();
            }
        }
    }

}
