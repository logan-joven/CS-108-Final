using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine: MonoBehaviour
{
    public int damage = 1;
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D collision){
        if (!collision.CompareTag("Enemy")){
            // If it was the Player itself, damage the player
            if(collision.GetComponent<PlayerTopDown>())
            {
                collision.GetComponent<PlayerTopDown>().health -= damage;
            }
            animator.SetFloat("Contact", 1);
            StartCoroutine(Explode());
        }
    }
    IEnumerator Explode(){
         // Wait for the duration of the animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(this.gameObject);
    }
}