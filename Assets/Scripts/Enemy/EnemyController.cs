using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    AttributesManager attributesManager;
    PlayerStats playerStats;
    public float damage;

    public GameObject enemy;
    public int enemyCount;
    public float spawnInterval = 1f;

    public GameObject dieParticlePrefab;

    public AudioSource characterDieAS;
    public AudioClip characterDieAC;

    public CardController cardController;


    private void Start()
    {
        attributesManager = GetComponent<AttributesManager>();
        
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on the enemy object.");
        }

       
    }
    private void Update()
    {
  
        if (gameObject.activeSelf && agent != null && agent.isOnNavMesh)
        {

            agent.SetDestination(player.position);
        }
            playerStats = FindObjectOfType<PlayerStats>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerStats.GetComponent<PlayerStats>().TakeDamage(damage);
            if (playerStats.currentHealth <= 0)
            {
                if (dieParticlePrefab != null)
                {
                    GameObject particleEffect = Instantiate(dieParticlePrefab, transform.position, Quaternion.identity);
                    Destroy(particleEffect, 2f);
                    characterDieAS.Play();
                }
                Debug.Log("DIE");
                Time.timeScale = 1;
                cardController.cardPanel.SetActive(false);
            }
            
        }
    }
   

}
