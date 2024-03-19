using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource doorHitSound;
    [SerializeField] private AudioSource runnerDieSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameoverSound;

    void Start()
    {
        PlayerDetection.onDoorsHit += PlayDoorHitSound;
        GameManager.onGameStateChanged += GameStateChangedCallBack;
        Enemy.onRunnerDied += PlayRunnerDieDound;
    }

    private void OnDestroy()
    {
        PlayerDetection.onDoorsHit -= PlayDoorHitSound;
        GameManager.onGameStateChanged -= GameStateChangedCallBack;
        Enemy.onRunnerDied -= PlayRunnerDieDound;
    }

    void Update()
    {
        
    }

    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.LevelComplete)
        {
            levelCompleteSound.Play();
        }
        else if(gameState == GameManager.GameState.Gameover)
        {
            gameoverSound.Play();
        }
    }

    private void PlayDoorHitSound()
    {
        doorHitSound.Play();
    }

    private void PlayRunnerDieDound()
    {
        runnerDieSound.Play();
    }

    public void DisableSounds()
    {
        buttonSound.volume = 0;
        doorHitSound.volume = 0;
        runnerDieSound.volume = 0;
        levelCompleteSound.volume = 0;
        gameoverSound.volume = 0;

    }

    public void EnableSounds()
    {
        buttonSound.volume = 1;
        doorHitSound.volume = 1;
        runnerDieSound.volume = 1;
        levelCompleteSound.volume = 1;
        gameoverSound.volume = 1;
    }

}
