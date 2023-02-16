using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType6 : MonoBehaviour, Enemy
{

    private float health = 600f;
    private float moveSpeed = 1.1f;

    private float switch_direction_rate = 1.2f;
    private float switch_direction_time;

    private int direction;

    private bool moving = true;

    private float spawn_timer = 2.0f;
    private float spawn_rate = 1.1f;
 


    private float radius = 0.2f;
    private float angle = 0.0f;
    private float angle_change = 2.0f;

   

    private Vector3 original_position;


    public GameObject Interceptor;
    public GameObject Explosion;
   



    private GameObject Player;

    //variables for generating random number for itemDrops
    private int randNum;
    public GameObject[] itemDrops;

    // Start is called before the first frame update
    void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player");
        direction = Random.Range(-1, 2);
        original_position = transform.position;



    }

    // Update is called once per frame
    void Update()
    {
        if (!UI_Manager.instance.IsGameOver && !UI_Manager.instance.IsShopOpen)
        {
            Move();
            SpawnInterceptors();



        }
       

    }

    void SpawnInterceptors()
    {

        if (!moving && Time.time > spawn_timer && !UI_Manager.instance.IsGameOver)
        {

       


            Instantiate(Interceptor, original_position + new Vector3(0, -1, 0), transform.rotation);

            spawn_timer = Time.time + spawn_rate;
        }

    }

    public void TakeDamage(float damage)
    {

       
        health -= damage;
        //update color
        gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1, health / 600, health / 600);

        if (health <= 0)
        {
            //Drops:
            GameObject toDrop = randomLootDrop();
            if (toDrop != null)
            {
                ItemDrop(toDrop);
            }

            // Debug.Log("enemykilled");
            Score_Manager.instance.UpdateScore(5);
            UI_Manager.instance.UpdateEnemyCounter(1);
            BossSpawn.instance.SpawnAfterEnemiesBeat(1);
            Destroy(this.gameObject);
            Instantiate(Explosion, transform.position, transform.rotation);
        }
    }

    public void Move()
    {
        //move toward player until a certain distance away
        if (moving)
        {
           

            if (transform.position.y > 2) transform.position += new Vector3(0, -moveSpeed * Time.deltaTime, 0);
            else moving = false;


            transform.position += new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);


            if (Time.time > switch_direction_time)
            {
                if (Random.Range(0, 2) == 0) direction *= -1;
                switch_direction_time = Time.time + switch_direction_rate;
            }

            if (transform.position.x < -7 || transform.position.x > 0) direction *= -1;
            original_position = transform.position;
        }
        else
        {
            angle += angle_change;
            transform.position = original_position + new Vector3(radius * Mathf.Cos(angle / 360), radius * Mathf.Sin(angle / 360), 0);

        }
    }



    public void Attack()
    {
        return;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "PlayerProjectile")
        {
            GetComponent<AudioSource>().Play();
            Destroy(other);
        }
    }


    private void ItemDrop(GameObject drop)
    {
        Instantiate(drop, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
    }

    //Generates a random number that will determine what will drop
    //              - bomb   = 5%  chance
    //              - shield = 10% chance
    //              - Debuff = 15% chance
    //              - Health = 40% chance
    //              - Gem XP = 80% chance
    private GameObject randomLootDrop(){
        randNum = Random.Range(1,101);      //(1- 100)
        List<GameObject> possibleDrop = new List<GameObject>();
            if(randNum <= 5){
                possibleDrop.Add(itemDrops[4]);    // Bomb Ammo
            }else if(randNum <= 10){
                possibleDrop.Add(itemDrops[0]);    // shield
            }else if(randNum <= 15){
                possibleDrop.Add(itemDrops[3]);    // Debuff
            }else if(randNum <= 40){
                possibleDrop.Add(itemDrops[1]);    // Health
            } else if (randNum <= 80){
                possibleDrop.Add(itemDrops[2]);    // Gem XP
        }
        else
        {
            possibleDrop.Add(itemDrops[5]); //Diagonal shooter
        }

        if (possibleDrop.Count >0){
            GameObject toDrop = possibleDrop[Random.Range(0, possibleDrop.Count)];
            return toDrop;
        }

        return null;    //Means no drop
    }

}
