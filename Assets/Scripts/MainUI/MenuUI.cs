using System.Threading.Tasks;
using SimplePopupManager;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] 
    private Button leaderboardButton;
    [SerializeField] 
    private Transform canvas;
    
    private void Awake()
    {
        leaderboardButton.onClick.AddListener(OnLeaderboardButton);
    }

    private async void OnLeaderboardButton()
    {
        leaderboardButton.interactable = false; 
        await OpenLeaderboard(); 
        leaderboardButton.interactable = true;
    }

    private async Task OpenLeaderboard()
    {
        await PopupManagerService.Instance.OpenPopup("LeaderboardPopup", 
            new LeaderboardPopupParam(
                canvas, 
                () => PopupManagerService.Instance.ClosePopup("LeaderboardPopup")));
    }
}
