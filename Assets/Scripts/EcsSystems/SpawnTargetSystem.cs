using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class SpawnTargetSystem : IEcsRunSystem
    {
        private EcsFilter<SpawnTargetEvent> _spawnTargetFilter;

        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;
        
        private static readonly int TargetAppearSpeed = Animator.StringToHash("Speed");
        
        float randomPosX;
        float randomPosY;

        public void Run()
        {
            foreach (var index in _spawnTargetFilter)
            {
                var targetActorRef = Object.Instantiate(_staticData.TargetPrefab, _sceneData.PlayGround.transform);
                targetActorRef.Init(_ecsWorld);

                if (!_runtimeData.FreezeBonusEnabled)   
                {
                    randomPosX = Random.Range(_runtimeData.XSpawnMin, _runtimeData.XSpawnMax);
                    randomPosY = Random.Range(_runtimeData.YSpawnMin, _runtimeData.YSpawnMax);
                }

                var anchoredPos = targetActorRef.RectTransform.anchoredPosition;
                anchoredPos.x = randomPosX;
                anchoredPos.y = randomPosY;
                
                targetActorRef.RectTransform.anchoredPosition = anchoredPos;
                targetActorRef.TargetImage.sprite = _runtimeData.CurrentLevelData.TargetImage;

                if (_runtimeData.SizeBonusEnabled)
                {
                    targetActorRef.RectTransform.localScale *= _staticData.SizeBonusMultiplier;
                    targetActorRef.Animator.SetFloat(TargetAppearSpeed, 2f);
                }

                _spawnTargetFilter.GetEntity(index).Destroy();
            }
        }
    }
}