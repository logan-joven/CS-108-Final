using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuffPickup : MonoBehaviour
{
    public PlayerShootScript player;
    [SerializeField] int seconds = 3;
    private void Awake()
    {
        // Set the Target to be Player
        player = FindObjectOfType<PlayerShootScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Buff(seconds));

        }
    }

    IEnumerator Buff(int seconds)
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f); // make it transparent
        player.bulletFireRate = 0.5f;
        yield return new WaitForSeconds(seconds);
        player.bulletFireRate = 1.5f;
        Destroy(this.gameObject);
    }
}
