using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileDiagon : MonoBehaviour
{

    [SerializeField] public float moveSpeed = 300f;
    [SerializeField] private float life_time = 10f;
    [SerializeField] public int damage = 10;

    public Rigidbody2D rb;
    public Vector2 angle;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, life_time);
        angle = new Vector3(0.5f,0,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        moveProjectile();
    }

    private void moveProjectile(){
        transform.position = Quaternion.Euler(0, 45, 0) * Vector3.forward;   
        }

    void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Player") {
            other.GetComponent<Player>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
    
}
