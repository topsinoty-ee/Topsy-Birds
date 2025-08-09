using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public delegate void GameDeli();
    public static event GameDeli OnGameStarted;   
    public static event GameDeli OnGameOverConfirmed;
    
    public static GameManagerScript Instance;

    public GameObject StartPage;
    public GameObject CountDown;
    public Text Score;
    public GameObject GameOverPage; 


    enum PageState
    {
        None,
        Start,
        GameOver,
        CountDown
    }
    int score = 0;
    bool gameOver = true;

    public bool GameOver { get { return gameOver; } }

    void awake()
    {
        if(Instance != null){
            Destroy(gameObject);
        }
        else
        {
            Instance=this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void OnEnable()
    {
        TapForce.OnPlayerDied += OnPlayerDied;
        TapForce.OnPlayerScored += OnPlayerScored;
        CountdownText.OnCountdownFinished+= OnCountdownFinished;
    }

    void OnDisable()
    {
        TapForce.OnPlayerDied -= OnPlayerDied;
        TapForce.OnPlayerScored -= OnPlayerScored; 
        CountDown.OnCountdownFinished -= OnCountdownFinished;
    }

    void OnCountdownFinished()
    {
        SetPageState(PageState.None);
        OnGameStarted();
        score = 0;
        gameOver = false;
    }
    
    void OnPlayerScored(){
        score++;
        Score.text=score.ToString();
    }
    void OnPlayerDied(){
        gameOver = true;
        int savedScore=PlayerPrefs.GetInt("Highscore");
        if(score>savedScore){
            PlayerPrefs.SetInt("Highscore", score);
        }
        SetPageState(PageState.GameOver);
    }

    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                StartPage.SetActive(false);
                CountDown.SetActive(false);
                GameOverPage.SetActive(false);
                break;
            //For the start page
            case PageState.Start:
                StartPage.SetActive(true);
                CountDown.SetActive(false);
                GameOverPage.SetActive(false);
                break;
            //For the GameOver Page
            case PageState.GameOver:
                StartPage.SetActive(false);
                CountDown.SetActive(false);
                GameOverPage.SetActive(true);
                break;
            //For the CountDown Page
            case PageState.CountDown:
                StartPage.SetActive(false);
                CountDown.SetActive(true);
                GameOverPage.SetActive(false);
                break;
        }
    }
    public void OnConfirmRestart()
    {
        OnGameOverConfirmed();
        Score.text = "0";
        SetPageState(PageState.CountDown);
    }
    public void StartGame()
    {
        SetPageState(PageState.CountDown);
    }
    public void OnHome(){
        OnGameOverConfirmed();
        Score.text="0";
        SetPageState(PageState.Start);
    }
}
