using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffDrop : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dropForce = 2.0f;
    public GameObject debuffEffect;

    private float lifeTime = 10f;

    public AudioClip debuffSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);

         Destroy(this.gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        
        
        if(hitInfo.gameObject.tag == "Player"){

            hitInfo.GetComponent<Player>().deactivateShield();              //deactivates shield
            hitInfo.GetComponent<PlayerMovement>().DebuffPickedUp();        //The player will be slowed
            hitInfo.GetComponent<WeaponContainer>().DebuffPickedUp();       //The fireRate will be slowed

            AudioSource.PlayClipAtPoint(debuffSound,transform.position);

            GameObject effect =  Instantiate(debuffEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1.0f);
            Destroy(gameObject);
        }
    }
}
