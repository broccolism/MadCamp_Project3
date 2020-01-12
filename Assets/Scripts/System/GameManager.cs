using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameStatus
    {
        MainMenu,
        StartInGame,
        UpdateInGame,
        StartResult,
        UpdateResult,
    }
    public float playTimeInSecond;
    public GameStatus gameStatus;

    //시스템 UI정보
    public Text playTimeText;
    public InGameUI inGameUI;

    //점수
    public float finalScore;

    // Start is called before the first frame update
    void Start()
    {
        //this is debug perpose only
        gameStatus = GameStatus.StartInGame;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameStatus)
        {
            case GameStatus.MainMenu:
                UpdateMainMenu();
                break;
            case GameStatus.StartInGame:
                StartInGame();
                break;
            case GameStatus.UpdateInGame:
                UpdateInGame();
                break;
            case GameStatus.StartResult:
                StartResult();
                break;
            case GameStatus.UpdateResult:
                UpdateResult();
                break;
        }
    }

    private void UpdateMainMenu()
    {

    }

    private void StartInGame()
    {
        Cursor.visible = false;
        playTimeInSecond = Time.time;
        gameStatus = GameStatus.UpdateInGame;
    }

    private void UpdateInGame()
    {
        playTimeText.text = SecondToString(Time.time - playTimeInSecond);
    }

    private void StartResult()
    {
        Cursor.visible = true;
        finalScore = Time.time - playTimeInSecond;
        gameStatus = GameStatus.UpdateResult; 
    }

    private void UpdateResult()
    {
        inGameUI.ActivateEndGame();
        inGameUI.endsScoreText.text = SecondToString(finalScore);
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        if(gameStatus == GameStatus.UpdateInGame)
            gameStatus = GameStatus.StartResult;
    }

    public string SecondToString(float _second)
    {
        int secondInInt = (int)_second;
        int second = secondInInt % 60;
        int min = ((secondInInt - second) / 60) % 60;
        int hour = (((secondInInt - second - min * 60)) / 3600);

        string secondString = (second == 0) ? "00" : second.ToString("00");
        string minString = (min == 0) ? "00" : min.ToString("00");
        string hourString = (hour == 0) ? "00" : hour.ToString("00");

        string timeString = hourString + ":" + minString + " " + secondString;

        return timeString;
    }
}
