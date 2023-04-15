using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionRadius = 2f;
    public float explosionForce = 5f;
    public GameObject explosionEffect;
    public float blockHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        blockHealth -= collision.relativeVelocity.magnitude;

        if (blockHealth <= 0)
        {
            Detonate();
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Detonate()
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in colliders){
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            Vector2 explosionPoint = hit.transform.position - transform.position;
            if (rb != null)
            {
                rb.AddForce(explosionForce * explosionPoint);
            }
        }

    }
}
