using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    This class is the Enemy Projectile type 2
    This projectile will be lunched towards the last known Player position
**/
public class EProjectileType2 : MonoBehaviour
{
    private Transform player;
    private Vector2 target;
    public float speed = 20f;
    private float lifeTime = 10f;
    public int damage = 15;
    public AudioClip clip;

    public bool isAutoAim = true;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);

        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
          if(UI_Manager.instance.IsGameOver &&  !UI_Manager.instance.IsShopOpen){
            Destroy(gameObject);
        }
        moveProjectile();
    }

    private void moveProjectile(){
        transform.position = Vector2.MoveTowards(transform.position, target, (speed / 10f)*Time.deltaTime);
        
        if(transform.position.x == target.x && transform.position.y == target.y){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        
        Player enemy = hitInfo.GetComponent<Player>();
        if(enemy != null){
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        PlayerProjectile playerProjectile = hitInfo.GetComponent<PlayerProjectile>();
        if(playerProjectile != null){
            AudioSource.PlayClipAtPoint(clip, transform.position);
            Destroy(gameObject);
            playerProjectile.destroyPlayerProj();
        }

        OrbitingShield orbitingShield = hitInfo.GetComponent<OrbitingShield>();
        if (orbitingShield != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
            Destroy(gameObject);
            
        }


    }

    public bool getAutoAim(){
        return this.isAutoAim;
    } 
}
