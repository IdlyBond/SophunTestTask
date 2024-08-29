using System;
using System.Collections.Generic;
using UnityEngine;

namespace MainUI.Leaderboard.Settings
{
    [CreateAssetMenu(fileName = "RankCardSettings", menuName = "CreateSettings/RankCardSettings")]
    public class RankCardSettings : ScriptableObject
    {
        [SerializeField] 
        private List<RankCardInfo> RanksInfo;

        public RankCardInfo GetInfo(PlayerRank rank)
        {
            RankCardInfo rankCardInfo = RanksInfo.Find(r => r.Rank == rank);
            if (rankCardInfo == default)
            {
                Debug.LogError("Not all ranks settings are set.");
            }
            

            return rankCardInfo;
        }
    }
    
    [Serializable]
    public class RankCardInfo
    {
        [SerializeField] 
        public PlayerRank Rank;
        [SerializeField] 
        public float CardHeight = 300f;
        [SerializeField] 
        public Color Colour = Color.white;

    }
}