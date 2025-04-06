using UnityEngine;

public class DiamondSpawner : MonoBehaviour
{
    public GameObject diamondPrefab; 
    public float spawnDistance = 80f; 
    public int numberOfDiamonds = 5; 

    private float[] lanePositions = new float[] { -2f, 0f, 2f }; 

    public float fixedY = 1f; 
    void Start()
    {
        SpawnDiamonds();
    }

    void SpawnDiamonds()
    {
        for (int i = 0; i < numberOfDiamonds; i++)
        {
            float randomX = lanePositions[Random.Range(0, lanePositions.Length)];

            float randomZ = transform.position.z + i * spawnDistance;

            Vector3 spawnPosition = new Vector3(randomX, fixedY, randomZ);

            Instantiate(diamondPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
