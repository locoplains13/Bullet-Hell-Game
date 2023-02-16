using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour, Enemy
{
    private float health;
    private float dY, dX;
    private float radius;
    private float rotateSpeed;
    private float fire_rate;
    private float fire_time;

    private float angle = 0;

    [SerializeField] private GameObject Explosion;
    [SerializeField] private GameObject Projectile;


    // Start is called before the first frame update
    void Start()
    {

        health = 1;
        radius = 1.2f;
        dX = 0;
        dY = 0;
        rotateSpeed = 2.0f;
        fire_rate = 0.7f;


    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        Attack();
        
   
    }

    public void SetAngle(float a)
    {
        angle = a;
    }

    //orbit and rotate
    public void Move()
    {
        angle += 1;
        transform.position = new Vector3(radius * Mathf.Cos(angle / 360) + dX, radius * Mathf.Sin(angle / 360) + dY, 0);
        transform.position += new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, 0);

    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, -50) * rotateSpeed * Time.deltaTime);
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

    public void Attack()
    {
        if (Time.time > fire_time)
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

}
