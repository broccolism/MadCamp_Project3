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
    
    public 
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = maxPlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack(float demage)
    {
        playerHealth = Mathf.Clamp(playerHealth - demage, 0, maxPlayerHealth);
        SetUI();
    }

    public void Heal(float demage)
    {
        playerHealth = Mathf.Clamp(playerHealth + demage, 0 ,maxPlayerHealth);
        SetUI();
    }

    void SetUI()
    {
        playerHealthImage.fillAmount = playerHealth / maxPlayerHealth;
        playerHeathText.text = Mathf.RoundToInt(playerHealth).ToString();
    }
}
