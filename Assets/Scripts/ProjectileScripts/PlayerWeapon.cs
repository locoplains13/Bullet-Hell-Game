 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    //public Transform firePoint;
    public GameObject projectilePrefab;

    public int powerUpLevelReq = 0;         //for upgrades in shop

    //shooting logic
    public void Shoot(){
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);

    }

    public void increaseFireRate(){
        projectilePrefab.GetComponent<PlayerProjectile>().speed += 5;
        Debug.Log("current fireRate: " + projectilePrefab.GetComponent<PlayerProjectile>().speed);
    }

    public float getProjectileSpeed(){
        return  projectilePrefab.GetComponent<PlayerProjectile>().speed;
    }
}
