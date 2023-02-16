using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{

    [SerializeField] public float moveSpeed = 300f;
    [SerializeField] private float life_time = 10f;
    [SerializeField] public int damage = 10;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, life_time);
    }

    // Update is called once per frame
    void Update()
    {
        moveProjectile();
    }

    private void moveProjectile(){
        rb.velocity = -transform.up * (moveSpeed*60) * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Player") {
            other.GetComponent<Player>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
    
}
