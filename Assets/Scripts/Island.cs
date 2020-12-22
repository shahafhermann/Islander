using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island
{
    private Bounds islandBounds;
    private ArrayList islandItems;
    private GameObject islandGO;
    private MinimapScript minimapIsland;
    private float timeToDrown;

    public Island(GameObject islandGO, Bounds bounds, ArrayList items, MinimapScript minimap)
    {
        islandBounds = bounds;
        islandItems = items;
        this.islandGO = islandGO;
        this.minimapIsland = minimap;
        // timeToDrown = Random.Range(180, 360);
        timeToDrown = Random.Range(5, 30);
    }

    public void collectItem(Item item)
    {

    }

    public float getTimeToDrown()
    {
        return timeToDrown;
    }

    public GameObject getIslandGO()
    {
        return islandGO;
    }

    public void setMinimap(MinimapScript minimap)
    {
        minimapIsland = minimap;
    }

    public MinimapScript getMinimap()
    {
        return minimapIsland;
    }
}
