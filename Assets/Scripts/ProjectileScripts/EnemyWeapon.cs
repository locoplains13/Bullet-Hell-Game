using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    This will spawn the enemy projectiles 
**/
public class EnemyWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int enemyLevel = 1;   //level that determines the power of the
    Vector2 direction1;
    void Start(){
        direction1 = (transform.localRotation * -Vector2.up).normalized;
    }
    
    void Update()
    {

    }

    public void Fire(){

        if (bulletPrefab.GetComponent<EProjectileType2>() != null && bulletPrefab.GetComponent<EProjectileType2>().getAutoAim()){
            GameObject projectile1 = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        } else{
            GameObject projectile1 = Instantiate(bulletPrefab, transform.position, transform.rotation);
            EnemyProjectile moveProjectile = projectile1.GetComponent<EnemyProjectile>();
            moveProjectile.direction = direction1;
        }

        

    }
}
