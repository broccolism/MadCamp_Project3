using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public static TreeManager instance;


    TreeEntity tree;

    private TreeBluePrint treeToBuild;
    private TreeEntity selectedTree;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one manager!");
            return;
        }
        instance = this;

    }

    public TreeBluePrint getTreeToBuild()
    {
        return treeToBuild;
    }



    public void SelectTree (TreeEntity tree)
    {
        selectedTree = tree;
        treeToBuild = null;
    }


    public void DeselectTree()
    {
        selectedTree = null;
    }

    public void selectTreeToBuild (TreeBluePrint tree)
    {
        treeToBuild = tree;
        DeselectTree();
    }

}
