using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVital : MonoBehaviour
{
    //Control UI element of player
    public Image playerHealthImage;
    public Text playerHeathText;
    public float maxPlayerHealth;
    private float playerHealth;

    public float playerHungerRate;
    public Image playerHungerImage;
    public Text playerHungerText;
    public float maxPlayerHunger;
    private float playerHunger;
    public GameManager gameManager;

    public
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = maxPlayerHealth;
        playerHunger = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHunger < maxPlayerHunger)
        {
            playerHunger = Mathf.Clamp(playerHunger + Time.deltaTime * playerHungerRate, 0, maxPlayerHunger);

        }
        else
        {
            playerHealth = Mathf.Clamp(playerHealth - Time.deltaTime * playerHungerRate, 0, maxPlayerHealth);
        }

        if(playerHealth <= 0)
        {
            Kill();
        }

        SetUI();

    }

    public void Attack(float demage)
    {
        playerHealth = Mathf.Clamp(playerHealth - demage, 0, maxPlayerHealth);
        SetUI();
    }

    public void EatMushroom(float demage)
    {
        playerHunger = Mathf.Clamp(playerHunger - demage, 0, maxPlayerHunger);
        SetUI();
    }

    public void EatCarrot()
    {
        playerHunger = 0;
        playerHealth = maxPlayerHealth;
        SetUI();
    }

    public void Kill()
    {
        playerHealth = 0;
        gameManager.EndGame(false);
        SetUI();
    }

    void SetUI()
    {
        playerHealthImage.fillAmount = playerHealth / maxPlayerHealth;
        playerHeathText.text = Mathf.RoundToInt(playerHealth).ToString();
        playerHungerImage.fillAmount = playerHunger / maxPlayerHunger;
        playerHungerText.text = Mathf.RoundToInt(playerHunger).ToString();
    }
}
