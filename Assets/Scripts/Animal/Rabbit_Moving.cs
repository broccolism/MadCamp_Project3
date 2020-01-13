using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Moving : MonoBehaviour
{
    public GameManager gameManager;

    public float moveSpeed = 10.0f;
    public float rotSpeed = 3.0f;
    public Camera tpsCam;
    private int jump_cool = 0;
    public int maxJumpCool;
    public int numOfCarrots = 0;

    public InGameUI ui;

    //플레이어 관련 변수들
    public PlayerVital playerVital;
    public float heal = 100f;

    private bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
       if(maxJumpCool == 0)
        {
            maxJumpCool = 100;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerVital.playerHeathText.text != "0")
        {
            if (jump_cool > 0)
                jump_cool--;

            if (this.transform.position.y <= 12.1f)
            {
                playerVital.Attack(1);
            }
            MoveCtrl();
            RotCtrl();
        }

        if(numOfCarrots == 7)
        {
            gameManager.EndGame(true);
        }

        ui.jumpImage.fillAmount = 1 - jump_cool / (float)maxJumpCool;
        ui.jumpText.text = Mathf.RoundToInt(maxJumpCool - ((jump_cool / (float)maxJumpCool) * 100)).ToString();

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
                rabbit.AddForce(0, 500f, 0);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Carrot")
        {
            numOfCarrots++;
            Debug.Log("#### num of carrots: " + numOfCarrots);
            if (numOfCarrots == 1)
            {
                Debug.Log("@@@@before SET");
                Debug.Log("@@@@@@@ NULL: " + ui.full_carrot_0);
                ui.full_carrot_0.SetActive(true);
                Debug.Log("@@@@after SET");
            }
            else if (numOfCarrots == 2)
            {
                ui.full_carrot_1.SetActive(true);
            }
            else if (numOfCarrots == 3)
            {
                ui.full_carrot_2.SetActive(true);
            }
            else if (numOfCarrots == 4)
            {
                ui.full_carrot_3.SetActive(true);
            }
            else if (numOfCarrots == 5)
            {
                ui.full_carrot_4.SetActive(true);
            }
            else if (numOfCarrots == 6)
            {
                ui.full_carrot_5.SetActive(true);
            }
            else if (numOfCarrots == 7)
            {
                ui.full_carrot_6.SetActive(true);
            }
        }
        else if (collision.collider.tag == "Fox")
        {
            playerVital.Kill();
        }
        else if (collision.collider.tag == "Mushroom")
        {
            playerVital.Eat(heal);
        }
    }
}