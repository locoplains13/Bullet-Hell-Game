using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor_behavior : MonoBehaviour
{
    [SerializeField] private GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("selfDestroy", 15);
    }

    void Update(){
        
if(UI_Manager.instance.IsGameOver){
    Destroy(gameObject);
}
    }

    public void TakeDamage(float damage)
    {
            Score_Manager.instance.UpdateScore(2);
            UI_Manager.instance.UpdateMeteorCounter(1);
            Destroy(this.gameObject);
            Instantiate(Explosion, transform.position, transform.rotation);

    }
    // Update is called once per frame
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
      //  Debug.Log("here");
        if (collision.transform.tag == "PlayerProjectile")
        {
            TakeDamage(1);
            Destroy(collision);
        }
    }

    void selfDestroy()
    {
        Destroy(gameObject);
    }
}
