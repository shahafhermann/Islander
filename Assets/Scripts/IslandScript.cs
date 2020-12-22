﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandScript : MonoBehaviour
{
    public Collider2D borderCollider;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.addIsland(new Island(gameObject, null, borderCollider.bounds, new ArrayList()));
    }

    // Update is called once per frame
    void Update()
    {
    }
}