namespace MainUI.Leaderboard
{
    public enum PlayerRank
    {
        Default,
        Bronze,
        Silver,
        Gold,
        Diamond
    }

    public class PlayerRankHelper
    {
        public static PlayerRank DataToRank(string rankData)
        {
            switch (rankData)
            {
                case "Diamond":
                    return PlayerRank.Diamond;
                case "Gold":
                    return PlayerRank.Gold;
                case "Silver":
                    return PlayerRank.Silver;
                case "Bronze":
                    return PlayerRank.Bronze;
                default:
                    return PlayerRank.Default;
            }
            
        }
    }
}