using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : MonoBehaviour, Enemy
{

    private float health;
    private float fire_rate;
    private float fire_time;
    private float dY, dX;
    private float moveSpeed;
    private float radius;


    private float angle;
    private float angle_change;
    private bool attacking;


    private GameObject Player;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private GameObject Projectile;

    //variables for generating random number for itemDrops
    private int randNum;
    public GameObject[] itemDrops;

    // Start is called before the first frame update
    void Start()
    {
        health = 1;
        transform.Rotate(0, 0, -90);
        moveSpeed = 0.1f;
        radius = 3.0f;
        fire_rate = 3f;
        dX = transform.position.x;
        dY = transform.position.y;
        angle = 0;
        float[] angles = { -1.0f, -0.5f, 0.5f, 1.0f};
        angle_change = angles[Random.Range(0, 4)];
        attacking = true;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!UI_Manager.instance.IsGameOver &&  !UI_Manager.instance.IsShopOpen){

        Move();
        if (attacking) Attack();

        }
     }

    public void Move()
    {
        float distance = Vector3.Distance(this.transform.position, Player.transform.position);
    
        angle += angle_change;
        transform.position = new Vector3(radius * Mathf.Cos(angle / 360) + dX, radius * Mathf.Sin(angle / 360) + dY, 0);
        dY -= moveSpeed * 3 * Time.deltaTime;
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //Drops:
            GameObject toDrop = randomLootDrop();
            if(toDrop != null){
                ItemDrop(toDrop);
            }


            Score_Manager.instance.UpdateScore(5);
            UI_Manager.instance.UpdateEnemyCounter(1);
            BossSpawn.instance.SpawnAfterEnemiesBeat(1);
            Destroy(this.gameObject);
            Instantiate(Explosion, transform.position, transform.rotation);
        }
    }

    public void Attack()
    {
        if (Mathf.Abs(Player.transform.position.x - transform.position.x) <= 0.3 && Time.time > fire_time)
        {
            Instantiate(Projectile, transform.position, transform.rotation);
            fire_time = Time.time + fire_rate;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PlayerProjectile")
        {
            
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
