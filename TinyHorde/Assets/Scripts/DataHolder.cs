using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHolder
{
    public static int cash = 450;
    public static int zombieHP = 3;
    public static float zombieSpeed = 4.0f;
    public static int objectivesAmount = 1;

    public static int runnerKills;
    public static int pistolKills;
    public static int runnerKillGoal;
    public static int pistolKillGoal;
    public static int runnerKillPayout;
    public static int pistolKillPayout;

    public static int hordeSizePeak;
    public static int hordeSizeGoal;
    public static int hordeSizePayout;


    public static void endGame()
    {
        checkObjectives();
    }

    private static void checkObjectives()
    {
        if (hordeSizePeak >= hordeSizeGoal)
        {
            cash += hordeSizePayout;
        }

        if (runnerKills >= runnerKillGoal)
        {
            cash += runnerKillPayout;
        }

        if (pistolKills >= pistolKillGoal)
        {
            cash += pistolKillPayout;
        }
    }
}
