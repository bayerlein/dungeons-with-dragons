using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D playerRb;
    private Vector2 direction;
    private BowController bowController;
    private bool recovering;
    private float recoveryCounter;
    public float recoveryTime;
    public int health;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        bowController = FindObjectOfType<BowController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.direction.x = Input.GetAxisRaw("Horizontal");
        this.direction.y = Input.GetAxisRaw("Vertical");

        if(Input.GetMouseButtonDown(0))
        {
            bowController.Shoot();
        }

        if(recovering)
        {
            recoveryCounter += Time.deltaTime;
            if(recoveryCounter >= recoveryTime)
            {
                recovering = false;
                recoveryCounter = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        this.playerRb.MovePosition(this.playerRb.position + (this.direction * this.moveSpeed * Time.fixedDeltaTime));
    }

    public void TakeDamage(int damage)
    {
        if(!recovering)
        {
            health -= damage;
            recovering = true;

            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
