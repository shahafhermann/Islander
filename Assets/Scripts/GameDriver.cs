using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct Malfunction
{
    public GameObject malfunctionIsland;
    public GameObject malfunctionObject;
    public GameObject fixIsland;
    public GameObject fixObject;
    // public bool isPickupFix;  // Not needed after all, using object tags instead.
    public Sprite malfunctionImage;
    public Sprite fixImage;

    public float startedAt; // when the malfunction started
    //public float totalTime; // how long until malfunction can no longer be repaired
}

public class GameDriver : MonoBehaviour
{
    public List<Malfunction> malfunctionsList;
    
    private MalfunctionFactory malfunctionFactory;
    [HideInInspector] public Malfunction curMalfunction;

    public Image malfunctionWayPoint;
    private RectTransform _wayPointRectTransform;
    public Vector3 malfunctionWayPointOffset;

    public float timeGameStarted; // time when the game actually started

    private bool temp = true; // TODO delete

    void Start()
    {
        malfunctionFactory = new MalfunctionFactory(malfunctionsList);
        curMalfunction = malfunctionFactory.generateMalfunction();
        curMalfunction.startedAt = Time.time;
        _wayPointRectTransform = malfunctionWayPoint.GetComponent<RectTransform>();
        timeGameStarted = -1;

        GameManager.Instance.setGameDriver(this); // maybe not needed, set to game manager so it can be used staticly (GameManager.Instance...)
    }

    private void Update() {
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
        showWaypoints();
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
            
            
            // Generate new malfunction:
            curMalfunction = malfunctionFactory.generateMalfunction();
            curMalfunction.startedAt = Time.time;
            temp = true; // TODO delete
        }
        else {  // Failed to fix
            // Tell the player that he's wrong
            // Steps to make the flooding worse
        }
    }
}
