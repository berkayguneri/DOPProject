using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    ObjectPool pool;
    public GameObject particleEffectPrefab;

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

            if (particleEffectPrefab != null)
            {
                GameObject particleEffect = Instantiate(particleEffectPrefab, other.transform.position, Quaternion.identity);
                Destroy(particleEffect, 2f);
            }
        }
    }
}
