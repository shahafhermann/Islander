using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

[Serializable]
public class Island {
    
    public GameObject islandObject;
    public Water2DScript waterSurface;
    public GameObject gates;
    [Range(1f, 4f)]
    public float maxSink = 2f;
    [Range(0.1f, 1f)]
    public float sinkSpeed;
    private float sinkPercentage = 0;
    
    private bool drowned = false;
    private List<Malfunction> malfunctions = new List<Malfunction>();

    public GameObject getIsland() {
        return islandObject;
    }

    public float getSinkPercentage() {
        return sinkPercentage;
    }
    
    /**
     * Overloaded method, if no argument is given use the default island's sink speed.
     */
    public void increaseSink() {
        if (sinkPercentage < 1 - sinkSpeed / 1000) {
            sinkPercentage += sinkSpeed / 1000;
        }
        else {  // Increased to 100%
            sinkPercentage = 1f;
            drownIsland();
        }
        waterSurface.testSinkPercentage = sinkPercentage * maxSink;
    }

    /**
     * Overloaded method, sink by the given amount.
     */
    public void increaseSink(float amount) {
        if (sinkPercentage < 1 - amount) {
            sinkPercentage += amount;
        }
        else { // Increased to 100%
            sinkPercentage = 1f;
            drownIsland();
        }
        waterSurface.testSinkPercentage = sinkPercentage * maxSink;
    }

    public void reduceSink(float amount) {
        sinkPercentage -= sinkPercentage > amount ? amount : sinkPercentage;
        waterSurface.testSinkPercentage = sinkPercentage * maxSink;
    }

    private void drownIsland() {
        drowned = true;
        foreach (var malfunction in malfunctions) {
            malfunction.setDrowned();
        }
        gates.SetActive(true);
    }

    public bool isDrowned() {
        return drowned;
    }

    public void addMalfunction(Malfunction malfunction) {
        malfunctions.Add(malfunction);
    }
}
