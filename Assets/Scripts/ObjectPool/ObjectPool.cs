using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public GameObject enemyPrefab;
    public int poolSize = 10;
    List<GameObject> enemyPool;

    void Start()
    {
        enemyPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            CreatePooledEnemy();
        }
    }

    private GameObject CreatePooledEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.SetActive(false);
        enemyPool.Add(enemy);
        return enemy;
    }

    public GameObject GetPooledEnemy()
    {
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (!enemyPool[i].activeInHierarchy)
            {
                return enemyPool[i];
            }
        }
        return CreatePooledEnemy();
    }

    public void ReleaseEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
    }

}
