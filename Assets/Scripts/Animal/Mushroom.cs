using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public DynamicObjectGenerator generator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Rabbit") //when the carrot meets rabbit
        {
            Debug.Log("@@@@ YOU ATE MUSHROOM @@@");
            generator.RemoveObject(this.gameObject);
            Destroy(this.gameObject);
        }

    }
}
