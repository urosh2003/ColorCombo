using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] public float comboDuration;
    [SerializeField] float timeElapsed;
    [SerializeField] int currentCombo;
    [SerializeField] int comboMultiplier;
    [SerializeField] int basePoints;
    public int score;

    public event Action<int> ScoreChanged;
    public event Action<WizardColor> ColorEnemyFell;
    public event Action<int> RefreshCombo;

    private void Awake()
    {
        instance = this;
        score = 0;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed>=comboDuration && currentCombo!=0)
        {
            currentCombo = 0;
            RefreshCombo.Invoke(currentCombo);
        }
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
        EnemyDied();
        ColorEnemyFell?.Invoke(color);
    }

    public void EnemyDied()
    {
        currentCombo += 1;
        timeElapsed = 0;
        RefreshCombo.Invoke(currentCombo);

        score += CalculatePoints();
        ScoreChanged?.Invoke(score);
    }

    private int CalculatePoints()
    {
        if(currentCombo < 3)
            return basePoints;

        return basePoints * currentCombo;
    }
}