using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapScript : MonoBehaviour
{
    private Image islandRenderer;
    private float blinkTime = 0.3f;
    private float lastBlink;

    private Island island;
    // Start is called before the first frame update
    void Start()
    {
        islandRenderer = GetComponent<Image>();
        // GameManager.Instance.addMinimap(this);
        lastBlink = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float drownedPerc = (Time.time - GameManager.Instance.getTimeStarted()) / island.getTimeToDrown();
        if(drownedPerc >= 0.5f && Time.time - lastBlink > blinkTime) // TODO: and user hasnt visited the island
        {
            lastBlink = Time.time;
            islandRenderer.enabled = !islandRenderer.enabled;
        }
    }

    public void setIsland(Island island)
    {
        this.island = island;
    }
}
