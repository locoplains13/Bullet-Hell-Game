using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XP_Manager : MonoBehaviour
{

    public static XP_Manager instance;
    public int level = 1;
    public float currentXp;
    public float requiredXp = 5;

    private float lerpTimer;
    private float delayTimer;

    [Header("UI")]
    public Image frontXpBar;
    public Image backXpBar;

    public GameObject currentGems;      //text

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        frontXpBar.fillAmount = currentXp / requiredXp;
        backXpBar.fillAmount = currentXp / requiredXp;
    }

    // Update is called once per frame
    void Update()
    {
        updateXpUi();
        

        if(currentXp > requiredXp){
            levelUP();
        }
        
    }

    public void updateXpUi(){
        float xp = currentXp / requiredXp;
        float frontXp = frontXpBar.fillAmount;

        if (frontXp < xp ){
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xp;

            currentGems.GetComponent<Text>().text = currentXp + " / " + requiredXp; 

            if(delayTimer > 3){
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 3;  //animation 4 scs
                frontXpBar.fillAmount = Mathf.Lerp(frontXp, backXpBar.fillAmount, percentComplete);

            }
            

        }
    }

    public void GainExperience (float xpGained){
        currentXp += xpGained;
        lerpTimer = 0f;
    } 

    // Handles what will change when the player Level Up
    public void levelUP(){
        level++;
        //open shop
        UI_Manager.instance.IsShopOpen=true;
        UI_Manager.instance.Shope_panel.SetActive(true);
        Shop.instance.SetDataText();
        frontXpBar.fillAmount = 0f;
        backXpBar.fillAmount = 0f;

        Time.timeScale = 0f; // Pauses 


        currentXp = Mathf.RoundToInt(currentXp - requiredXp);

        requiredXp += 5; //increase the required XP;
    }
}
