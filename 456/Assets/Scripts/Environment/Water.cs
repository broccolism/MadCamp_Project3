using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float waterDept;
    public Transform playerTransform;
    public GameObject waterShaderObject;

    private float renderingDst;

    private void Start()
    {
        renderingDst = Camera.main.farClipPlane;
    }

    public void Update()
    {
        waterShaderObject.transform.position = new Vector3(playerTransform.position.x, waterDept, playerTransform.position.z);
    }
}
