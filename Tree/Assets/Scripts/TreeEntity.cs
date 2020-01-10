using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeEntity : MonoBehaviour
{
    public float maxScale;
    public float currentScale;
    public float growthSpeed;
    public bool adult;
    
    public Color leafColor;
    

    public GameObject tree;
    public int treeStep;

    TreeManager treeManager;

    private void Awake()
    {
        treeManager = TreeManager.instance;
    }

    private void Start()
    {
           }

    public void Init(float max, float current, float speed, bool a)
    {
        maxScale = max;
        currentScale = current;
        growthSpeed = speed;
        adult = a;

    }

    public void increaseScale()
    {
        if (currentScale < maxScale)
        {
            this.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            currentScale += growthSpeed * Time.deltaTime;
        }
        else //done for this small tree
        {
            Invoke("pop", 5f);
        }
    }

    public void pop()
    {
        int cubesInRow = 4;
        float explosionRadius = 16.0f;
        float explosionForce = 16.0f;

        //gameObject.SetActive(false); //Hides gameobject when this function is called

        //loop 3 times to create 5x5x5 pieces in x, y, z coordinates
        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    createPiece(x, y, z);
                }
            }
        }

        Vector3 explosionPos = transform.position;


        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
            //get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 0);
        }

        cascade(colliders);

    }

    private void cascade(Collider[] destroyMe)
    {
        foreach (Collider hit in destroyMe)
        {
            DestroyImmediate(hit);
        }
    }

    private void createPiece(int x, int y, int z)
    {
        float cubeSize = 0.1f;

        //create Piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z); //$$$ "- cubesPivot"
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //add rigidbody and see mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
    }

    private void buildTree(TreeBluePrint blueprint)
    {
        GameObject _tree = (GameObject)Instantiate(blueprint.prefabS);
        tree = _tree;
    }

    public void goAway()
    {
        Destroy(gameObject);
    }

}
