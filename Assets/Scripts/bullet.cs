using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    [Space(10)]
    public float moveSpeed = 10f;
    [SerializeField] float deathTime = 2.5f;
    public int damage = 1;

    private void Start()
    {
        Destroy(this.gameObject, deathTime);
    }

    private void FixedUpdate()
    {
        // Makes the Bullet Move
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            // For Melee Enemies
            if(collision.GetComponent<Enemy>())
            {
                collision.GetComponent<Enemy>().health -= damage;
            }
            // For Ranged Enemies
            if(collision.GetComponent<RangedEnemy>())
            {
                collision.GetComponent<RangedEnemy>().health -= damage;
            }
            // For Ranged Enemy Body
            if(collision.GetComponent<RangedEnemyBody>())
            {
                 collision.GetComponent<RangedEnemyBody>().health -= damage;
            }
            Destroy(this.gameObject);
        }
                    
    }
}
