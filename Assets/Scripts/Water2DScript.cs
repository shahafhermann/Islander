using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water2DScript : MonoBehaviour
{
    public Vector2 MaxSpeed = new Vector2(0.01f, 0.01f);
    public float testSinkPercentage;

    private MeshRenderer renderer;
    private Material material;
    private Color waterColor;
    private float shaderMagnitude;

    void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        material = renderer.material;
        waterColor = material.color;
        shaderMagnitude = material.GetFloat("_Magnitude");
    }

    void LateUpdate()
    {
        //float sinkPerc = GameManager.Instance.getIslandSinkPercentage();
        float sinkPerc = testSinkPercentage;
        material.color = Color.white + sinkPerc * (waterColor - Color.white);
        material.SetFloat("_Magnitude", sinkPerc * shaderMagnitude);
        Vector2 scroll = Time.deltaTime * (sinkPerc * MaxSpeed);
        material.mainTextureOffset += scroll;
    }
}