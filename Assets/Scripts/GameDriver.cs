using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    [HideInInspector]
    private Malfunction malfunctionFactory;

    void Start()
    {
        malfunctionFactory = new Malfunction(malfunctionObjects, fixObjects, pickup);
        GameManager.Instance.malfunction = malfunctionFactory.generateMalfunction();
        // TODO: indicate malfunction on screen
    }

    public void solve(bool success) {
        if (success) {
            // Steps to reduce flooding go here:
            
            
            // Generate new malfunction:
            GameManager.Instance.malfunction = malfunctionFactory.generateMalfunction();
            // TODO: indicate malfunction on screen
        }
        else {  // Failed to fix
            // Tell the player that he's wrong
            // Steps to make the flooding worse
        }
    }
}
