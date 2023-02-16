using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
    This class will be the container for the Enemy weapon
    depending on the lvl of the enemy it will modify the projectiles:
            - number
            - speed 
            - damage 

**/
public class EnemyWeaponCont : MonoBehaviour
{
    public int currentEnemyLevel = 1;       //deafault is level 1

    EnemyWeapon[] projectileList;

    //projectile customization 
    private float timer;
    public int waitingTime = 1;  // seconds; how many projectile should spawn
    private int randNum, lastNum;   //variables for generating random number
    private int maxAttempts = 5;


    // Start is called before the first frame update
    void Start()
    {
        projectileList = transform.GetComponentsInChildren<EnemyWeapon>();
        foreach(EnemyWeapon projectile in projectileList){
            if(projectile.enemyLevel != currentEnemyLevel){
                projectile.gameObject.SetActive(false);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitingTime){
            foreach(EnemyWeapon projectile in projectileList){
                if(projectile.gameObject.activeSelf){
                    projectile.Fire();
                }
            }
            timer = 0;
        }
        
    }

    //generates random number for projectile to spawn 
    public void randomProjectileNumber (){

        for(int i = 0; randNum == lastNum && i < maxAttempts; i++ ){
            randNum = Random.Range(1,5);
        }
        lastNum = randNum;
        waitingTime = randNum;

        //Debug.Log(waitingTime);

    }

    //a setter that levelups the Enemy
    public void levelUp(int newLevel){
        this.currentEnemyLevel = newLevel;
    }
}
