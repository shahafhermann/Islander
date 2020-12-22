using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private ArrayList islands;
    private GameObject playerGO;
    private int playerOnIsland;
    private float timeGameStarted;

    public GameManager()
    {
        islands = new ArrayList();
        playerOnIsland = -1;
        timeGameStarted = -1;
    }

    public float getTimeStarted()
    {
        if (timeGameStarted == -1)
            timeGameStarted = Time.time;

        return timeGameStarted;
    }

    public void setPlayer(GameObject playerGO)
    {
        this.playerGO = playerGO;
        if (islands.Count > 0 && playerOnIsland == -1)
            goNextIsland();
    }

    public void addIsland(Island newIsland)
    {
        bool added = false;
        for(int i=0; i<islands.Count; i++)
        {
            MinimapScript minimap = (islands[i] as Island).getMinimap();
            if (minimap != null && newIsland.getIslandGO().name.Equals(minimap.gameObject.name))
            {
                added = true;
                newIsland.setMinimap(minimap);
                islands[i] = newIsland;
                minimap.setIsland(islands[i] as Island);
            }
        }
        if(!added)
            islands.Add(newIsland);

        if(playerGO != null && playerOnIsland == -1)
            goNextIsland();
    }

    public void addMinimap(MinimapScript minimap)
    {
        foreach (Island island in islands)
        {
            if (island.getIslandGO() != null && island.getIslandGO().name.Equals(minimap.name))
            {
                island.setMinimap(minimap);
                minimap.setIsland(island);
                return;
            }
        }

        islands.Add(new Island(null, new Bounds(), null, minimap));
    }

    public void changePlayerToIsland(int islandIdx)
    {
        playerOnIsland = islandIdx;
        playerGO.transform.position = (islands[playerOnIsland] as Island).getIslandGO().transform.position;
    }

    public void goNextIsland()
    {
        changePlayerToIsland((playerOnIsland + 1) % islands.Count);
    }

    public ArrayList getIslands()
    {
        return islands;
    }
}
