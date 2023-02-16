using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float spawnWidth = 4.5f;// 14.0f;
    private float spawnRate = 1.2f;
    private float spawnTimer = 2.0f; //0.0f;
    private int whichEnemy;
    private bool active;


    [SerializeField] private GameObject enemyType1;
    [SerializeField] private GameObject enemyType2;
    [SerializeField] private GameObject enemyType3;
    [SerializeField] private GameObject enemyType5;
    [SerializeField] private GameObject enemyType6;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (active && Time.time > spawnTimer && !UI_Manager.instance.IsGameOver)
        {
            float spawnX = Random.Range(transform.position.x - spawnWidth / 2, transform.position.x + spawnWidth / 2);


            whichEnemy = Random.Range(0, 101);
                
            if (whichEnemy < 77) Instantiate(enemyType1, new Vector3(transform.position.x + spawnX, transform.position.y, 0), Quaternion.identity);
            if (whichEnemy >= 30 && whichEnemy < 77) Instantiate(enemyType2, new Vector3(transform.position.x + spawnX, transform.position.y, 0), Quaternion.identity);
            if (whichEnemy >= 77 && whichEnemy < 87) Instantiate(enemyType3, new Vector3(transform.position.x + spawnX, transform.position.y, 0), Quaternion.identity);
            if (whichEnemy >= 87 && whichEnemy < 97) Instantiate(enemyType5, new Vector3(transform.position.x + spawnX, transform.position.y, 0), Quaternion.identity);
            if (whichEnemy >= 93 && whichEnemy < 101) Instantiate(enemyType6, new Vector3(transform.position.x + spawnX, transform.position.y, 0), Quaternion.identity);




            spawnTimer = Time.time + spawnRate;
            spawnRate -= 0.001f;
        }
        
    }
}
