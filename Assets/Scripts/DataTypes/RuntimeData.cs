using System;
using UnityEngine;

namespace SimpleClicker
{
    [Serializable]
    public class RuntimeData
    {
        public Camera MainCamera;
        public LevelData CurrentLevelData;
        public int KilledTargets;
        public float LevelTimer;
        
        [Header("Bonus States")]
        public bool DoubleDamageBonusEnabled;
    }
}
