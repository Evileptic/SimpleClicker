using System;

namespace SimpleClicker
{
    [Serializable]
    public class PlayerData
    {
        public int CurrentLevel;
        public PlayerLevelData[] PlayerLevelsData;
    }
}