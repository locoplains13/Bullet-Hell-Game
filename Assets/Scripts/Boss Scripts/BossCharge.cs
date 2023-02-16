using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharge : MonoBehaviour, Boss
{
    [SerializeField] public float health;
    [SerializeField] private GameObject Explosion;

    [SerializeField] Transform[] waypoints;

    [SerializeField] private float moveSpeed = 2f;
    public int damage = 10;

    private int waypointIndex = 0;

    public GameObject itemDrops;

    private Rigidbody2D rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        waypoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //move the boss
        if(!UI_Manager.instance.IsGameOver &&  !UI_Manager.instance.IsShopOpen){
            Move();
        }
    }

    void Reposition(){
        Vector3 direction = waypoints[waypointIndex].position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        }

    void Move(){
        transform.position = Vector3.MoveTowards(transform.position,
        waypoints[waypointIndex].transform.position,
        moveSpeed * Time.deltaTime);

        Reposition();

        if(transform.position == waypoints[waypointIndex].transform.position){
            waypointIndex += 1;
        }

        if(waypointIndex == waypoints.Length){
            waypointIndex = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        Player player = hitInfo.GetComponent<Player>();
        if(player != null){
            player.TakeDamage(damage);
        }

        if (hitInfo.transform.tag == "PlayerProjectile")
        {
            TakeDamage(1);
            Destroy(hitInfo);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
            Instantiate(Explosion, transform.position, transform.rotation);
        }
    }
    private void ItemDrop(){
        Instantiate(itemDrops, transform.position + new Vector3(0,1,0), Quaternion.identity);
    }
}
