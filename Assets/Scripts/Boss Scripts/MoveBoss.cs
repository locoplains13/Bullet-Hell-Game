using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoss : MonoBehaviour
{
    private float dirX;
    private float moveSpeed;
    private Rigidbody2D rb;
    private Vector3 localScale;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        dirX = -1f;
        moveSpeed = 3f;    
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.GetComponent<Wall>()){
            dirX *= -1f;
        }
    }

    void FixedUpdate(){
        if(!UI_Manager.instance.IsGameOver &&  !UI_Manager.instance.IsShopOpen){
            rb.velocity = new Vector3(dirX * moveSpeed, rb.velocity.y);
        }
    }
}
