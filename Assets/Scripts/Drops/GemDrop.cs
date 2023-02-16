using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemDrop : MonoBehaviour
{
    private float lifeTime = 10f;
    private float XpPoint = 1f;

    public GameObject gemEffect;

    public AudioClip gemSound;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
        
    }


    void OnTriggerEnter2D(Collider2D hitInfo){
        
        Player player = hitInfo.GetComponent<Player>();
        if(player != null){

            XP_Manager.instance.GainExperience(XpPoint);
            AudioSource.PlayClipAtPoint(gemSound, transform.position);

            GameObject effect =  Instantiate(gemEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1.0f);
            Destroy(gameObject);
        }
    }
}
