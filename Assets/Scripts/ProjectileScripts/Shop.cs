using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public GameObject weapons;
    public GameObject insufficient_Meteors_obj;
    public Text totalMeteorText;
    public AudioClip declinedSound, approvedSound;
    // Start is called before the first frame update
    void OnEnable()
    {
       instance=this;
    }

    // Update is called once per frame
    public void SetDataText()
    {
        totalMeteorText.text="Total Meteor : "+UI_Manager.instance.meteor_counter;
        TurnOffInsuffien_Meteor_obj();
        
    }

    void TurnOffInsuffien_Meteor_obj(){
        insufficient_Meteors_obj.SetActive(false);
    }

    public void increaseGun(){
        if(UI_Manager.instance.meteor_counter>=10)
        {
            UI_Manager.instance.meteor_counter=UI_Manager.instance.meteor_counter-10;
            UI_Manager.instance.MetorCountText.text= UI_Manager.instance.meteor_counter.ToString("0000"); 
            weapons.GetComponent<WeaponContainer>().addGuns();
            UI_Manager.instance.TurnOffTheProp();

            Time.timeScale = 1.0f ;    //wil UNfreeze the game
            AudioSource.PlayClipAtPoint(approvedSound, transform.position);
        }
        else{
            AudioSource.PlayClipAtPoint(declinedSound, transform.position);
            insufficient_Meteors_obj.SetActive(true);
            Invoke("TurnOffInsuffien_Meteor_obj",3);
        }
    }

    public void increaseFireRate(){
        if(UI_Manager.instance.meteor_counter>=5)
        {
            UI_Manager.instance.meteor_counter=UI_Manager.instance.meteor_counter-5;
            UI_Manager.instance.MetorCountText.text= UI_Manager.instance.meteor_counter.ToString("0000"); 
            weapons.GetComponent<WeaponContainer>().addFireRate();
            UI_Manager.instance.TurnOffTheProp();

            AudioSource.PlayClipAtPoint(approvedSound, transform.position);
            Time.timeScale = 1.0f ;    //wil UNfreeze the game
        }
        else{
            AudioSource.PlayClipAtPoint(declinedSound, transform.position);
            insufficient_Meteors_obj.SetActive(true);
            Invoke("TurnOffInsuffien_Meteor_obj",3);
        }
    } 
    
    public void LessDamage(){
        if(UI_Manager.instance.meteor_counter>=3)
        {
            UI_Manager.instance.meteor_counter=UI_Manager.instance.meteor_counter-3;
            UI_Manager.instance.MetorCountText.text= UI_Manager.instance.meteor_counter.ToString("0000"); 
            UI_Manager.instance.LessDamage=true;
            UI_Manager.instance.TurnOffTheProp();

            Time.timeScale = 1.0f ;    //wil UNfreeze the game
            AudioSource.PlayClipAtPoint(approvedSound, transform.position);
        }
        else{
            AudioSource.PlayClipAtPoint(declinedSound, transform.position);
            insufficient_Meteors_obj.SetActive(true);
            Invoke("TurnOffInsuffien_Meteor_obj",3);
        }
    }
    public void ExitShop(){

        UI_Manager.instance.TurnOffTheProp();
        AudioSource.PlayClipAtPoint(approvedSound, transform.position);

        Time.timeScale = 1.0f ;    //wil UNfreeze the game
    }
}
