using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleClicker
{
    [Serializable]
    public class RuntimeData
    {
        public Camera MainCamera;
        public LevelData CurrentLevelData;
        public int KilledTargets;
        public float PlayerTimer;
        public string SaveDataPath;
        public Dictionary<string, float> Leaderboard;

        [Header("Bonus States")]
        public bool DoubleDamageBonusEnabled;
    }
}
