using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemyBody: MonoBehaviour
{
    // Health of the body
    public int health = 2;
    bool dead = false;
    // Speed of the Tank
    public float speed = 3;
    // Distance of Where to stop
    public float distanceToStop = 7f;
    // Distance of Where to Back away
    // public float distanceToBackOff = 2f;
    // Distance to Detect Player
    public float distanceToDetect = 7f;
    // Animator
    public Animator animator;
    // Explode Animator
    public Animator explode;

    // Target
    Transform target;

    // Once Awakened
    private void Awake(){
        // Set the Target to be Player
        target = FindObjectOfType<PlayerTopDown>().transform;
    }

    private void FixedUpdate()
    { 
        // If within distance to detect
        if(Vector2.Distance(target.position, transform.position) < distanceToDetect){
            // Direction for Enemy to Look at Player, Dividing by 5 makes it rotate slower than head
            Vector2 direction = new Vector2(target.position.x - transform.position.x /5, target.position.y - transform.position.y);
            // Actually Enables Enemy to Look at player
            transform.up = direction;

            // If player far away from the enemy, the enemy move towards player
            if(Vector2.Distance(target.position, transform.position) >= distanceToStop){
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                animator.SetFloat("Speed",1);
            } 
            else{
                animator.SetFloat("Speed", -1);
            }
            
        }
    }

    private void Update()
    {
        // If Enemy is out of Health, Destroy this Object
        if (health <= 0 && !dead)
        {
            explode.SetFloat("Explode", 1);
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode(){
        dead = true;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(explode.GetCurrentAnimatorStateInfo(0).length);
        Destroy(this.gameObject);
    }
}