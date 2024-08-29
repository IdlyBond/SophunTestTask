using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helpers;
using MainUI.Leaderboard;
using MainUI.Leaderboard.Graphics;
using SimplePopupManager;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPopup : MonoBehaviour, IPopupInitialization
{
    [SerializeField] 
    private Button closeButton;
    [SerializeField] 
    private RectTransform contentTransform;
    [SerializeField] 
    private LeaderboardPlayerInfoUI playerInfoPrefab;
    [SerializeField] 
    private Button loadMoreButton;
    [SerializeField] 
    private int previewPoolSize = 10;
    [SerializeField] 
    private int increasePoolAmount = 5;
    [SerializeField] 
    private PopupRolloutAnimation rolloutAnimation;

    private LeaderboardInfo leaderboardInfo;
    private List<LeaderboardPlayerInfoUI> contentPool = new();
    private LeaderboardPopupParam settings;
    
    private const string LeaderboardInfoPath = "Leaderboard";
    

    private void Awake()
    {
        loadMoreButton.gameObject.SetActive(false);
        loadMoreButton.SetOnClick(OnLoadMoreButton);
    }

    public async Task Init(object param)
    {
        if (param is LeaderboardPopupParam currentSettings)
        {
            gameObject.transform.SetParent(currentSettings.Parent, false);
            closeButton.SetOnClick(OnClose);
            settings = currentSettings;
        }

        LoadLeaderboard();
        DisplayLeaderboard();
        await Task.CompletedTask;
    }

    private void LoadLeaderboard()
    {
        TextAsset text = Resources.Load<TextAsset>(LeaderboardInfoPath);
        leaderboardInfo = JsonUtility.FromJson<LeaderboardInfo>(text.text);
    }

    private void DisplayLeaderboard()
    {
        for (int i = 0; i < Mathf.Min(previewPoolSize, leaderboardInfo.leaderboard.Count); i++)
        {
            LeaderboardPlayerInfo playerInfo = leaderboardInfo.leaderboard[i];
            LeaderboardPlayerInfoUI playerInfoUI = Instantiate(playerInfoPrefab, contentTransform);
            playerInfoUI.Initialize(playerInfo);
            contentPool.Add(playerInfoUI);
        }
        
        loadMoreButton.gameObject.SetActive(contentPool.Count < leaderboardInfo.leaderboard.Count);
        loadMoreButton.transform.SetAsLastSibling();

    }

    private void OnLoadMoreButton()
    {
        loadMoreButton.gameObject.SetActive(false);
        int contentPoolCount = contentPool.Count;
        for (int i = contentPoolCount; i < Mathf.Min(contentPoolCount + increasePoolAmount, leaderboardInfo.leaderboard.Count); i++)
        {
            LeaderboardPlayerInfo playerInfo = leaderboardInfo.leaderboard[i];
            LeaderboardPlayerInfoUI playerInfoUI = Instantiate(playerInfoPrefab, contentTransform);
            playerInfoUI.Initialize(playerInfo);
            contentPool.Add(playerInfoUI);
        }
        loadMoreButton.gameObject.SetActive(contentPool.Count < leaderboardInfo.leaderboard.Count);
        loadMoreButton.transform.SetAsLastSibling();
    }

    private void OnClose()
    {
        rolloutAnimation.AnimateOut(settings.OnClose);
    }
}

public class LeaderboardPopupParam
{
    public readonly Transform Parent;
    public readonly Action OnClose;

    public LeaderboardPopupParam(Transform parent, Action onClose)
    {
        Parent = parent;
        OnClose = onClose;
    }
}