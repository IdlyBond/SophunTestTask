using System.Threading.Tasks;
using Helpers;
using MainUI.Leaderboard.Graphics;
using MainUI.Leaderboard.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainUI.Leaderboard
{
    public class LeaderboardPlayerInfoUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI nameLabel;
        [SerializeField]
        private TextMeshProUGUI scoreLabel;
        [SerializeField]
        private RawImage avatarImage;
        [SerializeField] 
        private AvatarSkeletonAnimationUI avatarSkeleton;
        [SerializeField] 
        private RankCardSettings rankCardsSettings;
        [SerializeField] 
        private Image backgroundImage;
        [SerializeField] 
        private RectTransform rectTransform;

        public void Initialize(LeaderboardPlayerInfo info)
        {
            nameLabel.text = info.name;
            scoreLabel.text = $"Score: {info.score}";
            SetRank(info.type);

            SetAvatar(info.avatar);
        }

        private async Task SetAvatar(string url)
        {
            avatarImage.texture =  await ImageRequestHelper.RequestTexture(url);
            avatarSkeleton.StopAnimation();
        }

        private void SetRank(string type)
        {
            RankCardInfo info = rankCardsSettings.GetInfo(PlayerRankHelper.DataToRank(type));
            backgroundImage.color = info.Colour;

            Vector2 sizeDelta = rectTransform.sizeDelta;
            sizeDelta.y = info.CardHeight;
            rectTransform.sizeDelta = sizeDelta;
        }
    }


    
    
}