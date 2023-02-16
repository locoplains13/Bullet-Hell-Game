using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interceptor : MonoBehaviour, Enemy
{

    private float health;
    private float fire_rate;
    private float fire_time;
    private float dY, dX;
    private float moveSpeed;

    private Vector3 original_position;

    private float exit_time, total_exit_time;

    private float x_change;
    private float x_dir;

    private bool attacking;

    private GameObject Player;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private GameObject Projectile;

    // Start is called before the first frame update
    void Start()
    {
        health = 1;
        transform.Rotate(0, 0, -90);
        moveSpeed = 0.3f;
         fire_rate = 2f;

        total_exit_time = 1f;
        exit_time = 0f;

        float[] x_dirs = { -1, 1 };
        x_dir = x_dirs[Random.Range(0, 2)];
       

        attacking = false;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!UI_Manager.instance.IsGameOver &&  !UI_Manager.instance.IsShopOpen){

        if(Player!=null)
        {

        Move();
        if (attacking) Attack();
        }
        }
    }

    public void Move()
    {

        if (exit_time < total_exit_time)
        {
            transform.position += new Vector3(moveSpeed * x_dir * 2 * Time.deltaTime, -moveSpeed * 3 * Time.deltaTime, 0);
            exit_time += Time.deltaTime;
        } else
        {
            float distance = Vector3.Distance(this.transform.position, Player.transform.position);

            LookAt2DMethod.LookAt2D(transform, Player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, moveSpeed * Time.deltaTime);
            attacking = true;
        }

        
        
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
           // Debug.Log("enemykilled");
            Score_Manager.instance.UpdateScore(5);
            UI_Manager.instance.UpdateEnemyCounter(1);
            BossSpawn.instance.SpawnAfterEnemiesBeat(1);
            Destroy(this.gameObject);
            Instantiate(Explosion, transform.position, transform.rotation);

        }
    }

    public void Attack()
    {
        if (Time.time > fire_time && !UI_Manager.instance.IsGameOver)
        {
            Instantiate(Projectile, transform.position + new Vector3(0.0f, -0.25f, 0.0f), transform.rotation);
            fire_time = Time.time + fire_rate;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PlayerProjectile")
        {
            TakeDamage(1);
            Destroy(other);
        }
    }

}
