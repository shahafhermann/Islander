using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDriver : MonoBehaviour
{
    // These arrays are defined through the GameDriver inspector, and should be filled "in parallel",
    // so index 'i' in all of them refers to the same thing.
    public List<GameObject> malfunctionObjects;
    public List<GameObject> fixObjects;
    public bool[] pickup;
    
    void Start()
    {
        GameManager.Instance.malfunctionFactory = new Malfunction(malfunctionObjects, fixObjects, pickup);
    }
}
