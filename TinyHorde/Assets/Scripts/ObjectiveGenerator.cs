using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveGenerator : MonoBehaviour
{
    private int objectivesToSpawn;
    int objectiveToSpawn;

    public Text[] objectiveTexts;
    public int objectiveInt;
    public int objectivePayout;

    public int runnerKills;
    public int pistolKills;

    // Start is called before the first frame update
    void Start()
    {
        objectivesToSpawn = DataHolder.objectivesAmount;
        DataHolder.runnerKills = 0;
        DataHolder.pistolKills = 0;
        DataHolder.hordeSizeGoal = 0;

        for (int i = 0; i < objectivesToSpawn; i++)
        {
            GenerateObjective(i);
        }
    }

    void GenerateObjective(int objectiveNumber)
    {
        if (objectiveNumber == 0)
        {
            objectiveInt = Random.Range(25, 50);
            DataHolder.hordeSizeGoal = objectiveInt;
            objectivePayout = Random.Range(25, 50);
            DataHolder.hordeSizePayout = objectivePayout;
            objectiveTexts[0].text = ("Reach a Horde Size of " + objectiveInt.ToString() + " for " + objectivePayout.ToString() + " cash.");
        }

        if (objectiveNumber == 1)
        {
            objectiveInt = Random.Range(10, 50);
            DataHolder.runnerKillGoal = objectiveInt;
            objectivePayout = Random.Range(10, 50);
            DataHolder.runnerKillPayout = objectivePayout;
            objectiveTexts[1].text = ("Kill " + objectiveInt.ToString() + " running humans for " + objectivePayout.ToString() + " cash.");
        }

        if (objectiveNumber == 2)
        {
            objectiveInt = Random.Range(10, 20);
            DataHolder.pistolKillGoal = objectiveInt;
            objectivePayout = Random.Range(20, 40);
            DataHolder.pistolKillPayout = objectivePayout;
            objectiveTexts[2].text = ("Kill " + objectiveInt.ToString() + " pistol humans for " + objectivePayout.ToString() + " cash.");
        }
    }

}
