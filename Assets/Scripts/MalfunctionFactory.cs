using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MalfunctionFactory {
    
    private List<Malfunction> malfunctions;
    private int lastMalfunction = -1;
    
    public MalfunctionFactory(List<Malfunction> malfunctions) {
        this.malfunctions = malfunctions;
    }

    public Malfunction generateMalfunction() {
        int indexOfMalfunctionToReturn;
        do {
            indexOfMalfunctionToReturn = Random.Range(0, malfunctions.Count);
        } while (indexOfMalfunctionToReturn == lastMalfunction || 
                 malfunctions[indexOfMalfunctionToReturn].isOriginIslandDrowned());

        lastMalfunction = indexOfMalfunctionToReturn;
        return malfunctions[indexOfMalfunctionToReturn];
    }
}
