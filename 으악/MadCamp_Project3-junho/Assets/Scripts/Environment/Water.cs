using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float waterDept;
    public Transform playerTransform;
    public GameObject waterShaderObject;
    public Vector2 offset;

    private float renderingDst;

    private void Start()
    {
        renderingDst = Camera.main.farClipPlane;
    }

    public void Update()
    {
        waterShaderObject.transform.position = new Vector3(playerTransform.position.x + offset.x, waterDept, playerTransform.position.z + offset.y);
    }
}
