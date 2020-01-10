using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectGenerator : MonoBehaviour
{
    const string terrainName = "Terrain Chunk"; //터렌인 청크를 확인하는데 사용한다 
    public float maxDistance = 1000f; //얼만큼 깊이로 RayCast 를 할건지 정한다 
    public bool combine; //combine 설정은 리기드 바디 생성과 convex 옵션을 가립니다 

    [System.NonSerialized]
    public static bool genreatingTerrainObjectCompleted;

    public StaticTerrainObjectType terrainObjectType;

    //밀도 관련 변수들
    float totalDenstiy;

    //청크 관련 변수
    int initialChunkCoordX = 0;
    int initialChunkCoordY = 0;
    int chunkSize = 95;
    int chunkNumber = 1;
    float scale;

    Dictionary<Vector2, GameObject> terrainChunkObjectDictionary = new Dictionary<Vector2, GameObject>();

    public void Start()
    {
        chunkNumber = TerrainGenerator.chunkRenderNumber;
        chunkSize = MapGenerator.mapChunkSize;
        scale = TerrainGenerator.scale;
        float width = terrainObjectType.prefab.GetComponent<Renderer>().bounds.size.x;
        float height = terrainObjectType.prefab.GetComponent<Renderer>().bounds.size.z;
        totalDenstiy = terrainObjectType.spwanDensity * terrainObjectType.spwanDensity / (scale * scale * width * height);

        StartCoroutine(tempCorutine());
    }

    void OnValidate()
    {
        if (terrainObjectType.startPosition > terrainObjectType.endPosition)
            terrainObjectType.endPosition = 1;
    }


    IEnumerator tempCorutine()
    {
        yield return new WaitForSeconds(3);
        GenerateTerrainObject();
    }


    void GenerateTerrainObject()
    {
        initialChunkCoordX = 0;
        initialChunkCoordY = 0;

        for (int yOffset = -chunkNumber; yOffset <= chunkNumber; yOffset++)
        {
            for (int xOffset = -chunkNumber; xOffset <= chunkNumber; xOffset++)
            {
                Vector2 chunkCoord = new Vector2(initialChunkCoordX + xOffset, initialChunkCoordY + yOffset);
                terrainChunkObjectDictionary.Add(chunkCoord, GenerateTerrainObjectAtChunk(chunkCoord));
            }
        }

        genreatingTerrainObjectCompleted = true;
    }


    GameObject GenerateTerrainObjectAtChunk(Vector2 coord)
    {
        Vector2 position = coord * chunkSize;

        Queue<MeshFilter> meshFilters = new Queue<MeshFilter>();

        int yBound = Mathf.RoundToInt((position.x - chunkSize / 2f) * scale);
        int xBound = Mathf.RoundToInt((position.y - chunkSize / 2f) * scale);

        for (int yOffset = yBound; yOffset <= yBound + chunkSize * scale; yOffset++)
        {
            for (int xOffset = xBound; xOffset <= xBound + chunkSize * scale; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(initialChunkCoordX + xOffset, initialChunkCoordY + yOffset);
                if (CalculateDensity())
                {
                    //오브젝트가 스폰됨    
                    GameObject generatedObject = SpwanTerrainObject(viewedChunkCoord, terrainObjectType);
                    if (generatedObject != null)
                        meshFilters.Enqueue(generatedObject.GetComponent<MeshFilter>());
                }
            }
        }

        if (meshFilters.Count == 0)
            return null;

        if (combine == false)
            return null;

        //여러개의 터레인 오브젝트를 하나로 뭉치는 역할을 한다 
        GameObject meshes = new GameObject();
        CombineInstance[] combines = new CombineInstance[meshFilters.Count];

        int i = 0;
        while (meshFilters.Count > 0)
        {
            MeshFilter tempMesh = meshFilters.Dequeue();
            combines[i].mesh = tempMesh.sharedMesh;
            combines[i].transform = tempMesh.transform.localToWorldMatrix;
            Destroy(tempMesh.gameObject);

            i++;
        }

        meshes.transform.parent = gameObject.transform;
        meshes.name = "Terrain Object Chunk";
        meshes.AddComponent<MeshFilter>();
        meshes.AddComponent<MeshRenderer>();

        meshes.GetComponent<MeshFilter>().mesh = new Mesh();
        meshes.GetComponent<MeshFilter>().mesh.CombineMeshes(combines);
        meshes.GetComponent<MeshRenderer>().sharedMaterial = terrainObjectType.prefab.GetComponent<MeshRenderer>().sharedMaterial;


        if (terrainObjectType.useCollider)
        {
            var object_meshCollider = meshes.AddComponent<MeshCollider>();
        }

        meshFilters.Clear();
        return meshes;
    }

    public GameObject SpwanTerrainObject(Vector2 position, StaticTerrainObjectType terrainObjectType)
    {
        //스폰되면 게임 오브젝트를 아니면 null 을 반환한다 
        RaycastHit raycastHit;
        Vector3 origin = new Vector3(position.x, maxDistance / 2, position.y);

        Vector3 spwanPosition;
        Vector3 spwanNormal;
        Quaternion spwanRotation;

        bool isHit = Physics.Raycast(origin, Vector3.down, out raycastHit, maxDistance);

        if (isHit == true && raycastHit.collider.name.Contains(terrainName))
        {
            spwanPosition = raycastHit.point;
            //만약 위치가 생성하고자 하는 위치가 아니면 건너 뛴다 
            if (spwanPosition.y < terrainObjectType.startPosition || spwanPosition.y > terrainObjectType.endPosition)
            {
                return null;
            }
            spwanNormal = raycastHit.normal;

            if (!terrainObjectType.isErect)
                spwanRotation = Quaternion.FromToRotation(Vector3.up, 360 * spwanNormal) * terrainObjectType.prefab.transform.rotation;
            else
                spwanRotation = terrainObjectType.prefab.transform.rotation;
        }
        else
        {
            return null;
        }

        MeshCollider object_meshCollider;
        Rigidbody object_rigidbody;

        GameObject rock = Instantiate(terrainObjectType.prefab);
        //Initializing spwaned object
        if (terrainObjectType.insertDepth <= 0.001)
            rock.transform.position = new Vector3(spwanPosition.x, spwanPosition.y - terrainObjectType.insertDepth * scale, spwanPosition.z);
        else
            rock.transform.position = new Vector3(spwanPosition.x, spwanPosition.y, spwanPosition.z);
        rock.transform.rotation = spwanRotation;
        rock.transform.parent = this.transform;
        if (terrainObjectType.sizeMultiplier > 0.001)
            rock.transform.localScale *= terrainObjectType.sizeMultiplier;
        else
            rock.transform.localScale *= scale;

        //combine 을 사용한다면 콜라이더를 미리 붙힐 필요가 없다 
        if (combine == true)
            return rock;

        if (terrainObjectType.useRigidbody)
        {
            object_rigidbody = rock.AddComponent<Rigidbody>();
            object_meshCollider = rock.AddComponent<MeshCollider>();
            object_meshCollider.convex = true;
            if (terrainObjectType.isKnematic)
            {
                object_rigidbody.isKinematic = true;
            }
        }
        else if (terrainObjectType.useCollider)
        {
            object_meshCollider = rock.AddComponent<MeshCollider>();
        }

        return rock;
    }

    public bool CalculateDensity()
    {
        if (Random.Range(0, 1f) < totalDenstiy)
            return true;

        return false;
    }

}

[System.Serializable]
public class StaticTerrainObjectType
{
    public bool useRigidbody;
    public bool useCollider;
    public bool isKnematic;
    public bool isErect;

    public float height;
    public GameObject prefab;

    [Range(0, 1f)]
    public float spwanDensity;
    public float sizeMultiplier; //0 means no change to object size
    public float insertDepth;

    public float startPosition;
    public float endPosition;
}
