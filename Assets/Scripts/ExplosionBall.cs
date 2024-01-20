using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBall : MonoBehaviour
{
    public float patlamaCapi = 5f; // Patlama �ap�
    public LayerMask dusmanLayer; // D��manlar�n katman�

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
            // D��man� �ld�r veya ba�ka bir �ey yap
            Destroy(dusmanCollider.gameObject);
        }

        // Patlama efekti veya ba�ka bir �ey eklenebilir
        Destroy(gameObject);
    }
}
