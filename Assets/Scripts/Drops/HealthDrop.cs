using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dropForce = 2.0f;

    public int heal = 10;

    public GameObject healthEffect;

    private float lifeTime = 10f;

    public AudioClip heathSound;

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

            player.GetComponent<Player>().Heal(heal);
            AudioSource.PlayClipAtPoint(heathSound, transform.position);
            
            GameObject effect =  Instantiate(healthEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1.0f);
            Destroy(gameObject);
        }
    }
}
