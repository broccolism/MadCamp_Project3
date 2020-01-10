using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeExplosion_S : MonoBehaviour
{
    Transform pivot;

    //$$$$$ I don't think this is not that essential.... but... anyway!
    float cubesPivotDistance;
    Vector3 cubesPivot;

    public float cubeSize = 0.1f;
    public int cubesInRow = 4;
    public float explosionRadius = 5.0f;
    public float explosionForce = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        pivot = new GameObject().transform;
        pivot.position = new Vector3(0, 0, 0);
        transform.SetParent(pivot);


        //$$$$$ Not essential , neither
        //calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void pop()
    {
        gameObject.SetActive(false); //Hides gameobject when this function is called

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

    private void createPiece(int x, int y, int z)
    {
        //create Piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot; //$$$ "- cubesPivot"
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //add rigidbody and see mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
    }

    private void cascade(Collider[] destroyMe)
    {
        foreach (Collider hit in destroyMe)
        {
            DestroyImmediate(hit);
        }
    }

}
