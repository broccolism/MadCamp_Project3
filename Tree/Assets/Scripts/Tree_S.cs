using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_S : TreeEntity
{
    //GameObject PreFabTree_M = Tree_M.prefab;
    
    TreeManager treeManager;
    public TreeBluePrint treeBluePrint;


    private void Awake()
    {
        treeManager = TreeManager.instance;
        treeBluePrint = new TreeBluePrint();
    }

    private void Start()
    {
        
        float max = 1f;
        float current = 0;
        float speed = Random.Range(0.1f, 0.6f);
        bool adult = false;

        Init(max, current, speed, adult);


    }

    private void Update()
    {

        if (currentScale < maxScale)
        {
            this.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            currentScale += growthSpeed * Time.deltaTime;
                       
        }
        else
        {
            Invoke("pop", 10f);

            //after growing
            pop();
            goAway();

            buildTree_M(treeBluePrint);
        }

    }

    private void buildTree_S(TreeBluePrint blueprint)
    {
        GameObject _tree = (GameObject)Instantiate(blueprint.prefabS, transform.position, transform.rotation);
        tree = _tree;
        treeBluePrint = blueprint;

        treeStep = 0;
        //GameObject currentTree = (GameObject)Instantiate(Tree_M);
    }

    private void buildTree_M(TreeBluePrint blueprint)
    {
        Debug.Log("DESTROYING");
        Destroy(tree); //destory smaller tree

        GameObject _tree = (GameObject)Instantiate(blueprint.prefabM, transform.position, transform.rotation);
        Debug.Log("FAB: " + blueprint.prefabM);
        tree = _tree;
        Debug.Log("TREE: " + tree);

        treeStep = 1;
    }
}
