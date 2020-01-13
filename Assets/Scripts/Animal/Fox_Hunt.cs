﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox_Hunt : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip chaseSound;
    public AudioClip findSound;

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
        //audioSource.clip = findSound;
        //audioSource.Play();
    }

    IEnumerator IEChase()
    {
        while (true)
        {
            if (found)
            {
                audioSource.clip = chaseSound;
                if (!audioSource.isPlaying)
                    audioSource.Play();
                foxStatus = FoxStatus.Chase;
            }
            else
            {
                if(audioSource.isPlaying)
                    audioSource.Stop();

                Rigidbody fox = GetComponent<Rigidbody>();
                float dir = Random.Range(-50f, 50f);

                if (this.transform.position.y <= 13)
                {
                    this.transform.eulerAngles = new Vector3(0, 180f, 0);
                }
                else
                {
                    this.transform.eulerAngles = new Vector3(0, dir, 0);
                }
                fox.velocity = (this.transform.forward * 10);
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
        switch (foxStatus)
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
                    found = true;
                }
                break;
            }
        }
    }

    void Chase()
    {
        if (target_rabbit == null)
            return;
        Rigidbody fox = GetComponent<Rigidbody>();
        Vector3 current_pos = this.transform.position;
        offset = target_rabbit.transform.position - current_pos;
        if (target_rabbit == null)
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
            if (target_rabbit.transform.position.y <= 13)
            {
                this.transform.eulerAngles = new Vector3(0, 180f, 0);
                fox.velocity = (this.transform.forward * 10);
                target_rabbit = null;
                found = false;
            }
            else
            {
                fox.transform.position = Vector3.MoveTowards(fox.transform.position, target_rabbit.transform.position, 0.25f);
                fox.transform.LookAt(target_rabbit.transform);
            }

        }
    }






}