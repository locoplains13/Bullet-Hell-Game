using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAmmoDrop : MonoBehaviour
{
    private Rigidbody2D rb;
    private float lifeTime = 10f;
    public float dropForce = 2.0f;
    public GameObject bombEffect;

    public AudioClip bombSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);

        Destroy(this.gameObject, lifeTime);
        
    }


    void OnTriggerEnter2D(Collider2D hitInfo){
        
        
        if(hitInfo.gameObject.tag == "Player"){
            
            hitInfo.GetComponent<WeaponContainer>().addBomb();  //add bomb ammo
            
            
            AudioSource.PlayClipAtPoint(bombSound, transform.position);

            GameObject effect =  Instantiate(bombEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1.0f);
            Destroy(gameObject);
        }
    }
}
