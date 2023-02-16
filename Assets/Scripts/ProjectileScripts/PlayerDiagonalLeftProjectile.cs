using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiagonalLeftProjectile : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 50;
    private float lifeTime = 1f;
    public Rigidbody2D rb;
    public AudioClip clip;


    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
        Destroy(this.gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, speed * Time.deltaTime * 2, 0);
    }

    


    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        Enemy enemy = hitInfo.GetComponent<Enemy>();
        Boss boss = hitInfo.GetComponent<Boss>();
        EnemyProjectile enemyProjectile = hitInfo.GetComponent<EnemyProjectile>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        if (boss != null)
        {
            boss.TakeDamage(damage);
            Destroy(gameObject);
        }

        Destroy(enemyProjectile);

    }

   
}
