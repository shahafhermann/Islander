using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float perc;
    private float timeGameStarted;
    private GameDriver gameDriver;

    public GameManager()
    {
        timeGameStarted = -1;
    }

    public void setGameDriver(GameDriver gameDriver)
    {
        this.gameDriver = gameDriver;
    }

    public float getTimeStarted()
    {
        if (timeGameStarted == -1)
            timeGameStarted = Time.time;

        return timeGameStarted;
    }

    public float getIslandSinkPercentage()
    {
        return perc;
    }
}