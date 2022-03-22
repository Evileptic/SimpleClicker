using System;

namespace SimpleClicker
{
    [Serializable]
    public class PlayerLevelData
    {
        public int Id;
        public string LevelName;
        public int Wins;
        public int Loses;
        public int UsedBonuses;
    }
}