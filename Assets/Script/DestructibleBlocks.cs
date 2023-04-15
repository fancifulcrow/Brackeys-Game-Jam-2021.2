using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlocks : MonoBehaviour
{
    public float blockHealth = 20f;
    public GameObject breakingEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        blockHealth -= collision.relativeVelocity.magnitude;

        if (blockHealth <= 0)
        {
            DestroyBlock();
        }
    }

    private void DestroyBlock()
    {
        Instantiate(breakingEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
