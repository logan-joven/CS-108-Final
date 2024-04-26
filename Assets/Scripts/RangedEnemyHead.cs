using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyHead: MonoBehaviour{
    // Distance to Shoot
    public float distanceToShoot = 7f;

    // Bullet Variables
    private float shotCooldown;
    public float startShotCooldown;
    public GameObject EnemyBullet;

    // Animator
    public Animator animator;

    Transform target;

     private void Awake()
    {
        // Set the Target to be Player
        target = FindObjectOfType<PlayerTopDown>().transform;
        // Set Initial ShotCooldown
        shotCooldown = startShotCooldown;

    }

    private void FixedUpdate()
    {
        // Direction for Enemy to Look at Player
        Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        // Actually Enables Enemy to Look at player
        transform.up = direction;
    
         // If Shot is not on Cooldown and Target is Close Enough to Shoot
        if(shotCooldown <= 0 && Vector2.Distance(target.position, transform.position) <= distanceToShoot){          
            animator.SetFloat("SetFire",1);
            StartCoroutine(Shoot());
        } else{
            shotCooldown -= Time.deltaTime;
            animator.SetFloat("SetFire", 0);
        }
    }

    IEnumerator Shoot(){
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        if(shotCooldown <= 0 && Vector2.Distance(target.position, transform.position) <= distanceToShoot){
            Instantiate(EnemyBullet, transform.position, transform.rotation);
            shotCooldown = startShotCooldown;
        }
        // Instantiate(EnemyBullet, transform.position, transform.rotation);
        // shotCooldown = startShotCooldown;
    }
}