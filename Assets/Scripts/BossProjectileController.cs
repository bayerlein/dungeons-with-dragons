using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileController : MonoBehaviour
{
    private bool hasDirection;
    private Vector2 moveDirection;

    public float projectileLifeTime;
    public float projectileSpeed;
    public int projectileDamage;
    void Start()
    {
        Destroy(gameObject, projectileLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasDirection)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveDirection, projectileSpeed * Time.deltaTime);
        }
    }

    public void SetDirection(Vector2 projectileDirection)
    {
        hasDirection = true;
        moveDirection = projectileDirection;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }
}
