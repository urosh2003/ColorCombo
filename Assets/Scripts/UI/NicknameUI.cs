using UnityEngine;
using TMPro;

public class NicknameUI : MonoBehaviour
{
    public TMP_InputField nicknameField;

    private void Start()
    {
        nicknameField.text = LeaderboardManager.Instance.PlayerNickname;
    }

    public void OnNicknameChanged()
    {
        LeaderboardManager.Instance.SetNickname(nicknameField.text);
    }
}
