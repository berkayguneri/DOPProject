using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickController : MonoBehaviour
{
    public float hitSpeed;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("ball"))
        {
            collision.transform.GetComponent<Rigidbody>().AddForce(collision.GetContact(0).normal * hitSpeed,ForceMode.Impulse);
            
        }
    }

    public void TopSpeedIncrease()
    {
        hitSpeed += 50;
    }
}
