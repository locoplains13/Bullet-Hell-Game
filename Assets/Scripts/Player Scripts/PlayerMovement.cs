using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    private float startingMoveSpeed = 5.0f;
    [SerializeField] private float moveSpeed = 5.0f;
    private float debuffSPeed = 1f;
    private Vector3 position;

    public AudioClip buffSound;

    // Update is called once per frame
    void Update(){
        if(!UI_Manager.instance.IsShopOpen)
        MovePlayer();

    }

    private void MovePlayer(){
        // Forward & Backward Movement
        if (Input.GetKey(KeyCode.W)) {
            if (transform.position.y <= 4.25){
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.S)) {
            if (transform.position.y >= -4.25){
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.A)) {
            if (transform.position.x >= -7.4f){
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
            
        }


        if (Input.GetKey(KeyCode.D)) {
            if (transform.position.x <= 2.6){//3.8
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
        }
    }

    public void HalveMovespeed() { moveSpeed = startingMoveSpeed / 2; }
    public void UnhalveMovespeed() { moveSpeed = startingMoveSpeed; }


    //////////////////// Debuffs //////////////////////////

    public void DebuffPickedUp(){
        StartCoroutine(DebuffRoutine());
    }

    IEnumerator DebuffRoutine(){
        moveSpeed = debuffSPeed;
        yield return new WaitForSeconds(4f);
        moveSpeed = 5.0f;
         AudioSource.PlayClipAtPoint(buffSound,transform.position);

    }


}
