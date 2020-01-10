using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunhoTreeMange : MonoBehaviour
{
    public GameObject treeSmallPrefab;
    public GameObject treeMediumPrefab;
    public GameObject treeLargePrefab;



    private GameObject currentTreeObject;

    private int currentTree; //0: S, 1: M, 2: L

    private void Start()
    {
       
        GenerateTree();

        StartCoroutine(counter());
    }

    public void FixedUpdate()
    {
        
    }

    public void Udpate()
    {
        
    }


    public void GenerateTree()
    {
        //만약 지금 렌더링 되는 오브젝트가 있으면 지운다.

        //제일 조그마한 트리를 먼저 만든다. 
        currentTree = 0;

        treeSmallPrefab.transform.position = this.transform.position;

        currentTreeObject = Instantiate(treeSmallPrefab);

    }



    public void ChangeTree()
    {

        switch(currentTree)
        {
            case 0: //S -> M
                Destroy(currentTreeObject);
                currentTree = 1;
                treeMediumPrefab.transform.position = this.transform.position;
                currentTreeObject = Instantiate(treeMediumPrefab);
                Debug.Log("CHANGED 0 -> 1");
                break;
            case 1: //M -> L
                Destroy(currentTreeObject);
                currentTree = 2;
                treeLargePrefab.transform.position = this.transform.position;
                currentTreeObject = Instantiate(treeLargePrefab);

                Debug.Log("CHANGED 1 -> 2");
                break;
            default:
                
                break;
        }


        // m -> l

        //제일 큰 트리면 타이머 중지
    }


    IEnumerator counter() 
    {
        yield return new WaitForSeconds(6); //before S -> M
        ChangeTree();
        yield return new WaitForSeconds(16); //before M -> L
        ChangeTree();
        
    }


}
