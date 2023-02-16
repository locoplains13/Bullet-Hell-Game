using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public static BackgroundMusicController instance;

    private void Awake(){

        DontDestroyOnLoad(this.gameObject);

        if (instance == null){
            instance = this;
        }

        else{
            Destroy(gameObject);
        }
    }
}