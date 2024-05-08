using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] string nextScene;
    [SerializeField] int enemiesToProgress;

    int enemyCount;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == enemiesToProgress)
        {
            StaticData.Health = player.GetComponent<PlayerTopDown>().health;
            StartCoroutine(Delay(5));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerTopDown>())
        {
            StaticData.Health = player.GetComponent<PlayerTopDown>().health;
            SceneManager.LoadScene(nextScene.ToString());
        }
    }

    IEnumerator Delay(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(nextScene.ToString());
    }

}
