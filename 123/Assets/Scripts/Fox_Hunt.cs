using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox_Hunt : MonoBehaviour
{
    public enum FoxStatus
    {
        Chase,
        Idle,
    }
    public FoxStatus foxStatus;
    public float moveSpeed = 15.0f;
    public float viewRange = 60f;

    private Rigidbody eatMe; //rabbit
    private bool found = false; //found rabbit
    private int count = 0;
    GameObject target_rabbit;
    Ray ray;
    RaycastHit rayHit;
    
    Vector3 offset;

    public Collider collider;
    public float groundCheckDistance;
    private bool m_PreviouslyGrounded;
    private bool m_IsGrounded;


    // Start is called before the first frame update
    void Start()
    {
        m_PreviouslyGrounded = false;
        m_IsGrounded = false;
        StartCoroutine(IEChase());
        foxStatus = FoxStatus.Idle;
    }

    IEnumerator IEChase()
    {
        while(true)
        {
            if(found)
            {
                print("chase rabbit");
                foxStatus = FoxStatus.Chase;
            } else
            {
                Rigidbody fox = GetComponent<Rigidbody>();
                float dir = Random.Range(-50f, 50f);
                this.transform.eulerAngles = new Vector3(0, dir, 0);
                fox.velocity = (this.transform.forward * 10);
                print("found rabbit");
                foxStatus = FoxStatus.Idle;
            }
            yield return new WaitForSeconds(1);
        }

    }

    void OnCollisionEnter(Collision collision)
    {

    }

    void FixedUpdate()
    {   
        switch(foxStatus)
        {
            case FoxStatus.Idle:
                Find();
                break;
            case FoxStatus.Chase:
                Chase();
                break;
        }
    }

    void Find()
    {
        GameObject[] target = GameObject.FindGameObjectsWithTag("Rabbit");
        for (int i = 0; i < target.Length; i++)
        {
            GameObject onetarget = target[i];
            Vector3 current_pos = this.transform.position;
            Vector3 target_pos = onetarget.transform.position;
            offset = target_pos - current_pos;
            float target_dist = offset.magnitude;
            
            if (target_dist < viewRange)
            {
                target_rabbit = onetarget;
                ray = new Ray();
                
                ray.origin = current_pos;
                ray.direction = offset;
                if (Physics.Raycast(ray.origin, ray.direction, out rayHit, target_dist))
                {
                    
                    Debug.Log("@@@ RAY @@@ origin: " + ray.origin + ", direction: " + ray.direction + ", dist: " + target_dist);
                    Debug.Log("@@@@@" + rayHit.collider.gameObject.tag);
                    found = true;
                }
                break;
            }
        }
    }

    void Chase()
    {
        Rigidbody fox = GetComponent<Rigidbody>();
        Vector3 current_pos = this.transform.position;
        offset = target_rabbit.transform.position - current_pos;
        if(target_rabbit == null)
        {
            foxStatus = FoxStatus.Idle;
            return;
        }

        if (offset.magnitude > viewRange)
        {
            target_rabbit = null;
            found = false;
        }
        else
        {
            Debug.Log("chasing");
            fox.transform.position = Vector3.MoveTowards(fox.transform.position, target_rabbit.transform.position, 0.1f);
            fox.transform.LookAt(target_rabbit.transform);
            fox.velocity = (this.transform.forward * 10);
        }
    }

    

   

   
}
