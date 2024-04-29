using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup: MonoBehaviour
{
    public int health = 1;
    
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){        
            // If it was the Player itself, heal the player
            if(collision.GetComponent<PlayerTopDown>() && collision.GetComponent<PlayerTopDown>().health < 3)
            {
                collision.GetComponent<PlayerTopDown>().health += health;
                 Destroy(this.gameObject);
            }
        }
    }
}