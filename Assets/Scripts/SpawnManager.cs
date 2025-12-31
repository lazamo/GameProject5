using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject PowerupPrefab;
    public float spawnRange = 9.0f;
    private int EnemyCount;
    private int waveNumber = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;

        if (EnemyCount == 0) 
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerup();
        }
    }

    private void SpawnPowerup() 
    {
        Instantiate(PowerupPrefab, GenerateSpawnPosition(), PowerupPrefab.transform.rotation);
    }

    private void SpawnEnemyWave(int enemyToSpawn) 
    {
        for (int i = 0; i < enemyToSpawn; i++) 
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition() 
    {
        float randomPosX = Random.Range(-spawnRange, spawnRange);
        float randomPosY = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(randomPosX, 0, randomPosY);
        return randomPos;
    }
}
