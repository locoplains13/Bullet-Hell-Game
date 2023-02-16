using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : MonoBehaviour
{
    private float missileSpeed = 5f;
    private bool isEnemyFound;
    GameObject ClosestEnemy;
    Vector3 ClosestEnemyPrevpos;

    public GameObject missileImpactEffect;
    public int damage = 100;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isEnemyFound){
            MissileMove();
        } else{
            transform.position += Vector3.up * missileSpeed *Time.deltaTime;

            if(transform.position.y > 6f){
                Destroy(gameObject);
            }
        }

        
    }

    public void FindClosestEnemy(){
        float closestEnemyDistance = Mathf.Infinity;
        GameObject [] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyList != null){
            isEnemyFound = true;

            foreach (GameObject enemy in enemyList){
                float distanceToEnemy = (enemy.transform.position - transform.position).sqrMagnitude; 

                if (distanceToEnemy < closestEnemyDistance){
                    closestEnemyDistance = distanceToEnemy;
                    ClosestEnemy = enemy;
                    ClosestEnemyPrevpos = enemy.transform.position;
                }
            }
        }

        if (enemyList.Length == 0){
            isEnemyFound = false;
        }
    }


    //will handle the Missile movement towards the target
    void MissileMove(){

        //the missile will explode when the enemy it targeted dies
        if(ClosestEnemy == null){
            GameObject effect = Instantiate(missileImpactEffect, transform.position, Quaternion.identity);
            Destroy(effect,2.0f);
            Destroy(this.gameObject);
        }

        Vector3 direction = transform.position - ClosestEnemyPrevpos;
        
        direction = -direction.normalized;
        transform.rotation = Quaternion.LookRotation(transform.forward, direction);
        transform.position += direction *missileSpeed *Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null){
            enemy.TakeDamage(damage);

            GameObject effect = Instantiate(missileImpactEffect, transform.position, Quaternion.identity);
            Destroy(effect,2.0f);
            Destroy(gameObject);
        }
        
        BossBehavior boss = hitInfo.GetComponent<BossBehavior>();

        if(boss != null){
            boss.TakeDamage(damage);

            GameObject effect = Instantiate(missileImpactEffect, transform.position, Quaternion.identity);
            Destroy(effect,2.0f);
            Destroy(gameObject);
        }
        
    }
}
