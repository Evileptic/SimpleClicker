using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class InitializeSystem : IEcsInitSystem
    {
        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private EcsWorld _ecsWorld;
        
        public void Init()
        {
            _runtimeData.MainCamera = Camera.main;
            _runtimeData.SaveDataPath = $"{Application.dataPath}/{_staticData.SaveDataFolder}";
            _runtimeData.Leaderboard = new Dictionary<string, float>();
            _runtimeData.TargetBonusRemains = _staticData.TargetsForBonus;

            var actors = Object.FindObjectsOfType<Actor>(true);
            foreach (var actor in actors)
                actor.Init(_ecsWorld);

            _ecsWorld.NewEntity().Get<GenerateMenuEvent>();
            _ecsWorld.NewEntity().Get<LoadAudioSettingsEvent>();
        }
    }
}