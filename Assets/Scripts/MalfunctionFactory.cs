using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MalfunctionFactory {
    
    private List<Malfunction> malfunctions;
    private int lastMalfunction = -1;
    
    public MalfunctionFactory(List<Malfunction> malfunctionsList) {
        malfunctions = malfunctionsList;
    }

    public Malfunction generateMalfunction() {
        int indexOfMalfunctionToReturn;
        do {
            indexOfMalfunctionToReturn = Random.Range(0, malfunctions.Count);
        } while (indexOfMalfunctionToReturn == lastMalfunction);

        lastMalfunction = indexOfMalfunctionToReturn;
        return malfunctions[indexOfMalfunctionToReturn];
    }
}
