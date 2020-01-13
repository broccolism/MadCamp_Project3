using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    //버튼 관련 함수들
    public Button startButton;
    public Button settingButton;

    //부드럽게 장면 전환을 위한 함수들
    public Image loadingImage;
    public Text loadingText;
    public GameObject loadingObject;
    AsyncOperation sceneAO;
    float progress;

    private void Awake()
    {
        loadingObject.SetActive(false);
        startButton.onClick.AddListener(OnClickStartButton);
        settingButton.onClick.AddListener(OnClickSettingButton);
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClickStartButton()
    {
        loadingObject.SetActive(true);
        StartCoroutine(IELoadintScene("Map"));
    }

    void OnClickSettingButton()
    {
        Application.Quit();
    }

    void LoadScene()
    {
        
    }

    IEnumerator IELoadintScene(string sceneName)
    {
        sceneAO = SceneManager.LoadSceneAsync(sceneName);

        sceneAO.allowSceneActivation = false;

        while(!sceneAO.isDone)
        {
            loadingImage.fillAmount = sceneAO.progress;
            loadingText.text = (sceneAO.progress * 100).ToString();
            if(sceneAO.progress >= 0.9f)
            {
                loadingText.text = 100.ToString();
                loadingImage.fillAmount = 1;
                yield return new WaitForSeconds(1f);
                sceneAO.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
