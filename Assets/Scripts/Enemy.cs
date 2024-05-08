using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public float speed = 3;
    public int damage = 1;
    public float distanceToDetect = 7f;
    public Animator animator;
    public Animator explode;

    bool dead = false;

    Transform target;

    private void Awake()
    {
        target = FindObjectOfType<PlayerTopDown>().transform;
    }


    private void FixedUpdate()
    {
        if(health <= 0 && !dead)
        {
            explode.SetFloat("Explode", 1);
            StartCoroutine(Explode());
        }

        if (!dead)
        {
            if (Vector2.Distance(target.position, transform.position) < distanceToDetect)
            {
                // Direction for Enemy to Look at Player
                Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
                // Actually Enables Enemy to Look at player
                transform.up = direction;
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                animator.SetFloat("Speed", 1);
            }
            else
            {
                animator.SetFloat("Speed", -1);
            }
        }
        
    }

    IEnumerator Explode()
    {
        dead = true;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(explode.GetCurrentAnimatorStateInfo(0).length);
        Destroy(this.gameObject);
    }


}
