using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;

    private const string LeaderboardId = "ColorComboLeaderboard";
    private const string NicknameKey = "PlayerNickname";

    public string PlayerNickname { get; private set; }

    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            await InitServices();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private async Task InitServices()
    {
        try
        {
            await UnityServices.InitializeAsync();

            if (AuthenticationService.Instance.IsSignedIn)
                AuthenticationService.Instance.SignOut(true);

            PlayerNickname = PlayerPrefs.GetString(NicknameKey, "");
            if (string.IsNullOrEmpty(PlayerNickname))
            {
                PlayerNickname = "Player" + UnityEngine.Random.Range(1000, 9999);
                PlayerPrefs.SetString(NicknameKey, PlayerNickname);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"UGS Init Error: {e}");
        }
    }

    public void SetNickname(string newName)
    {
        PlayerNickname = newName;
        PlayerPrefs.SetString(NicknameKey, newName);
        Debug.Log($"Nickname changed to: {newName}");
    }

    public async void SubmitScore(int score)
    {
        try
        {
            if(AuthenticationService.Instance.IsSignedIn)
                AuthenticationService.Instance.SignOut(true);
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            var metadata = new Dictionary<string, string> { { "nickname", PlayerNickname } };
            AddPlayerScoreOptions addPlayerScoreOptions = new()
            {
                Metadata = metadata
            };
            var response = await LeaderboardsService.Instance.AddPlayerScoreAsync(
                LeaderboardId, score, addPlayerScoreOptions
            );
            Debug.Log($"Score submitted: {response.Score} by {PlayerNickname}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error submitting score: {e}");
        }
    }

    public async Task<List<LeaderboardEntry>> GetTopScores(int limit = 10)
    {
        if (!AuthenticationService.Instance.IsSignedIn)
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        var results = new List<LeaderboardEntry>();
        try
        {
            var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(
                LeaderboardId, new GetScoresOptions { Limit = limit, IncludeMetadata = true }
            );
            results.AddRange(scoresResponse.Results);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error retrieving scores: {e}");
        }
        AuthenticationService.Instance.SignOut(true);
        return results;
    }
}
