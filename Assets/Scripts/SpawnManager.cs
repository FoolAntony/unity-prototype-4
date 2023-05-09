using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] powerupObjects;
    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;
    private float spawnRange = 9.0f;
    public int enemyCounter;
    public int waveNum = 1;
    // Start is called before the first frame update
    void Start()
    {
        int startIndex = Random.Range(0, powerupObjects.Length);
        SpawnEnemyWave(waveNum);
        Instantiate(powerupObjects[startIndex], GenerateRandomPosition(), powerupObjects[startIndex].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCounter = FindObjectsOfType<Enemy>().Length;
        if (enemyCounter == 0)
        {
            waveNum++;
            if(waveNum % bossRound == 0)
            {
                SpawnBossWave(waveNum);
            }
            else
            {
                SpawnEnemyWave(waveNum);
            }
            int indexPowerup = Random.Range(0, powerupObjects.Length);
            Instantiate(powerupObjects[indexPowerup], GenerateRandomPosition(), powerupObjects[indexPowerup].transform.rotation);
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomSpawnPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomSpawnPos;
    }
    void SpawnEnemyWave(int numOfEnemies)
    {
        for(int i = 0; i < numOfEnemies; i++)
        {
            int indexEnemy = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[indexEnemy], GenerateRandomPosition(), enemyPrefab[indexEnemy].transform.rotation);
        }
    }

    void SpawnBossWave (int currentRound)
    {
        int miniEnemyToSpawn;
        //no division by 0

        if(bossRound != 0)
        {
            miniEnemyToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemyToSpawn = 1;
        }
        var boss = Instantiate(bossPrefab, GenerateRandomPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemyToSpawn;
    }
    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GenerateRandomPosition(), miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }
}
