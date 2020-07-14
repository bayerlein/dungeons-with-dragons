using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float arrowDamage;
    public float arrowSpeed = 30f;
    public float arrowLifeTime;
    void Start()
    {
        Destroy(gameObject, arrowLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * arrowSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            
            EnemyController meleeEnemy = other.GetComponent<EnemyController>();
            EnemyRangedController rangedEnemy = other.GetComponent<EnemyRangedController>();
            if(meleeEnemy != null)
            {
                meleeEnemy.TakeDamage(arrowDamage);
            }
            else if (rangedEnemy != null)
            {
                rangedEnemy.TakeDamage(arrowDamage);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Boss"))
        {
            other.GetComponent<BossController>().TakeDamage(arrowDamage);
            Destroy(gameObject);
        }
    }
}
