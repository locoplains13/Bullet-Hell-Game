using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Vector2 direction =  new Vector2(0, -1); 
    public float speed = 5f;
    public Vector2 velocity;
    private float lifeTime = 10f;
    public int damage = 10;
    public Rigidbody2D rb;
    public AudioClip clip;

    private bool isAutoAim = false;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    void Update(){
        if(UI_Manager.instance.IsGameOver &&  !UI_Manager.instance.IsShopOpen){
            Destroy(gameObject);
        }
        velocity = direction * speed;
    }

    private void FixedUpdate(){
        moveProjectile();
    }

    private void moveProjectile(){
        Vector2 pos = transform.position;

        pos += velocity * Time.fixedDeltaTime;
        transform.position = pos;
        // rb.velocity = -transform.up * (speed * 60f) * Time.deltaTime;            //Time.deltaTime??
    }                                                                           //Need to multiply to 60 since using T.dT

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
    }

    public bool getAutoAim(){
        return this.isAutoAim;
    }

    
}
