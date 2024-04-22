using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBody: MonoBehaviour
{
    // Health of the body
    public int health = 2;
    // Speed of the Tank
    public float speed = 3;
    // Distance of Where to stop
    public float distanceToStop = 7f;
    // Distance of Where to Back away
    public float distanceToBackOff = 2f;
    // Animator
    public Animator animator;

    // Target
    Transform target;

    // Once Awakened
    private void Awake(){
        // Set the Target to be Player
        target = FindObjectOfType<PlayerTopDown>().transform;
    }

    private void FixedUpdate()
    {
        // If Enemy is out of Health, Destroy this Object
        if(health <= 0) Destroy(this.gameObject);

        // If player far away from the enemy, the enemy move towards player
        if(Vector2.Distance(target.position, transform.position) >= distanceToStop){
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            animator.SetFloat("Speed", 1);
        }
        // If player is close stop moving
        if(Vector2.Distance(target.position, transform.position) <= distanceToStop){
            //Moving Backwards
            // transform.position = Vector2.MoveTowards(transform.position, target.position, -1 * speed * Time.deltaTime);
            animator.SetFloat("Speed", -1);
        }

    }
}