using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    This will spawn the enemy projectiles Randomly:
            - speed and direction will be random
**/
public class EnemyWeaponRandom : MonoBehaviour
{
    [SerializeField] private GameObject projectile;


    private float projectileSpeed, randomDirection, randomSpeed;
    Vector2 normDirection;

    private float timer;
    public int waitingTime = 1;  // seconds; how many projectile should spawn

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitingTime){
            Fire();
            timer = 0;
        }
        
    }

    public void Fire(){
        for(int i = 0; i < 4 ; i++){
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            randomDirection = Random.Range(-0.5f, 0.5f);
            randomSpeed = Random.Range(1f, 5f);
            
            Vector2 newDirection = new Vector2(0f + randomDirection, 1f);

            EnemyProjectile moveProjectile = newProjectile.GetComponent<EnemyProjectile>();
            moveProjectile.direction = -newDirection;
            moveProjectile.speed = randomSpeed;

        }
    }
}
