using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public GameObject sun;
    public GameObject moon;

    private Light sunLight;
    private Light moonLight;
    // Start is called before the first frame update
    void Start()
    {
        sunLight = sun.GetComponent<Light>();
        moonLight = moon.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, 10f * Time.deltaTime);
        if(transform.rotation.eulerAngles.z < 70 || transform.rotation.eulerAngles.z > 290)
        {
            sunLight.intensity = 0;
            moonLight.intensity = 1;
        } else if( transform.rotation.eulerAngles.z > 110 && transform.rotation.eulerAngles.z < 270)
        {
            sunLight.intensity = 1;
            moonLight.intensity = 0;
        }
    }
}
