using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private float spawnPos = 5f;
    private float tileLength = 10f;
    private int startTiles = 10;
    [SerializeField] public Transform player;
    void Start()
    {
        SpawnTile(0);
        SpawnTile(1);
        SpawnTile(2);
        SpawnTile(3);
        SpawnTile(4);
        SpawnTile(5);
        SpawnTile(6);
    }


    void Update()
    {
        if (player.position.z > spawnPos - (startTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    private void SpawnTile(int tileIndex)
    {
        Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPos, Quaternion.Euler(0, 90, 0));
        spawnPos += tileLength;
    }
}
