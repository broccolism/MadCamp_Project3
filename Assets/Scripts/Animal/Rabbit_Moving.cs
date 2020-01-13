using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_Moving : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip healSound;
    public AudioClip hopSound;
    public AudioClip carrotSound;

    public GameManager gameManager;

    public bool isGround;

    public float moveSpeed = 10.0f;
    public float rotSpeed = 3.0f;
    public Camera tpsCam;
    private int jump_cool = 0;
    public int maxJumpCool;
    public int numOfCarrots = 0;

    public float jumpForce;

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

        Vector3 v3 = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            v3 += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            v3 += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            v3 += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            v3 += Vector3.right;
        }

        transform.Translate(moveSpeed * v3.normalized * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jump_cool <= 0 && isGround)
            {
                isGround = false;
                Rigidbody rabbit = GetComponent<Rigidbody>();
                rabbit.AddForce(0, jumpForce, 0);
                jump_cool = 100;
                audioSource.PlayOneShot(hopSound, 1);
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
        if(collision.collider.tag == "ground")
        {
            isGround = true;
        }
        if (collision.collider.tag == "Carrot")
        {
            playerVital.EatCarrot();
            audioSource.PlayOneShot(carrotSound, 1);
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
            audioSource.PlayOneShot(attackSound, 0.5f);
            playerVital.Kill();
        }
        else if (collision.collider.tag == "Mushroom")
        {
            audioSource.PlayOneShot(healSound, 1);
            playerVital.EatMushroom(heal);
        }
    }
}