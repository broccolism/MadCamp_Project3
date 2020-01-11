using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public GameObject ingamePanel;
    public GameObject pausePanel;
    public GameObject loading;
    public Text loadingText;
    public GameObject button_1;
    public GameObject button_2;

    private void Start()
    {
        ActivatePause();
        loading.SetActive(true);
        button_1.SetActive(false);
        button_2.SetActive(false);
        StartCoroutine(IELoading());
    }

    IEnumerator IELoading()
    {
        loadingText.text = "냇가와 잔디 심는 중...";
        yield return new WaitForSeconds(1);
        loadingText.text = "나무 심는 중...";
        yield return new WaitForSeconds(2);
        loadingText.text = "토끼 번식 중...";
        yield return new WaitForSeconds(2);
        loadingText.text = "여우 무리 생성중...";
        yield return new WaitForSeconds(2);
        loadingText.text = "맵 생성 완료중...";
        yield return new WaitForSeconds(1);
        loading.SetActive(false);
        button_1.SetActive(true);
        button_2.SetActive(true);
        ActivateInGame();
    }

    void ActivatePause()
    {
        ingamePanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    void ActivateInGame()
    {
        ingamePanel.SetActive(true);
        pausePanel.SetActive(false);
    }
}
