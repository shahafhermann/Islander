using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Malfunction
{
    public GameObject malfunctionIsland;
    public GameObject malfunctionObject;
    public GameObject fixIsland;
    public GameObject fixObject;
    public Sprite malfunctionImage;
    public Sprite fixImage;
    public float startedAt; // when the malfunction started
    //public float totalTime; // how long until malfunction can no longer be repaired
    private bool originIslandDrowned;  // Will be true if the island has drowned.

    public void setDrowned() {
        originIslandDrowned = true;
    }

    public bool isOriginIslandDrowned() {
        return originIslandDrowned;
    }
}
