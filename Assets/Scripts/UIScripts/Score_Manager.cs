using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Manager : MonoBehaviour
{
    public static Score_Manager instance;//making it static so that every script can access its functionality from this instance
    public int Current_scores;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//this object should not be destoryed while changing the scene(because its save data)
        }
        else
        {
            Destroy(gameObject);
        }
        Invoke("SetHighScore",1);
    }
    void SetHighScore()
    {
        UI_Manager.instance.Game_ui_highScores.text = PlayerPrefs.GetInt("highscore").ToString("000000000");
    }

    public void UpdateScore(int scores_)
    {
        scores_=Current_scores+scores_;
        StartCoroutine(UpdateScores_Coroutine(scores_));
        Current_scores = scores_;
        UI_Manager.instance.Game_Ui_scores.text = Current_scores.ToString("000000000");
        if (Current_scores > PlayerPrefs.GetInt("highscore"))
        {
            Debug.Log("settinghighScore");
            PlayerPrefs.SetInt("highscore",Current_scores);
            UI_Manager.instance.Game_ui_highScores.text =  Current_scores.ToString("000000000");

        }
    }


    IEnumerator UpdateScores_Coroutine(int targetscores)
    {
        while (Current_scores <= targetscores)
        {
            UI_Manager.instance.Game_Ui_scores.text = Current_scores.ToString("000000000");
            Current_scores++;
            yield return new WaitForSeconds(0.5f);
        }
    }

}
