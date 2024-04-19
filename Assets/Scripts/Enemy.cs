using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public float speed = 3;
    public int damage = 1;

    Transform target;

    private void Awake()
    {
        target = FindObjectOfType<PlayerTopDown>().transform;
    }


    private void FixedUpdate()
    {
        if(health <= 0) Destroy(this.gameObject);

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }



}
