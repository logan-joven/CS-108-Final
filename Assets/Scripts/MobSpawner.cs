using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] Camera _mCam;
    [SerializeField] float spawnTime;
    [Min(1)]
    [SerializeField] int spawnOffset = 1;
    [SerializeField] List<GameObject> enemyList = new List<GameObject>();
    bool spawned;
    // Start is called before the first frame update
 
    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            spawned = true;
            Invoke("SpawnEnemy", spawnTime);
        }
    }

    private void SpawnEnemy()
    {
        
        GameObject enemy = Instantiate(enemyList[0]);

        int randDir = Random.Range(0, 3);

        float cameraHeight = _mCam.orthographicSize;
        float cameraWidth = cameraHeight * _mCam.aspect;

        switch (randDir)
        {
            case 0: // Up
                enemy.transform.position = new Vector2(_mCam.transform.position.x + Random.Range(-cameraWidth, cameraWidth), _mCam.transform.position.y + cameraHeight + Random.Range(1, spawnOffset));
                break;
            case 1: // Down
                enemy.transform.position = new Vector2(_mCam.transform.position.x + Random.Range(-cameraWidth, cameraWidth), _mCam.transform.position.y - cameraHeight - Random.Range(1, spawnOffset));
                break;
            case 2: // Right
                enemy.transform.position = new Vector2(_mCam.transform.position.x + cameraWidth + Random.Range(1, spawnOffset), _mCam.transform.position.y + Random.Range(-cameraHeight, cameraHeight));
                break;
            case 3: // Left
                enemy.transform.position = new Vector2(_mCam.transform.position.x - cameraWidth - Random.Range(1, spawnOffset), _mCam.transform.position.y + Random.Range(-cameraHeight, cameraHeight));
                break;

        }

        spawned = false;
    }
}


