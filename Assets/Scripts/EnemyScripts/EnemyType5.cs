using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType5 : MonoBehaviour, Enemy
{

    private float health;
    private float moveSpeed;
    private float switch_direction_rate;
    private float switch_direction_time;
    private float timer;
    private float fire_rate;
    private float fire_time;


    private int direction;
   


    public GameObject OrbiterPrefab;
    private GameObject orbiter1, orbiter2, orbiter3;

    [SerializeField] private GameObject randomShooter;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private GameObject ShieldPrefab;


    private GameObject Shield;
    private GameObject Player;

    //variables for generating random number for itemDrops
    private int randNum;
    public GameObject[] itemDrops;

    // Start is called before the first frame update
    void Start()
    {
        health = 250;
        moveSpeed = 1.5f;
         fire_rate = 3f;
        switch_direction_rate = 1.2f;
        timer = 0;
        direction = Random.Range(-1, 2);
        Player = GameObject.FindGameObjectWithTag("Player");
        Shield = (GameObject)Instantiate(ShieldPrefab, transform.position, Quaternion.identity, transform);
        SpawnOrbiters();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!UI_Manager.instance.IsGameOver &&  !UI_Manager.instance.IsShopOpen){
        Move();
        DestroyShield();} else{
            Destroy(gameObject);
        }
        
    }

    void SpawnOrbiters()
    {
        
       
        orbiter1 = (GameObject) Instantiate(OrbiterPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity, transform);
        orbiter1.GetComponent<Orbiter>().SetAngle(90 * 8f);

        orbiter2 = (GameObject)Instantiate(OrbiterPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity, transform);
        orbiter2.GetComponent<Orbiter>().SetAngle(180 * 8f);

        orbiter3 = (GameObject)Instantiate(OrbiterPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity, transform);
        orbiter3.GetComponent<Orbiter>().SetAngle(270 * 8f);

    }

    public void TakeDamage(float damage)
    {

        if (orbiter1 == null && orbiter2 == null && orbiter3 == null) { 
            health -= damage;
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1, health / 250, health / 250);
        }
        if (health <= 0)
        {
            //Drops:
            GameObject toDrop = randomLootDrop();
            if(toDrop != null){
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
        float distance = Vector3.Distance(this.transform.position, Player.transform.position);

        if (distance > 6.5) transform.position += new Vector3(0, -moveSpeed * Time.deltaTime, 0);
        //if (distance < 2.5 && transform.position.x < 3) transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);

        
        transform.position += new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);
        timer += Time.deltaTime;

        if (timer > switch_direction_time)
        {
            if (Random.Range(0, 2) == 0) direction *= -1;
            switch_direction_time = Time.time + switch_direction_rate;
            timer = 0;
        }

        if (transform.position.x < -7 || transform.position.x > 0) direction *= -1;
    }

    public void DestroyShield()
    {
        if (orbiter1 == null && orbiter2 == null && orbiter3 == null && Shield != null)
        {
            Destroy(Shield);
            Instantiate(randomShooter, transform.position, transform.rotation, transform);
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


    private void ItemDrop(GameObject drop){
        Instantiate(drop, transform.position + new Vector3(0,1,0), Quaternion.identity);
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
