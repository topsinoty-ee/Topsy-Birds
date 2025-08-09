using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    [SerializeField]
    string strTag;

    [SerializeField]
    string Scenename;

    public void OnCollisionEnter(Collision col)
    {
        //Load Gameover
        if (col.collider.tag == strTag)
            SceneManager.LoadScene(Scenename);
    }
                                  
    public void OnTrigger(Collision cole)
    {
        if (cole.collider.tag == strTag) ;
            //Add Score
    }
}
