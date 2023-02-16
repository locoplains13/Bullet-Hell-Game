using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
    This is for the Player bomb projectile 
    On first contact with enemy it will explode and instantly kill the enemy in the "Field fo Impact" 
**/
public class PlayerBombProjectile : MonoBehaviour
{

    public Vector2 direction =  new Vector2(0,1);

    [SerializeField] public float speed = 5f;
    private Vector2 velocity;
    private float lifeTime = 10f;
    [SerializeField] public int damage = 100;

    [SerializeField] public float fieldOfImpact = 1;

    public GameObject explosionEffect;
    

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed;
    }

    private void FixedUpdate(){
        moveProjectile();
    }

    private void moveProjectile()
    {
        Vector2 pos = transform.position;
        
        pos += velocity * Time.fixedDeltaTime;
        transform.position = pos;
    }
    

    void OnTriggerEnter2D(Collider2D hitInfo){
        
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null){
            Explode();
            GameObject effect = Instantiate(explosionEffect,transform.position, transform.rotation);
            Destroy(effect, 2f);
            Destroy(gameObject);
        }

        BossBehavior boss = hitInfo.GetComponent<BossBehavior>();

        if(boss != null){

            boss.TakeDamage(damage); 

            GameObject effect = Instantiate(explosionEffect,transform.position, transform.rotation);
            Destroy(effect, 2f);
            Destroy(gameObject);
        }
        
        
    }


    void Explode(){
        var enemyHits = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact);

        foreach(var enemyHit in enemyHits){
            Enemy enemy = enemyHit.GetComponent<Enemy>();
            if(enemy != null){
                enemy.TakeDamage(damage);
            }

        }
    }
}
