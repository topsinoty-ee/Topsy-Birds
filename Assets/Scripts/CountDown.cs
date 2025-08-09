using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDown : MonoBehaviour
{
    public delegate void CountDownFinished();
    public static event CountDownFinished OnCountDownFinished;

    Text countdown;

    void onEnable()
    {
        countdown = GetComponent<Text>();
        countdown.text = "3";
        StartCoroutine("countDown");
    }

    IEnumerator countDown()
    {
        int count;
        count = 3;
        for (int i = 0; i < count; i++)
        {
            countdown.text = (count - 1).ToString();
            yield return new WaitForSeconds(1);
        }
        OnCountDownFinished();
    }
}
