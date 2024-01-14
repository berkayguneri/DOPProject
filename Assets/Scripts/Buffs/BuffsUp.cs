using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffsUp : MonoBehaviour
{
    public enum PowerUp { Health, Speed }
    public PowerUp powerUps;

    public float AmountToGive;

    private TopDownCharacterMover characterMov;

    private float rotationSpeed = 100f;

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void Awake()
    {
        characterMov = FindObjectOfType<TopDownCharacterMover>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (powerUps)
            {
                case PowerUp.Health:
                    characterMov.health += AmountToGive;
                    break;
                case PowerUp.Speed:
                    characterMov.MovementSpeed += AmountToGive;
                    break;
                default:
                    break;
            }

            Destroy(this.gameObject);
        }
    }
}
