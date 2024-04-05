using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header(" Coin Texts ")]
    [SerializeField] private Text[] coinsTexts;
    private int coins;

    void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else
            instance = this;
            
        coins = PlayerPrefs.GetInt("coins", 0);
    }

    void Start()
    {
        UpdateCoinsTexs();
    }

    void Update()
    {
        
    }

    private void UpdateCoinsTexs()
    {
        foreach (Text coinText in coinsTexts)
        {
            coinText.text = coins.ToString();
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;

        UpdateCoinsTexs();

        PlayerPrefs.SetInt("coins", coins);
    }

    public void UseCoins(int amount)
    {
        coins -= amount;

        UpdateCoinsTexs();

        PlayerPrefs.SetInt("coins", coins);
    }

    public int GetCoins()
    {
        return coins;
    }

}
