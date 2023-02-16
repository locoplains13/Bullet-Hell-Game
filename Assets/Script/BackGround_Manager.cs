using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround_Manager : MonoBehaviour
{
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveBg());
    }

    IEnumerator MoveBg()
    {
        while (true)
        {
            transform.position = new Vector3(-2, transform.position.y- Speed, 0);
            if (transform.position.y <= -0.549f)
            {
                transform.position =new Vector3 (-2, 4.61f, 0);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
