using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHealth = 25f;
    public GameObject deathEffect;
    public GameObject WinMenu;
    public GameObject GameUI;
    public static int enemiesAlive = 0;

    private void Awake()
    {
        enemiesAlive = 0;
    }

    private void Start()
    {
        enemiesAlive++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyHealth -= collision.relativeVelocity.magnitude;

        if (enemyHealth <=0)
        {
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

        enemiesAlive--;
        if (enemiesAlive <= 0)
        {
            GameUI.SetActive(false);
            WinMenu.SetActive(true);
            Debug.Log("you have won this level");
        }
    }
}
