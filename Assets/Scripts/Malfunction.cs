using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malfunction {
    
    // {malfunctioning object : {fix object, pickup?}}  :
    // -  If it's not for pickup, then it's just an interaction (such as a button).
    // -  If 'fix object' is null, then 'pickup' should always be false and the interaction should happen with the malfunctioning object.
    Dictionary<GameObject, Dictionary<GameObject, bool>> malfunctions = 
        new Dictionary<GameObject, Dictionary<GameObject, bool>>();
    
    public Malfunction(List<GameObject> malfunctions, List<GameObject> fixes, bool[] pickup) {
        for (int i = 0; i < malfunctions.Count; i++) {
            
        }
    }
}
