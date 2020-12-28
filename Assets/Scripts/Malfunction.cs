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
    public Sprite malfunctionTypeImage;
    private bool _originIslandDrowned;  // Will be true if the island has drowned.

    public void setDrowned() {
        _originIslandDrowned = true;
    }

    public bool isOriginIslandDrowned() {
        return _originIslandDrowned;
    }

    public void fix() {
        malfunctionObject.GetComponent<SpriteRenderer>().sprite = fixImage;
    }
    
    public void breakItem() {
        malfunctionObject.GetComponent<SpriteRenderer>().sprite = malfunctionImage;
    }
}
