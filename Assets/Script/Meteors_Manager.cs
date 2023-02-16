using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteors_Manager : MonoBehaviour
{
    public GameObject[] prefabs_;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spawn_Meteors");
    }

    IEnumerator Spawn_Meteors()
    {
        while (!UI_Manager.instance.IsGameOver &&  !UI_Manager.instance.IsShopOpen)
        {
            GameObject obj = Instantiate(prefabs_[Random.Range(0, prefabs_.Length)], transform);
            obj.transform.localPosition=new Vector3(Random.Range(-5,0), 7.8f,0);
            yield return new WaitForSeconds(Random.Range(3, 10));
        }
    }
}
