using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBall : MonoBehaviour
{
    public float patlamaCapi = 5f; // Patlama çapý
    public LayerMask dusmanLayer; // Düþmanlarýn katmaný

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Top"))
        {
            Invoke("PatlamaYap", 1f); // Topa vurulduktan 1 saniye sonra patlama yap
        }
    }

    void PatlamaYap()
    {
        Collider[] etkilenenDusmanlar = Physics.OverlapSphere(transform.position, patlamaCapi, dusmanLayer);

        foreach (Collider dusmanCollider in etkilenenDusmanlar)
        {
            // Düþmaný öldür veya baþka bir þey yap
            Destroy(dusmanCollider.gameObject);
        }

        // Patlama efekti veya baþka bir þey eklenebilir
        Destroy(gameObject);
    }
}
