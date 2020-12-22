using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    private SpriteRenderer floodRenderer;
    private Island island;
    // Start is called before the first frame update
    void Start()
    {
        foreach(SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            if (renderer.tag.Equals("Flood"))
            {
                floodRenderer = renderer;
                break;
            }
        }
        GameManager.Instance.addMinimap(this);
    }

    // Update is called once per frame
    void Update()
    {
        Color color = floodRenderer.color;
        color.a = (Time.time - GameManager.Instance.getTimeStarted()) / island.getTimeToDrown();
        floodRenderer.color = color;
    }

    public void setIsland(Island island)
    {
        this.island = island;
    }
}
