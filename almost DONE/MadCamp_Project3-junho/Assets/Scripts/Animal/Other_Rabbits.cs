using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Other_Rabbits : MonoBehaviour
{
    public RabbitStatus rabbitStatus;
    private bool found = false;
    public float viewRange = 20f;
    Vector3 offset;
    GameObject target_rabbit;
    public Transform canvas;

    private string[] messages = { "당근 맛있다\n\n",
        "당근 맛있다\n\n",
        "으악\n\n",
        "야옹\n\n",
        "토끼로 변했어요 살려주세요\n\n",
        "토끼 맛있다\n\n",
        "뒤!!\n\n",
        "당근, 파프리카, 호박 등 노란색 채소는 조리해 먹어야 효과가 극대화 된다\n\n",
        "당근에 있는 베타카로틴은 몸속에서 비타민A로 바뀌어 면역력 증진과 눈건강에 도움을 준다\n\n",
        "생활의 팁) 음식이 싱거울 땐, 소금을 뿌려서 먹자",
        "파프리카 맛있다\n\n",
        "어흥\n\n",
        "군고구마말랭이 마시쩡\n\n"};




    public enum RabbitStatus
    {
        Hop,
        Talk
    }

    
    // Start is called before the first frame update
    void Start()
    {
        //canvas.SetActive(false);
        StartCoroutine(Hop());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (rabbitStatus)
        {
            case RabbitStatus.Hop:
                Find();
                break;
            case RabbitStatus.Talk:
                Talk();
                break;
        }
    }

   

    IEnumerator Hop()
    {
        while (true)
        {
            Rigidbody rabbit = GetComponent<Rigidbody>();
            float dir = Random.Range(-50f, 50f);

            if (this.transform.position.y <= 13)
            {
                this.transform.eulerAngles = new Vector3(0, 180f, 0);
            }
            else
            {
                this.transform.eulerAngles = new Vector3(0, dir, 0);
            }
            rabbit.velocity = new Vector3(0, 2, 0) + (this.transform.forward * 10);
            rabbitStatus = RabbitStatus.Hop;

            yield return new WaitForSeconds(1);
        }
    }

    void Find()
    {
        
    }
    void Talk()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Rabbit")
        {
            Debug.Log("@@@@@@@@@@@ HIT ME ");
            StartCoroutine(ShowText());
        }

    }

    IEnumerator ShowText()
    {
        TextMesh imText = GetComponent<TextMesh>();
        int num = Random.Range(0, messages.Length-1);
        imText.text = messages[num];
        yield return new WaitForSeconds(2);
        imText.text = "";
    }


}

