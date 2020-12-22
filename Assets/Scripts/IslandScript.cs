using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandScript : MonoBehaviour
{
    private BoxCollider2D boxCol;
    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        GameManager.Instance.addIsland(new Island(gameObject, boxCol.bounds, new ArrayList(), null));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
