using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    //public float spawnRate = 2f;
    //float nextSpawnTime = 0f;
    public ObjectPool objectPool;
    public CardController cardController;
    //public GameObject enemy;
    public int xPos;
    public int zPos;
    public int enemyCount;
    public float spawnInterval = 1f;

    public float spawnDuration = 10f;
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        objectPool = FindObjectOfType<ObjectPool>();
        //cardController = FindObjectOfType<CardController>();
    }
  

    IEnumerator SpawnEnemies()
    {
        float elapsedTime = 0f;

        while (elapsedTime<spawnDuration)
        {
            xPos = Random.Range(-5, 5);
            zPos = Random.Range(-23, 10);
            GameObject enemy = objectPool.GetPooledEnemy();
            enemy.SetActive(true);
            if (enemy != null)
            {
                enemy.transform.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-23, 10));      
            }
            yield return new WaitForSeconds(spawnInterval);
            elapsedTime += spawnInterval;
            enemyCount += 1;
        }

        CheckAllEnemiesDead();
        StopCoroutine(SpawnEnemies());
    }

    private void CheckAllEnemiesDead()
    {
        StartCoroutine(CheckEnemiesCoroutine());
    }

    private IEnumerator CheckEnemiesCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

            if (enemies.Length == 0)
            {
                OnAllEnemiesDead();
                yield break;
            }
        }
    }

    private void OnAllEnemiesDead()
    {
        Debug.Log("Tum dusmanlar oldu.");
        cardController.cardPanel.gameObject.SetActive(true);
    }


}
