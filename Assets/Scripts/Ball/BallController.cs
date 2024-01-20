using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    ObjectPool pool;
    public GameObject particleEffectPrefab;
    public EnemySpawner enemySpawner;

    public AudioSource hitAS;
    public AudioClip hitAC;
    private void Start()
    {
        pool = FindObjectOfType<ObjectPool>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            CameraShake.Invoke();           
            pool.ReleaseEnemy(other.gameObject);
            hitAS.Play(); 

            if (particleEffectPrefab != null)
            {
                GameObject particleEffect = Instantiate(particleEffectPrefab, other.transform.position, Quaternion.identity);
                Destroy(particleEffect, 2f);
            }

            enemySpawner.NotifyEnemyKilled();
        }
    }
   
}
