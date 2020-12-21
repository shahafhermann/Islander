using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private ArrayList islands;
    private GameObject playerGO;
    private int playerOnIsland;
    private GameObject minimapGO;

    public GameManager()
    {
        islands = new ArrayList();
        playerOnIsland = -1;
    }

    public void setPlayer(GameObject playerGO)
    {
        this.playerGO = playerGO;
        if (islands.Count > 0 && playerOnIsland == -1)
            goNextIsland();
    }

    public void setMinimap(GameObject minimapGO)
    {
        this.minimapGO = minimapGO;
    }

    public void addIsland(Island island)
    {
        islands.Add(island);
        if(playerGO != null && playerOnIsland == -1)
            goNextIsland();
    }

    public void goNextIsland()
    {
        playerOnIsland = (playerOnIsland + 1) % islands.Count;
        playerGO.transform.position = (islands[playerOnIsland] as Island).getIslandGO().transform.position;
    }

    public Bounds getCurrentIslandBounds()
    {
        return (islands[playerOnIsland] as Island).getBounds();
    }

    public ArrayList getIslands()
    {
        return islands;
    }
}
