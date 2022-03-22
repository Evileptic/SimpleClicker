using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class SpawnTargetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<SpawnTargetEvent> _spawnTargetFilter;

        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;

        private float xSpawnMin;
        private float xSpawnMax;
        private float ySpawnMin;
        private float ySpawnMax;
        
        public void Init()
        {
            var canvasRect = _sceneData.UI.GetComponent<RectTransform>().rect;
            var targetRect = _staticData.TargetPrefab.GetComponent<RectTransform>().rect;
            xSpawnMin = -canvasRect.width / 2f + targetRect.width / 2f;
            xSpawnMax = -xSpawnMin;
            ySpawnMin = -canvasRect.height / 2f + targetRect.height / 2f;
            ySpawnMax = -ySpawnMin;
        }
        
        public void Run()
        {
            foreach (var index in _spawnTargetFilter)
            {
                var targetActorRef = Object.Instantiate(_staticData.TargetPrefab, _sceneData.PlayGround.transform);
                targetActorRef.Init(_ecsWorld);
                
                var anchoredPos = targetActorRef.RectTransform.anchoredPosition;
                anchoredPos.x = Random.Range(xSpawnMin, xSpawnMax);
                anchoredPos.y = Random.Range(ySpawnMin, ySpawnMax);
                
                targetActorRef.RectTransform.anchoredPosition = anchoredPos;
                targetActorRef.TargetImage.sprite = _runtimeData.CurrentLevelData.TargetImage;

                _spawnTargetFilter.GetEntity(index).Destroy();
            }
        }
    }
}