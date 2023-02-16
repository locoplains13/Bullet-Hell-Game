using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Manager : MonoBehaviour
{
    
    public static HealthBar_Manager instance;
    public Image health_bar;
    public float current_health;
    public float maxHealth = 100f;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        current_health = maxHealth;
        health_bar.fillAmount = current_health;
    }

    void Update(){
        current_health = Mathf.Clamp(current_health, 0, maxHealth);

        UpdateHealthUI();
    }

    public void UpdateHealthUI(){
        //Debug.Log(current_health);
        float hFraction = current_health / maxHealth;
        health_bar.fillAmount = hFraction;
    }

    public void TakeDamage(float damage){
        current_health -= damage;
    }

    public void TakeHealing(float heal){

        float newHealth = current_health + heal;
        if(newHealth < maxHealth){
            current_health = newHealth;
        }else{
            current_health = maxHealth;
        }
    }

    // public void UpdateHealth_Damage(float update_val)
    // {
    //     update_val= current_health + update_val;
    //  //    Debug.Log("update_val "+update_val);
    //     StartCoroutine(UpdateHealth_Coroutine(update_val));
    //     current_health= update_val;
    // }


    // IEnumerator UpdateHealth_Coroutine(float target_health)
    // {
    //     while (current_health >= target_health)
    //     {
    //         current_health--;   
    //         health_bar.fillAmount = current_health/100;

    //         yield return new WaitForSeconds(0.5f);
    //     }
    // }

}
