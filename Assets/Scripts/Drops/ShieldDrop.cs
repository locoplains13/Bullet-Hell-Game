using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDrop : MonoBehaviour
{

    private Rigidbody2D rb;
    public float dropForce = 2.0f;
    public GameObject uprgadeEffect;

    private float lifeTime = 10f;

    public AudioClip shieldSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);

         Destroy(this.gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        
        Player player = hitInfo.GetComponent<Player>();
        if(player != null){

            player.GetComponent<Player>().activateShield();
            player.GetComponent<Player>().resetShieldpower(1);

            AudioSource.PlayClipAtPoint(shieldSound,transform.position);

            GameObject effect =  Instantiate(uprgadeEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1.0f);
            Destroy(gameObject);
        }
    }
}
