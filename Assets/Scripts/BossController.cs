using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool rageMode;
    public float ragePercent;
    public float rageHealth;
    private Transform player;
    public bool canMove = true;
    public float moveSpeed;
    public int damage;
    public float health;
    public GameObject bossProjectile;
    public int projectileCount;
    public float shootTimer;
    public bool canShoot;
    public float shootCooldown;

    private Vector2 targetSpot;
    public Vector2[] movePoints;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GetNextSpot();
        rageHealth = (ragePercent * health) / 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if(transform.position.x != targetSpot.x && transform.position.y != targetSpot.y)
            {
                if (canMove) transform.position = Vector2.MoveTowards(transform.position, targetSpot, moveSpeed * Time.deltaTime);
            }
            else
            {
                GetNextSpot();
            }

            if (shootTimer <= 0)
            {
                Shoot();
            }
            else
            {
                shootTimer -= Time.deltaTime;
            }

            if (health <= rageHealth && !rageMode) Enrage();
        }
    }

    private void Enrage()
    {
        rageMode = true;
        moveSpeed *= 3;
        shootCooldown *= .75f;
        projectileCount = 10;
    }

    private void Shoot()
    {
        shootTimer = shootCooldown;
        var angleStep = 360f / projectileCount;

        float angle = 0f;

        for(int i = 0; i < projectileCount; i++)
        {
            float xPosition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * 360;
            float yPosition = transform.position.x + Mathf.Cos((angle * Mathf.PI) / 180) * 360;

            Vector2 projectileDirection = new Vector2(xPosition, yPosition);

            var projectile = Instantiate(bossProjectile, transform.position, Quaternion.identity);
            projectile.GetComponent<BossProjectileController>().SetDirection(projectileDirection * 3);

            angle += angleStep;
        }
    }

    void GetNextSpot()
    {
        int randomSpot = Random.Range(0, movePoints.Length);
        targetSpot = movePoints[randomSpot];
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(damage);
        }
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
