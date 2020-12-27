using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// [Serializable]
// public struct Malfunction
// {
//     public GameObject malfunctionIsland;
//     public GameObject malfunctionObject;
//     public GameObject fixIsland;
//     public GameObject fixObject;
//     // public bool isPickupFix;  // Not needed after all, using object tags instead.
//     public Sprite malfunctionImage;
//     public Sprite fixImage;
//     public float startedAt; // when the malfunction started
//     //public float totalTime; // how long until malfunction can no longer be repaired
//     [HideInInspector] public bool originIslandDrowned;  // Will be true if the island has drowned.
// }

public class GameDriver : MonoBehaviour
{
    public List<Island> islands;
    public List<Malfunction> malfunctionsList;
    private MalfunctionFactory malfunctionFactory;
    [HideInInspector] public Malfunction curMalfunction;
    private Island curIsland;

    public int maxDrownedAllowed = 3;
    private int curDrowned = 0;

    public Image malfunctionWayPoint;
    private RectTransform _wayPointRectTransform;
    public Vector3 malfunctionWayPointOffset;

    public float timeGameStarted; // time when the game actually started

    private bool temp = true; // TODO delete

    void Start()
    {
        malfunctionFactory = new MalfunctionFactory(malfunctionsList);
        newMalfunction();
        _wayPointRectTransform = malfunctionWayPoint.GetComponent<RectTransform>();
        timeGameStarted = -1;

        GameManager.Instance.setGameDriver(this); // maybe not needed, set to game manager so it can be used staticly (GameManager.Instance...)
        
        InvokeRepeating(nameof(sinkIsland), 0f, 0.1f);
    }

    private void Update() {
        if (curDrowned == maxDrownedAllowed) {
            // TODO end game
        }
        
        // set start time on first update when game actually starts
        if(timeGameStarted == -1)
        {
            timeGameStarted = Time.time;
        }

        if (temp) {  // TODO delete
            Debug.Log(curMalfunction.malfunctionIsland);
            Debug.Log(curMalfunction.malfunctionObject);
            Debug.Log(curMalfunction.fixIsland);
            Debug.Log(curMalfunction.fixObject);
            temp = false;
        }
        
        // TODO insert sink with timer
        
        showWaypoints();
    }

    private void sinkIsland() {
        curIsland.increaseSink();
    }

    private bool isVisible(GameObject go) {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, go.GetComponent<Collider2D>().bounds);
    }
    
    private void showWaypoints() {
        Vector3 islandPos = curMalfunction.malfunctionIsland.transform.position;
        if (!isVisible(curMalfunction.malfunctionIsland)) {
            if (!malfunctionWayPoint.enabled) {
                malfunctionWayPoint.enabled = true;
            }
            
            float minX = malfunctionWayPoint.GetPixelAdjustedRect().width / 2;
            float maxX = Screen.width - minX;
            float minY = malfunctionWayPoint.GetPixelAdjustedRect().height / 2;
            float maxY = Screen.height - minY;
            
            Vector2 pos = Camera.main.WorldToScreenPoint(islandPos + malfunctionWayPointOffset);
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            malfunctionWayPoint.transform.position = pos;
            
            // Angle
            Vector3 dir = (islandPos - Camera.main.transform.position);
            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
            _wayPointRectTransform.localEulerAngles = new Vector3(0, 0, angle);
        }
        else if (malfunctionWayPoint.enabled) {
            malfunctionWayPoint.enabled = false;
        }
    }

    public void solve(bool success) {
        if (success) {
            // Steps to reduce flooding go here:
            curIsland.reduceSink(0.25f);
            
            // Generate new malfunction:
            newMalfunction();
        }
        else {  // Failed to fix
            // Tell the player that he's wrong
            // Make the flooding worse
            curIsland.increaseSink(0.25f);
            // If the island has drowned, count it as a strike, block the island and generate a new malfunction.
            if (curIsland.isDrowned()) {
                curDrowned++;
                // TODO maybe give a global timer penalty?
                newMalfunction();
            }
        }
    }

    private void newMalfunction() {
        curMalfunction = malfunctionFactory.generateMalfunction();
        curMalfunction.startedAt = Time.time;
        setCurrentIsland();
        temp = true; // TODO delete
    }

    private void setCurrentIsland() {
        foreach (var island in islands) {
            if (island.getIsland().Equals(curMalfunction.malfunctionIsland)) {
                curIsland = island;
                break;
            }
        }
    }
}
