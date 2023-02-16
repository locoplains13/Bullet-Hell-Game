using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType3 : MonoBehaviour, Enemy
{

    private float health;
    private float fire_rate;
    private float fire_time;
    private float dY, dX;
    private float moveSpeed;
    private float radius;
    private float maintainDistance;


    private float angle;
    private float angle_change;
    private bool attacking;


    private GameObject Player;
    [SerializeField] private GameObject Explosion;
    
    //variables for generating random number for itemDrops
    private int randNum;
    public GameObject[] itemDrops;

    // Start is called before the first frame update
    void Start()
    {
        health = 300;
 
        moveSpeed = 0.3f;
        radius = 1.0f;
        dX = transform.position.x;
        dY = transform.position.y;

        fire_rate = 3f;
       
        angle = 0;
        float[] angles = { -2f, -1.5f, -1f, -0.5f, 0.5f, 1f, 1.5f, 2f };
        angle_change = angles[Random.Range(0, 8)];
        maintainDistance = Random.Range(4, 7);
        attacking = false;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!UI_Manager.instance.IsGameOver &&  !UI_Manager.instance.IsShopOpen){
        if (Player != null)
        {

            Move();
            
        }}
        else{
            Destroy(gameObject);
        }
    }

    public void Move()
    {
        float distance = Vector3.Distance(this.transform.position, Player.transform.position);

        //looping movement
        if (distance > maintainDistance)
        {

            angle += angle_change;
            transform.position = new Vector3(radius * Mathf.Cos(angle / 360) + dX, radius * Mathf.Sin(angle / 360) + dY, 0);
            dY -= moveSpeed * 3 * Time.deltaTime;

            if (Random.Range(0, 1000) == 0) angle_change *= -1;

        }
        

    }

    public void Attack()
    {
        return;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        //update color of all child sprites
        transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1, health / 300, health / 300);
        transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1, health / 300, health / 300);
        transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1, health / 300, health / 300);
        transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().material.color = new Color(1, health / 300, health / 300);

        if (health <= 0)
        {
            //Drops:
            GameObject toDrop = randomLootDrop();
            if(toDrop != null){
                ItemDrop(toDrop);
            }

            //Debug.Log("enemykilled");
            Score_Manager.instance.UpdateScore(5);
            UI_Manager.instance.UpdateEnemyCounter(1);
            BossSpawn.instance.SpawnAfterEnemiesBeat(1);
            Destroy(this.gameObject);
            Instantiate(Explosion, transform.position, transform.rotation);
        }
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
