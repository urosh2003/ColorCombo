using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {
        GameManager.instance.scoreChanged += ChangeScore;
    }

    private void ChangeScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    private void OnDestroy()
    {
        GameManager.instance.scoreChanged -= ChangeScore;
    }
}