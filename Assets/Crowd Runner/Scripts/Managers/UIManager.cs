using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] ShopManager shopManager;

    [Header(" Elements ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;

    [SerializeField] private Slider progressBar;
    [SerializeField] private Text levelText;

    void Start()
    {
        gamePanel.SetActive(false);
        gameoverPanel.SetActive(false);
        settingsPanel.SetActive(false);
        HideShop();

        progressBar.value = 0;

        levelText.text = "Level " + (ChunkManager.instance.GetLevel() + 1);

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    void Update()
    {
        UpdateProgressBar();
    }

    private void GameStateChangedCallback(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.Gameover) {
            ShowGameoverPanel();
        } else if(gameState == GameManager.GameState.LevelComplete) {
            ShowLevelCompletePanel();
        }
    }

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void RetryButtonPressed() 
    {
        // TO DO: Bulunulan level tekrar oynatılmalı.
        SceneManager.LoadScene(0);
    }

    public void ShowGameoverPanel() 
    {
        gamePanel.SetActive(false);
        gameoverPanel.SetActive(true);
    }

    private void ShowLevelCompletePanel()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if(!GameManager.instance.IsGameState()) {
            return;
        }

        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishZ();
        progressBar.value = progress;
    }

    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void HideSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    public void ShowShop()
    {
        shopPanel.SetActive(true);
        shopManager.UpdatePurchaseButton();
    }

    public void HideShop()
    {
        shopPanel.SetActive(false);
    }
    
}
