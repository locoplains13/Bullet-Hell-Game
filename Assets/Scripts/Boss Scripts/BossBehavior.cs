using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour, Boss
{

    [SerializeField] float health;
    [SerializeField] private float fire_rate;
    private float fire_time;
    private float dY, dX;
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    private Vector3 localScale;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private GameObject Projectile;

    public GameObject itemDrops;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();

        moveSpeed = 3f;
        
        fire_rate = 0.7f;
        dX = transform.position.x;
        dY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.transform.tag == "PlayerProjectile")
        {
            TakeDamage(1);
            Destroy(collision);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {

            ItemDrop();

            Destroy(this.gameObject);
            Instantiate(Explosion, transform.position, transform.rotation);
            Score_Manager.instance.UpdateScore(20);
            UI_Manager.instance.UpdateEnemyCounter(1);
        }
    }

    public void Attack()
    {
        if (Time.time > fire_time)
        {
            Instantiate(Projectile, transform.position, transform.rotation);
            fire_time = Time.time + fire_rate;
        }
        
    }

    private void ItemDrop(){
        Instantiate(itemDrops, transform.position + new Vector3(0,1,0), Quaternion.identity);
    }
}
