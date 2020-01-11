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
        Result,
    }
    public float playTimeInSecond;
    public GameStatus gameStatus;

    //시스템 UI정보
    public Text playTimeText;

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
            case GameStatus.Result:
                UpdateResult();
                break;
        }
    }

    public void UpdateMainMenu()
    {

    }

    public void StartInGame()
    {
        playTimeInSecond = Time.time;
        gameStatus = GameStatus.UpdateInGame;
    }

    public void UpdateInGame()
    {
        playTimeText.text = SecondToString(Time.time - playTimeInSecond);
    }

    public void UpdateResult()
    {

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
