using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public int health = 2;
    public float speed = 3;
    public int damage = 1;
    public float distanceToStop = 7f;

    // Bullet Variables
    private float shotCooldown;
    public float startShotCooldown;
    public GameObject EnemyBullet;

    // Target is player
    Transform target;

    private void Awake()
    {
        // Set the Target to be Player
        target = FindObjectOfType<PlayerTopDown>().transform;
        // Set Initial ShotCooldown
        shotCooldown = startShotCooldown;
    }

    // Called Once Per Frame
    private void FixedUpdate()
    {
        // If Enemy is out of Health, Destroy this Object
        if(health <= 0) Destroy(this.gameObject);

        // Direction for Enemy to Look at Player
        Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        // Actually Enables Enemy to Look at player
        transform.up = direction;
        
        // If player far away from the enemy, the enemy move towards player
        if(Vector2.Distance(target.position, transform.position) >= distanceToStop){
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        // If Shot is not on Cooldown and Target is Close Enough to Shoot
        if(shotCooldown <= 0 && Vector2.Distance(target.position, transform.position) <= distanceToStop){
            Instantiate(EnemyBullet, transform.position, transform.rotation);
            shotCooldown = startShotCooldown;
        } else{
            shotCooldown -= Time.deltaTime;
        }


    }



}
