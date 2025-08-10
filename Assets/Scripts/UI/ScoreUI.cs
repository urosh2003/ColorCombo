using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboText;

    [SerializeField] GameObject progressBarImage;

    [SerializeField] GameObject comboPanel;

    private float timeElapsed;
    private float comboDuration;

    private void Awake()
    {
        GameManager.instance.ScoreChanged += ChangeScore;
        GameManager.instance.RefreshCombo += ChangeCombo;
        comboDuration = GameManager.instance.comboDuration;
    }

    private void ChangeScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    private void ChangeCombo(int combo)
    {
        if (combo < 3)
            comboPanel.SetActive(false);
        else 
            comboPanel.SetActive(true);

        comboText.text = "COMBO: " + combo + "X";

        timeElapsed = 0;
    }

    public void SetProgress(float progress)
    {
        progressBarImage.transform.localScale = new Vector3(progress,1,1);
    }


    private void OnDestroy()
    {
        GameManager.instance.ScoreChanged -= ChangeScore;
        GameManager.instance.RefreshCombo -= ChangeCombo;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        SetProgress(1 - timeElapsed/comboDuration);
    }
}