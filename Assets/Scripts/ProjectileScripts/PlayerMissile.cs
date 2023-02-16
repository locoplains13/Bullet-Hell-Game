using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    A player missile will be added to a missile prefab
    This will find the first nearest enemy and will target it 
    This will be an Auto attack where the player will not have to press a button to fire
**/
public class PlayerMissile : MonoBehaviour
{
    //Bool switch for the missile CD
    private bool isOnCoolDown;

    public GameObject missilePrefab;

    //for Timer CD
    private float cdTimer = 10f;

    // Update is called once per frame
    void Update()
    {
        if( isOnCoolDown == false){
            Fire();
        }
    }


    // Will fire the missile 
    public void Fire(){

        Instantiate(missilePrefab, transform.position, Quaternion.identity);
        MissileProjectile missileMovement = GameObject.Find("PlayerMissile(Clone)").GetComponent<MissileProjectile>();

        if(missileMovement != null){
            missileMovement.FindClosestEnemy();
        }

        StartCoroutine(PlayerMissileCoolDownRoutine(cdTimer));
    }

    IEnumerator PlayerMissileCoolDownRoutine(float time){
        isOnCoolDown = true;
        yield return new WaitForSeconds(time);
        isOnCoolDown = false;
    }

    public void uprgadeCdTimer(float newTime){
        cdTimer = newTime;
    }

    public float getCurrCdTimer(){
        return cdTimer;
    }


}
