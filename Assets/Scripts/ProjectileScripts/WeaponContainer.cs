using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour
{   
    PlayerWeapon[] guns;

    public GameObject firepoint;                    //For bomb and missile 
    
    [SerializeField] public int powerUpGunLevel = 0;
    [SerializeField] public GameObject playerProjectile1;
    [SerializeField] public GameObject playerDiagonalLeft;
    [SerializeField] public GameObject playerDiagonalRight;
    [SerializeField] public GameObject playerBomb;
    private GameObject Player;
    

    //For weapon fire rate (#/s)
    [SerializeField] public float fireRate = 0.15f;
    private float diagonalFireRate = 0.0f;
    private float diagonalFireTime = 0.0f;
    private int maxFireRateUpgrade = 4;
    private int currFireRateUpgrade = 1;

    //For bomb specifications 
    [SerializeField] public int MaxBomb = 3;    
    [SerializeField] private int currBomb;

    //For Missile specificaitons
    private bool isMissileUnlocked = false; 
    private int maxMissileUpgrade = 5;
    private int currMissileUprade = 1;

    //For Debuff specifications
    private float debuffFireRate = 1f;

    // Start is called before the first frame update
    void Start()
    {   
        Player = GameObject.FindGameObjectWithTag("Player");
        guns = transform.GetComponentsInChildren<PlayerWeapon>();
        foreach(PlayerWeapon gun in guns){
            if(gun.powerUpLevelReq != 0){
                gun.gameObject.SetActive(false);
            }
        }

        currBomb = 1;
        UI_Manager.instance.UpdateBombAmmo(currBomb, MaxBomb);

        if(isMissileUnlocked){
            activateMissile();
        }

        InvokeRepeating("fireWeapon", 0.001f, fireRate);
        

        
    }

    // Update is called once per frame
    void Update()
    {   
        
        //  if(Input.GetButtonDown("Fire1")){
        //     InvokeRepeating("fireWeapon", 0.001f, fireRate);
        //     Player.GetComponent<PlayerMovement>().HalveMovespeed();


        // }else if(Input.GetButtonUp("Fire1")){
        //     CancelInvoke("fireWeapon");
        //     Player.GetComponent<PlayerMovement>().UnhalveMovespeed();

        if(Input.GetKeyDown(KeyCode.LeftControl)){
            fireBomb();
        }

        fireDiagonal();
    }

    ///////////////////// Player Weapon //////////////////////////
    public void fireWeapon(){
        foreach(PlayerWeapon gun in guns){
            if(gun.gameObject.activeSelf){
                gun.Shoot();
            }
        }

    }


    //////////////////// Special weapon //////////////////////////

    public void fireBomb(){
        if(currBomb > 0){
            Instantiate(playerBomb, firepoint.transform.position, Quaternion.identity);
            currBomb--;

            UI_Manager.instance.UpdateBombAmmo(currBomb, MaxBomb);
        }
    }

    //Handles the acitivation of missile,
    // If the missile has already been unlocked, it will receive an upgrade instead
    // upgrade will lower the CD of the Missile;
    public void activateMissile(){
        
        if(!isMissileUnlocked){
            //PlayerMissile missile = transform.GetComponentInChildren<PlayerMissile>();
            PlayerMissile missile = transform.GetChild(6).GetComponent<PlayerMissile>();
            if(missile != null){
                missile.gameObject.SetActive(true);
                isMissileUnlocked = true;
            } 
        } else{
            upgradeMissile();
        }

    }

    //Handles the missile upgrades, Missile can only be upgraded 5 times;
    private void upgradeMissile(){
        PlayerMissile missile = transform.GetChild(6).GetComponent<PlayerMissile>();

        if(missile != null){
            if(currMissileUprade <= maxMissileUpgrade){
                float newTimer = missile.getCurrCdTimer() - 1.0f;
                missile.uprgadeCdTimer(newTimer);
                currMissileUprade++;
            }
        } 

        Debug.Log(missile.getCurrCdTimer());
    }


    //////////////////// Shop Upgrade //////////////////////////
    public void addGuns(){
        powerUpGunLevel++;
        foreach(PlayerWeapon gun in guns){
            if(gun.powerUpLevelReq == powerUpGunLevel){
                gun.gameObject.SetActive(true);
            }
        }
    }

    public void addFireRate(){
        if(currFireRateUpgrade <= maxFireRateUpgrade){

            CancelInvoke("fireWeapon");
            fireRate -= 0.025f;
            currFireRateUpgrade++;
            InvokeRepeating("fireWeapon", 0.001f, fireRate);

            //Debug.Log(fireRate);
        } else{
            Debug.Log("reached Maximum firerate");
        }
    }

    public void addDiagonalRate()
    {

        if (diagonalFireRate == 0.0) diagonalFireRate = 1.0f;
        else
        {
            diagonalFireRate -= 0.05f;
        }

        
    }

    public void fireDiagonal()
    {
        if (diagonalFireRate != 0.0f && diagonalFireTime >= diagonalFireRate)
        {
            Instantiate(playerDiagonalLeft, transform.position, transform.rotation);
            Instantiate(playerDiagonalRight, transform.position, transform.rotation);
            diagonalFireTime = 0.0f;
        }

        diagonalFireTime += Time.deltaTime;
        
    }


    public void addBomb(){
        if(currBomb < MaxBomb){
            currBomb++;
        }
        UI_Manager.instance.UpdateBombAmmo(currBomb, MaxBomb);
    }


    public void increaseMaxBomb(){
        if(MaxBomb <=5){
            MaxBomb++;
        }
        UI_Manager.instance.UpdateBombAmmo(currBomb, MaxBomb);
        
    }

    //////////////////// Debuffs //////////////////////////

    public void DebuffPickedUp(){
        StartCoroutine(DebuffRoutine());
    }

    IEnumerator DebuffRoutine(){

        CancelInvoke("fireWeapon");
        InvokeRepeating("fireWeapon", 0.001f, debuffFireRate);

        yield return new WaitForSeconds(4f);

        CancelInvoke("fireWeapon");
        InvokeRepeating("fireWeapon", 0.001f, fireRate);
        
    }

}
