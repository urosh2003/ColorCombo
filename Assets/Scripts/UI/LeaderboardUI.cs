using UnityEngine;
using TMPro;
using Unity.VisualScripting;
[System.Serializable]
public class PlayerMetadata  // Better to use a more specific name
{
    public string nickname;
}

public class LeaderboardUI : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText;

    private async void OnEnable()
    {
        leaderboardText.text = "Loading...";
        var scores = await LeaderboardManager.Instance.GetTopScores(10);

        leaderboardText.text = "";
        foreach (var s in scores)
        {
            PlayerMetadata metadata = JsonUtility.FromJson<PlayerMetadata>(s.Metadata);
            leaderboardText.text += $"{s.Rank + 1}. {metadata.nickname} — {s.Score}\n";
        }
    }
}
