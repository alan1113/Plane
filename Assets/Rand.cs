using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rand : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectPrefab; // 要放置的物体的预制体
    [SerializeField]
    public Terrain terrain; // 地形对象
    public int numberOfObjects = 10; // 要放置的物体数量

    void Start()
    {
        PlaceObjects();
    }

    void PlaceObjects()
    {
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;

        for (int i = 0; i < numberOfObjects; i++)
        {
            float randomX = Random.Range(0f, terrainSize.x);
            float randomZ = Random.Range(0f, terrainSize.z);

            // 获取地形上的高度
            float terrainHeight = terrain.SampleHeight(new Vector3(randomX, 0f, randomZ));

            Vector3 spawnPosition = new Vector3(randomX, terrainHeight+0.5f, randomZ);
            Quaternion spawnRotation = Quaternion.identity;

            Instantiate(objectPrefab, spawnPosition, spawnRotation);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
