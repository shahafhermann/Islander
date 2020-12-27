using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Island {
    
    public GameObject islandObject;
    public Water2DScript waterSurface;
    [Range(1f, 4f)]
    public float maxSink = 2f;
    [Range(0.1f, 10f)]
    public float sinkSpeed;
    private float sinkPercentage = 0;

    public GameObject getIsland() {
        return islandObject;
    }

    public float getSinkPercentage() {
        return sinkPercentage;
    }

    public void increaseSink() {
        sinkPercentage += (sinkPercentage < 0.75) ? 0.25f : 1 - sinkPercentage;
        waterSurface.testSinkPercentage = sinkPercentage * maxSink;
    }

    public void reduceSink() {
        sinkPercentage -= sinkPercentage > 0.25f ? 0.25f : sinkPercentage;
        waterSurface.testSinkPercentage = sinkPercentage * maxSink;
    }
}
