using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class SpawnBonusSystem : IEcsRunSystem
    {
        private EcsFilter<SpawnBonusEvent> _spawnBonusFilter;
        private EcsFilter<Bonus> _bonusFilter;

        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;
        
        private float timer;

        public void Run()
        {
            foreach (var index in _spawnBonusFilter)
            {
                if (_runtimeData.BonusMode || _bonusFilter.GetEntitiesCount() > 0) return;
                
                var bonusForSpawn = _staticData.BonusActors[Random.Range(0, _staticData.BonusActors.Length)];
                
                var targetActorRef = Object.Instantiate(bonusForSpawn, _sceneData.PlayGround.transform);
                targetActorRef.Init(_ecsWorld);
                
                var anchoredPos = targetActorRef.RectTransform.anchoredPosition;
                anchoredPos.x = Random.Range(_runtimeData.XSpawnMin, _runtimeData.XSpawnMax);
                anchoredPos.y = Random.Range(_runtimeData.YSpawnMin, _runtimeData.YSpawnMax);
                
                targetActorRef.RectTransform.anchoredPosition = anchoredPos;
            }
        }
    }
}