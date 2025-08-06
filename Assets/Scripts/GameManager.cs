using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject gameOverScreen;
    public int score;

    public event Action<int> ScoreChanged;
    public event Action<WizardColor> ColorEnemyFell;

    private void Awake()
    {
        instance = this;
        score = 0;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void EnemyDied(WizardColor color)
    {
        score += 1;

        ScoreChanged?.Invoke(score);
        ColorEnemyFell?.Invoke(color);
    }

    public void EnemyDied()
    {
        score += 1;
        ScoreChanged?.Invoke(score);
    }
}