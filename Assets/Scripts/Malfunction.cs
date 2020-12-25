using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Malfunction {
    
    // {malfunctioning object : {fix object, pickup?}}  :
    // -  If it's not for pickup, then it's just an interaction (such as a button).
    // -  If 'fix object' is null, then 'pickup' should always be false and the interaction should happen with the malfunctioning object itself.
    private Dictionary<Item, Tuple<Item, bool>> malfunctions = 
        new Dictionary<Item, Tuple<Item, bool>>();

    private List<Item> malfunctionsList = new List<Item>();
    private Item lastMalfunction = null;
    
    public Malfunction(List<GameObjectMap> malfunctions, 
                       List<GameObjectMap> fixes, 
                       bool[] pickup) {
        foreach (var malfunction in malfunctions) {
            malfunctionsList.Add(new Item(malfunction.Item1, malfunction.Item2));
        }
        
        List<Item> fixesItems = new List<Item>();
        foreach (var fix in fixes) {
            fixesItems.Add(new Item(fix.Item1, fix.Item2));
        }

        for (int i = 0; i < malfunctions.Count; i++) {
            this.malfunctions.Add(malfunctionsList[i], new Tuple<Item, bool>(fixesItems[i], pickup[i]));
        }
    }

    public (Item, Item) generateMalfunction() {
        Item malfunctionToReturn;
        do {
            var indexOfMalfunctionToReturn = Random.Range(0, malfunctionsList.Count);
            malfunctionToReturn = malfunctionsList[indexOfMalfunctionToReturn];
        } while (malfunctionToReturn.Equals(lastMalfunction));

        lastMalfunction = malfunctionToReturn;
        return (malfunctionToReturn, malfunctions[malfunctionToReturn].Item1);
    }
}
