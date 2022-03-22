using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class InitializeSystem : IEcsInitSystem
    {
        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;
        
        public void Init()
        {
            _runtimeData.SaveDataPath = $"{Application.dataPath}/{_staticData.SaveDataFolder}";
            _runtimeData.Leaderboard = new Dictionary<string, float>();
            _runtimeData.TargetBonusRemains = _staticData.TargetsForBonus;

            var actors = Object.FindObjectsOfType<Actor>(true);
            foreach (var actor in actors)
                actor.Init(_ecsWorld);
            
            var canvasRect = _sceneData.UI.GetComponent<RectTransform>().rect;
            var targetRect = _staticData.TargetPrefab.GetComponent<RectTransform>().rect;
            _runtimeData.XSpawnMin = -canvasRect.width / 2f + targetRect.width / 2f + _staticData.SpawnLimitShift;
            _runtimeData.XSpawnMax = -_runtimeData.XSpawnMin;
            _runtimeData.YSpawnMin = -canvasRect.height / 2f + targetRect.height / 2f + _staticData.SpawnLimitShift;;
            _runtimeData.YSpawnMax = -_runtimeData.YSpawnMin;

            _ecsWorld.NewEntity().Get<LoadPlayerDataEvent>();
            _ecsWorld.NewEntity().Get<GenerateMenuEvent>();
            _ecsWorld.NewEntity().Get<LoadAudioSettingsEvent>();
        }
    }
}