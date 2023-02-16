using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 50;
    private float lifeTime = 1f; 
    public Rigidbody2D rb;
    public AudioClip clip;


    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
        Destroy(this.gameObject, lifeTime);
    }

    void Update(){
      
    }

    private void FixedUpdate(){
         if(!UI_Manager.instance.IsShopOpen)
        rb.velocity = transform.up * (speed*60) * Time.deltaTime;
    }


    void OnTriggerEnter2D(Collider2D hitInfo){
        
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        Boss boss = hitInfo.GetComponent<Boss>();
        EnemyProjectile enemyProjectile = hitInfo.GetComponent<EnemyProjectile>();

        if(enemy != null){
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        if(boss != null){
            boss.TakeDamage(damage);
            Destroy(gameObject);
        }
        
    }

    public void destroyPlayerProj(){
        Destroy(gameObject);
    }

}
