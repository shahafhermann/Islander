using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private GameObject objectInScene;
    private GameObject island;

    public Item(GameObject objectInScene, GameObject island) {
        this.objectInScene = objectInScene;
        this.island = island;
    }

    public GameObject getObject() {
        return objectInScene;
    }

    public GameObject getIsland() {
        return island;
    }
}
