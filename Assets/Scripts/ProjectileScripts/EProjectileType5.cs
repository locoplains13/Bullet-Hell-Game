using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EProjectileType5 : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] public int damage = 15;
    public AudioClip clip;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }
    void Update(){
          if(UI_Manager.instance.IsGameOver &&  !UI_Manager.instance.IsShopOpen){
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

        
    }
}
