using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island
{
    private Bounds islandBounds;
    private ArrayList islandItems;
    private GameObject islandGO;
    private GameObject minimapIsland;

    public Island(GameObject islandGO, GameObject minimapIsland, Bounds bounds, ArrayList items)
    {
        islandBounds = bounds;
        islandItems = items;
        this.islandGO = islandGO;
        this.minimapIsland = minimapIsland;
    }

    public bool isInBounds(Bounds bound)
    {
        if (islandBounds.Intersects(bound))
        {
            return false;
        }
        return true;
    }

    // TODO: currently only 1 bound (add more?)
    public Bounds getBounds()
    {
        return islandBounds;
    }

    public void collectItem(Item item)
    {

    }

    public GameObject getIslandGO()
    {
        return islandGO;
    }
}
