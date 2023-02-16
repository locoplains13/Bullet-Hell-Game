using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float fireRate = 0.1f;
    private float fireTime; 

    public void Attack()
    {
        if (Time.time > fireTime)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            fireTime = Time.time + fireRate;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        Attack();
    }
}
