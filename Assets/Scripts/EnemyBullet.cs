using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBullet: MonoBehaviour
{
    
    public int damage = 1;
    public float moveSpeed = 5f;
    [SerializeField] float deathTime = 2.5f;

    private void Start()
    {
        // If Enemy Bullet Stays alive too long, destroy it
        Destroy(this.gameObject, deathTime);
    }


    private void FixedUpdate()
    {
        // Makes the Bullet Move
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        // If the Object this Bullet Collided to isn't an Enemy
        if(!collision.CompareTag("Enemy")){
            
            Destroy(this.gameObject);

            // If it was the Player itself, damage the player
            if(collision.GetComponent<PlayerTopDown>())
            {
                collision.GetComponent<PlayerTopDown>().health -= damage;
            }
        }
    }
}