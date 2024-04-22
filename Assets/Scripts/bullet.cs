using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public Vector2 moveDirection;
    [Space(10)]
    public float moveSpeed = 5f;
    [SerializeField] float deathTime = 2.5f;
    public int damage = 1;

    private void Start()
    {
        Destroy(this.gameObject, deathTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);

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
        }
                    
    }
}
