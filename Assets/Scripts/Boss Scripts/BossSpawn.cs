using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class BossSpawn : MonoBehaviour {
 
    public static BossSpawn instance;
    public GameObject firstBoss;
    public GameObject secondBoss;
    private GameObject boss;
    int num;
    public ParticleSystem spawn;

    public int enemyCountLimit;
    private int enemyCount;
    public Text EnemyCountText;
    private int bossCheck;
    

    private void Awake(){
        instance = this;
    }

    void SpawnAnim () {
        Instantiate(spawn, transform.position, Quaternion.identity);
    }
    void SpawnBoss () {
        bossCheck = bossCheck + 1;
        if(bossCheck % 2 == 0){
            boss = firstBoss;
        }else{
            boss = secondBoss;
        }
        Instantiate(boss, transform.position, Quaternion.identity);
    }

    public void SpawnAfterEnemiesBeat(int updateNum){
        EnemyCountText.text = enemyCount.ToString("0000");
        enemyCount = enemyCount + updateNum;
        num = enemyCount + updateNum;
        if(num % enemyCountLimit == 0 && (GameObject.Find("Boss(Clone)") == null && GameObject.Find("ChargeBoss(Clone)") == null)){
            Invoke ("SpawnAnim", 0);
            Invoke ("SpawnBoss", 2);
            num = 0;
        }
    }
}