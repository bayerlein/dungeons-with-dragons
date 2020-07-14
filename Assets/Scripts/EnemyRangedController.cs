using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedController : MonoBehaviour
{
    private float shootTimer;
    private bool canShoot;
    public float shootCooldown;
    public float stoppingDistance;
    public float retreatDistance;
    public GameObject projectile;
    public float moveSpeed;
    private Transform player;
    public float health;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shootTimer = shootCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                // move to player
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            } 
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                //parado - pode atirar
                transform.position = this.transform.position;
                if (canShoot) Shoot();

            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                //recuar
                transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
            }
        }
        
        if (shootTimer <= 0)
        {
            canShoot = true;
        }
        else 
        {
            canShoot = false;
            shootTimer -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
        shootTimer = shootCooldown;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
