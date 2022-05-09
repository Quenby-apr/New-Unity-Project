using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject starttilePrefab;
    private List<GameObject> activeTiles = new List<GameObject> ();
    private float spawnPos = -100;
    private float tileLength = 100;

    [SerializeField] private Transform player;
    private int startTiles = 5;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startTiles;i++)
        {
            if (i <= 2)
                SpawnTile(0);
            if (i > 2)
                SpawnTile(Random.Range(1, tilePrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z - tileLength > spawnPos - (startTiles * tileLength))
        {
            SpawnTile(Random.Range(1, tilePrefabs.Length));
            DeleteTile();
        }
            
    }
    
    private void SpawnTile(int tileIndex)
    {
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], transform.forward * spawnPos, transform.rotation);
        activeTiles.Add(nextTile);
        spawnPos += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
