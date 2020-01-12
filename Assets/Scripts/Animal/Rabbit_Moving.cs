using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Moving : MonoBehaviour
{
    //게임 관련 변수
    public GameManager gameManager;

    public float moveSpeed = 10.0f;
    public float rotSpeed = 3.0f;
    public float HP = 500f;
    public Camera tpsCam;
    public int jump_cool = 0;

    //플레이어 관련 변수들
    public PlayerVital playerVital;

    private bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HP == 0)
        {
            Die();
        }
        else
        {
            if (jump_cool > 0)
                jump_cool--;

            if (this.transform.position.y <= 12.1f)
            {
                playerVital.Attack(1);
                HP--;
            }
            MoveCtrl();
            RotCtrl();
        }
    }

    void MoveCtrl()
    {

        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jump_cool <= 0)
            {
                Rigidbody rabbit = GetComponent<Rigidbody>();
                rabbit.AddForce(0, 300f, 0);
                jump_cool = 100;
            }

        }

    }

    void RotCtrl()
    {
        float rotX = Input.GetAxis("Mouse Y") * rotSpeed;
        float rotY = Input.GetAxis("Mouse X") * rotSpeed;

        this.transform.localRotation *= Quaternion.Euler(0, rotY, 0);
        tpsCam.transform.localRotation *= Quaternion.Euler(-rotX, 0, 0);
    }

    void Die()
    {
        gameManager.EndGame();
    }
}