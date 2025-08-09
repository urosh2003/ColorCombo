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
    [SerializeField] int chestPoints;
    public int score;

    public event Action<int> ScoreChanged;
    public event Action<WizardColor> ColorEnemyFell;
    public event Action<int> RefreshCombo;
    public event Action FailHit;
    public event Action<int> ChestCollected;

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

    public void HitFailed()
    {
        FailHit.Invoke();
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

    public void EnemyDied(WizardColor color, int points)
    {
        EnemyDied(points);
        ColorEnemyFell?.Invoke(color);
    }

    public void EnemyDied(int points)
    {
        currentCombo += 1;
        timeElapsed = 0;
        RefreshCombo.Invoke(currentCombo);

        score += CalculatePoints(points);
        ScoreChanged?.Invoke(score);
    }

    private int CalculatePoints(int points)
    {
        if(currentCombo < 3)
            return points;

        return points * currentCombo;
    }

    public void ChestPickedUp(int chestNumber)
    {
        ChestCollected?.Invoke(chestNumber);
        score += CalculatePoints(chestPoints);
        ScoreChanged?.Invoke(score);
    }
}