using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyHead: MonoBehaviour{
    // Distance to Shoot
    public float distanceToShoot = 7f;

    // Bullet Variables
    private float shotCooldown;
    public float startShotCooldown;
    public GameObject EnemyBullet1;
     public GameObject EnemyBullet2;
    private float pattern;

    // Animator
    public Animator animator;

    // Audio Sources
    public AudioClip shot1;
    public AudioClip shot2;

    Transform target;

     private void Awake()
    {
        // Set the Target to be Player
        target = FindObjectOfType<PlayerTopDown>().transform;
        // Set Initial ShotCooldown
        shotCooldown = startShotCooldown;
        pattern = 0;
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
            if(pattern == 0){
                StartCoroutine(Shoot1());
                pattern = 1;
            }
            else{
                StartCoroutine(Shoot2());
                pattern = 0;
            }
            
        } else{
            shotCooldown -= Time.deltaTime;
            animator.SetFloat("SetFire", -1);
        }
    }

    IEnumerator Shoot1(){
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        if(shotCooldown <= 0 && Vector2.Distance(target.position, transform.position) <= distanceToShoot){
            GetComponent<AudioSource>().PlayOneShot(shot1, 0.15f);
            Instantiate(EnemyBullet1, transform.position, transform.rotation);
            shotCooldown = startShotCooldown;
        }
        // Instantiate(EnemyBullet, transform.position, transform.rotation);
        // shotCooldown = startShotCooldown;
    }

    IEnumerator Shoot2(){
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        GetComponent<AudioSource>().PlayOneShot(shot2, 0.15f);
        Instantiate(EnemyBullet2, transform.position, transform.rotation);
        shotCooldown = startShotCooldown;
    }
}