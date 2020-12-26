using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct GameObjectMap
{
    public GameObject Item1;
    public GameObject Item2;
}

public class GameDriver : MonoBehaviour
{
    // These arrays are defined through the GameDriver inspector, and should be filled "in parallel",
    // so index 'i' in all of them refers to the same thing.
    public List<GameObjectMap> malfunctionObjects;
    public List<GameObjectMap> fixObjects;
    public bool[] pickup;
    
    private Malfunction malfunctionFactory;
    [HideInInspector] public (Item, Item) curMalfunction;

    public Image malfunctionWayPoint;
    private RectTransform _wayPointRectTransform;
    public Vector3 malfunctionWayPointOffset;

    private bool temp = true; // TODO delete

    void Start()
    {
        malfunctionFactory = new Malfunction(malfunctionObjects, fixObjects, pickup);
        curMalfunction = malfunctionFactory.generateMalfunction();
        _wayPointRectTransform = malfunctionWayPoint.GetComponent<RectTransform>();
    }

    private void Update() {
        if (temp) {  // TODO delete
            Debug.Log(curMalfunction.Item1.getIsland());
            Debug.Log(curMalfunction.Item1.getObject());
            Debug.Log(curMalfunction.Item2.getIsland());
            Debug.Log(curMalfunction.Item2.getObject());
            temp = false;
        }
        showWaypoints();
    }

    private bool isVisible(GameObject go) {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, go.GetComponent<Collider2D>().bounds);
    }
    
    private void showWaypoints() {
        GameObject curMalfunctionIsland = curMalfunction.Item1.getIsland();
        Vector3 islandPos = curMalfunctionIsland.transform.position;
        if (!isVisible(curMalfunctionIsland)) {  // (!curMalfunctionIsland.GetComponent<Renderer>().isVisible) {
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
            temp = true; // TODO delete
        }
        else {  // Failed to fix
            // Tell the player that he's wrong
            // Steps to make the flooding worse
        }
    }
}
