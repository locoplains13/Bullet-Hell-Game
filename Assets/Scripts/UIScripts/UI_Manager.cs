using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    public Text Game_Ui_scores, Game_ui_highScores, Game_Win_highscores, Game_Win_scores, Game_Lose_highScores, Game_Lose_Scores;//scores text
    public Text MetorCountText, EnemyCountText,GameLose_MetorCountText, GameLose_EnemyCountText, GameWin_MetorCountText, GameWin_EnemyCountText;
    public Image[] lives;
    
    public Text bombAmmoDisplay;

    public int EnemyCount;
    public int meteor_counter;
    public int Remanin_lives;

    public GameObject gamePlayUI, gameWinUI, gameLoseUI,Shope_panel;
    public bool IsGameOver,IsShopOpen;
public bool DoubleFireRate,Double_Bullets,LessDamage;

    public Text Timer_;
    int timer_int=60;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
            StartCoroutine(GameWinTimer());
    }

    public void UpdateEnemyCounter(int update_val)
    {
        EnemyCount = EnemyCount + update_val;
        EnemyCountText.text = EnemyCount.ToString("0000");
    }
    public void UpdateMeteorCounter(int update_val)
    {
        meteor_counter = meteor_counter + update_val;
        MetorCountText.text = meteor_counter.ToString("0000");
    }

    public void Updata_life(int update_val)
    {
        Remanin_lives = Remanin_lives + update_val;
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i].color = Color.black;
        }
        if (Remanin_lives > lives.Length) Remanin_lives = lives.Length;
        for (int i = 0; i < Remanin_lives; i++)
        {
            lives[i].color = Color.white;

        }
        if (Remanin_lives <= 0)
        {
            ActiveGameOver();
        }
    }
    public void UpdateBombAmmo(int currAmmo , int maxAmmo)
    {
        bombAmmoDisplay.text = currAmmo + " / " + maxAmmo;
        
    }

    public void TurnProps(){
           Timer_.gameObject.SetActive(false);
        LessDamage=false;
        Double_Bullets=false;
        DoubleFireRate=false;
    }

    public void TurnOffTheProp()
    {
        timer_int=60;
        if(LessDamage)
        {
             Timer_.gameObject.SetActive(true);
        StartCoroutine(Start_PropTimer());
        }

        IsShopOpen=false;
        Shope_panel.SetActive(false);
        Invoke("TurnProps",60);
    }

    public void ActiveGameOver()
    {
        IsGameOver=true;
        SetTexts_for_results();
        gamePlayUI.SetActive(false);
        gameLoseUI.SetActive(true);

    }
    public void ActiveGameWin()
    {
        SetTexts_for_results();
        gamePlayUI.SetActive(false);
        gameWinUI.SetActive(true);

    }

    void SetTexts_for_results()
    {
        Game_Win_scores.text = Score_Manager.instance.Current_scores.ToString("000000000");
        Game_Win_highscores.text = PlayerPrefs.GetInt("highscore").ToString("000000000");

        Game_Lose_Scores.text = Score_Manager.instance.Current_scores.ToString("000000000");
        Game_Lose_highScores.text = PlayerPrefs.GetInt("highscore").ToString("000000000");

        GameLose_MetorCountText.text = meteor_counter.ToString("0000"); 
        GameLose_EnemyCountText.text = EnemyCount.ToString("0000"); 
        GameWin_MetorCountText.text = meteor_counter.ToString("0000"); 
        GameWin_EnemyCountText.text = EnemyCount.ToString("0000"); 

    }

    IEnumerator Start_PropTimer(){
   //     Debug.Log("in coroutine");
       
        while(timer_int > -1){
            timer_int=timer_int-1;
            Timer_.text="00:"+timer_int.ToString();
            yield return new WaitForSeconds(1);
    }
  

    }

    public int GameTime=0;
    public int intended_Win_time=0;
    public Text gameWinTimer_text;
    


     IEnumerator GameWinTimer(){
   //     Debug.Log("in coroutine");
       
        while(GameTime < intended_Win_time && !IsGameOver ){
            if(!IsShopOpen)
            {

            GameTime=GameTime+1;
            int minutes = Mathf.FloorToInt(GameTime / 60F);
            int seconds = Mathf.FloorToInt(GameTime - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            gameWinTimer_text.text="Game Timer : "+niceTime;
            }

            yield return new WaitForSeconds(1);
    }
    gameWinUI.SetActive(true);
    IsGameOver=true;
  

    }
}
