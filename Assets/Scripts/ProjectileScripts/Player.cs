using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float speed;
    [SerializeField] public int health = 100;
    public int currMaxHealth = 100;
    public GameObject deathEffect;
    public GameObject hitEffect;
    public AudioClip clip;
    public GameObject OrbitingShield;
    private OrbitingShield [] orbitShieldList; 

    GameObject shield;
    private GameObject orbiter1, orbiter2, orbiter3;
    int shieldPower = 1;


    // Start is called before the first frame update
    void Start()
    {
        
        shield = transform.Find("Shield").gameObject;
       
    }

    // Update is called once per frame
    void Update()
    {
       
        if(!UI_Manager.instance.IsShopOpen)
        playerMove();
    }


    private void playerMove(){
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed * Time.deltaTime;
    }


    public void TakeDamage(int damage){
       // Debug.Log("Player took damage. Current health: " + health);
        if( UI_Manager.instance.LessDamage)
        {
            damage=damage-10;
        }
        if(hasShield() && shieldPower > 0){
            shieldPower -= damage;

        } else{
            AudioSource.PlayClipAtPoint(clip, transform.position);
            GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(effect, 0.5f);
            health -= damage;
            HealthBar_Manager.instance.TakeDamage((float) damage);
        }
        
        if(shieldPower <= 0){
            deactivateShield();
        }

        if(health <= 0){
            Die();
            health = 10;

            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
        }

    }


    
    public void Die(){
        GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effect, 1.0f);
        UI_Manager.instance.ActiveGameOver();
        Destroy(this.gameObject);
    }


    public void Heal(int healthToAdd){
        
        int newHealth = this.health + healthToAdd;

        if(newHealth <= currMaxHealth){
            this.health = newHealth;
        }else{
            this.health = currMaxHealth;
        }
        
        HealthBar_Manager.instance.TakeHealing((float) health);

    }

    /////////////////////////////   Shield specs   ////////////////////////////
    public void activateShield(){
        shield.SetActive(true);

        orbiter1 = (GameObject)Instantiate(OrbitingShield, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity, transform);
        orbiter1.GetComponent<OrbitingShield>().SetAngle(90 * 8f);

        orbiter2 = (GameObject)Instantiate(OrbitingShield, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity, transform);
        orbiter2.GetComponent<OrbitingShield>().SetAngle(180 * 8f);

        orbiter3 = (GameObject)Instantiate(OrbitingShield, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity, transform);
        orbiter3.GetComponent<OrbitingShield>().SetAngle(270 * 8f);

    }

    public void deactivateShield(){
        shield.SetActive(false);
        orbitShieldList = transform.GetComponentsInChildren<OrbitingShield>();
        foreach(OrbitingShield shield in orbitShieldList){
            shield.DebuffDestroy();
        }

        
    }

    public bool hasShield(){
        return shield.activeSelf;
    }

    public void resetShieldpower(int power){
        this.shieldPower = power;
    }

}
