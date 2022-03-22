using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleClicker
{
    [Serializable]
    public class RuntimeData
    {
        public LevelData CurrentLevelData;
        public int KilledTargets;
        public float PlayerTimer;
        public string SaveDataPath;
        public Dictionary<string, float> Leaderboard;
        public int TargetBonusRemains;
        public PlayerData PlayerData;

        [Header("Bonus States")]
        public bool BonusMode;
        public bool DoubleBonusEnabled;
        public bool SizeBonusEnabled;
        public bool FreezeBonusEnabled;
        
        [Header("Spawn Limits")]
        public float XSpawnMin;
        public float XSpawnMax;
        public float YSpawnMin;
        public float YSpawnMax;
    }
}
