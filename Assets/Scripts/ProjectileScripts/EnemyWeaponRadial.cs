using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    This will spawn the enemy projectiles in a radial:
            - can change the number of projectile to spawn
**/
public class EnemyWeaponRadial : MonoBehaviour
{
    [SerializeField] int numberOfProjectiles;
    [SerializeField] GameObject projectile;

    Vector2 startPoint;

    float radius, moveSpeed;

    private float timer;
    public int waitingTime = 1;  // seconds; how many projectile should spawn
    private int randNum, lastNum;   //variables for generating random number
    private int maxAttempts = 5;

    // Start is called before the first frame update
    void Start()
    {
        radius = 5f;
        moveSpeed = 5f;
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!UI_Manager.instance.IsGameOver)
        {

        randomProjectileNumber();
        
        timer += Time.deltaTime;
        if(timer > waitingTime){
            Fire(numberOfProjectiles);
            timer = 0;
        }
        }
       
    }

    void Fire(int numberOfProjectiles){

        startPoint = new Vector2(transform.position.x, transform.position.y); //DO NOT REMOVE PLS -Ehsan

        float anglestep = 360f / numberOfProjectiles;
        float angle = 0f;

        for(int i = 0; i <=numberOfProjectiles - 1; i++){
            float projectileDirXposition = startPoint.x + Mathf.Sin ((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = startPoint.y + Mathf.Cos ((angle * Mathf.PI) / 180) * radius;
        
            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

            var proj = Instantiate(projectile, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2 (projectileMoveDirection.x, projectileMoveDirection.y);

            angle += anglestep;
        }   

    }

    //generates random number for projectile to spawn 
    public void randomProjectileNumber (){

        for(int i = 0; randNum == lastNum && i < maxAttempts; i++ ){
            randNum = Random.Range(3,11);
        }
        lastNum = randNum;
        this.numberOfProjectiles = randNum;

        //Debug.Log(waitingTime);

    }
}
