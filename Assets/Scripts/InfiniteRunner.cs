
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRunner : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject diamondPrefab;
    public Transform player;
    public float spawnDistance = 400f; // Match the tile length
    public int numberOfTiles = 2; // Number of tiles to keep active
    public int diamondsPerTile = 20; // Number of diamonds per tile

    public Vector3 startPosition = Vector3.zero; // Let the user define the start position

    private List<GameObject> activeTiles = new List<GameObject>();
    private Vector3 nextTileSpawnPosition;
    private bool gameStarted = false; // Flag to check if the game has started

    void Start()
    {
        gameStarted = true;

        // Set the initial spawn position to the user-defined start position
        nextTileSpawnPosition = startPosition;

        // Align the player to start from a position relative to the first tile
        player.position = new Vector3(player.position.x, player.position.y, startPosition.z);

        // Spawn the initial tiles and diamonds
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (!gameStarted)
        {
            // Debug.Log("Game not started yet.");
            return;
        }

        // Check if the player has moved enough to trigger the spawning of the next tile
        if (player.position.z > nextTileSpawnPosition.z - (spawnDistance * (numberOfTiles / 2)))
        {
            SpawnTile();
            RemoveOldTile();
        }
    }

    void SpawnTile()
    {
        GameObject newTile = Instantiate(tilePrefab, nextTileSpawnPosition, Quaternion.identity);
        activeTiles.Add(newTile);

        // Update the next tile spawn position
        nextTileSpawnPosition.z += spawnDistance;

        // Spawn diamonds on the new tile
        for (int i = 0; i < diamondsPerTile; i++)
        {
            float randomY = 1f; // Diamond height
            float zPosition = newTile.transform.position.z + Random.Range(50f, 350f); // Random Z within tile

            // Choose one of the two methods below:
            float randomX = Random.Range(-5f, 3f); // ✅ Free movement within tile
                                                   // float randomX = lanes[Random.Range(0, lanes.Length)]; // ✅ Lane-based spawning

            Vector3 diamondPos = new Vector3(randomX, randomY, zPosition);
            Instantiate(diamondPrefab, diamondPos, Quaternion.identity);

        }
    }

    void RemoveOldTile()
    {
        if (activeTiles.Count > numberOfTiles)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
        }
    }
}
