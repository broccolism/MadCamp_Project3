using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWater : MonoBehaviour
{
    public float waterHeight;
    public Transform targetObjectTransform;

    private bool isUnderWater;
    private Color normalFogColor;
    private float normalFogDensity;
    public Color underwaterColor;
    public float underwaterDensity;

    // Start is called before the first frame update
    void Start()
    {
        normalFogColor = RenderSettings.fogColor;
        normalFogDensity = RenderSettings.fogDensity;
        underwaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
        underwaterDensity = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if((targetObjectTransform.position.y < waterHeight) != isUnderWater)
        {
            isUnderWater = targetObjectTransform.position.y < waterHeight;
        }
        if (isUnderWater)
        {
            SetUnderWater();
        }
        if (!isUnderWater)
        {
            SetNormal();
        }
    }

    void SetNormal()
    {
        RenderSettings.fogColor = normalFogColor;
        RenderSettings.fogDensity = normalFogDensity;
    }

    void SetUnderWater()
    {
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fogDensity = underwaterDensity;
    }
}
