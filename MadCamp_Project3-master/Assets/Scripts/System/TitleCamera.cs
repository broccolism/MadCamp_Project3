using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour
{
    public Transform cameraTransform;
    public float dollySpeed;

    private float dollyDelta;
    // Start is called before the first frame update
    void Start()
    {
        dollyDelta = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        cameraTransform.RotateAround(transform.position, Vector3.up, dollyDelta);
    }
}
