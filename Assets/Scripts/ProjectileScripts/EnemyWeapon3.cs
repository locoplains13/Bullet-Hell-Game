using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    This class will handle the Enemy Projectile type 3
    This projectile will be lunched at three different rotational direction 
**/

public class EnemyWeapon3 : MonoBehaviour
{
    public Transform firepoint1;
    public Transform firepoint2;
    public Transform firepoint3;

    Vector2 direction1;
    Vector2 direction2;
    Vector2 direction3;
    

    public GameObject bulletPrefab;
    private float timer;
    public int waitingTime = 1;  // seconds; how many projectile should spawn
    private int randNum, lastNum;   //variables for generating random number
    private int maxAttempts = 5;


    void Start(){
        direction1 = (firepoint1.localRotation * -Vector2.up).normalized;
        direction2 = (firepoint2.localRotation * -Vector2.up).normalized;
        direction3 = (firepoint3.localRotation * -Vector2.up).normalized;

        spawner(); // spawn them on the time they are spawned
        randomProjectileNumber();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitingTime){
            spawner();
            timer = 0;
        }
    
    }

    void spawner(){
        GameObject projectile1 = Instantiate(bulletPrefab, firepoint1.position, firepoint1.rotation);
        EnemyProjectile moveProjectile = projectile1.GetComponent<EnemyProjectile>();
        moveProjectile.direction = direction1;

        GameObject projectile2 = Instantiate(bulletPrefab, firepoint2.position, firepoint2.rotation);
        EnemyProjectile moveProjectile2 = projectile2.GetComponent<EnemyProjectile>();
        moveProjectile2.direction = direction2;
            

        GameObject projectile3 = Instantiate(bulletPrefab, firepoint3.position, firepoint3.rotation);
        EnemyProjectile moveProjectile3 = projectile3.GetComponent<EnemyProjectile>();
        moveProjectile3.direction = direction3;
        
    }

    //generates random number for projectile to spawn 
    public void randomProjectileNumber (){

        for(int i = 0; randNum == lastNum && i < maxAttempts; i++ ){
            randNum = Random.Range(1,5);
        }
        lastNum = randNum;
        waitingTime = randNum;

        Debug.Log(waitingTime);

    }
}
