using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingShield : MonoBehaviour
{

        private float dX = 0;
        private float dY = 0;
        private float radius = 1f;
        private float rotateSpeed = 6.0f;
        private float angle = 0;
        private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        TimedDestroy();
      
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

    public void TimedDestroy()
    {
        timer += Time.deltaTime;
        if (timer > 15) Destroy(gameObject);
    }

    public void DebuffDestroy(){
        Destroy(gameObject);
    }
   

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "EnemyProjectile")
        {

            Destroy(other);
            Destroy(gameObject);
        }
    }
}
