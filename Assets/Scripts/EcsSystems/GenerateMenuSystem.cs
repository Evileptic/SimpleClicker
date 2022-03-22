using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class GenerateMenuSystem : IEcsRunSystem
    {
        private EcsFilter<GenerateMenuEvent> _generateMenuFilter;

        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;

        public void Run()
        {
            foreach (var index in _generateMenuFilter)
            {
                _runtimeData.LevelEntries = new LevelEntryActor[_staticData.Levels.Length];
                
                for (int i = 0; i < _staticData.Levels.Length; i++)
                {
                    var levelData = _staticData.Levels[i];
                    var levelEntryRef = Object.Instantiate(
                        _staticData.LevelEntryPrefab, 
                        _sceneData.UI.LevelGridContainer);
                    levelEntryRef.Init(_ecsWorld);

                    levelEntryRef.LevelData = levelData;
                    levelEntryRef.LevelNameText.text = levelData.LevelName;
                    levelEntryRef.BackgroundImage.sprite = levelData.BackgroundImage;

                    for (int j = 0; j <= levelData.Difficult.DifficultLevel; j++)
                        levelEntryRef.DifficultSculls[j].SetActive(true);

                    _runtimeData.LevelEntries[i] = levelEntryRef;
                }
                
                for (int i = 0; i < 19; i++)
                    Object.Instantiate(_staticData.BlockedEntryPrefab, _sceneData.UI.LevelGridContainer);

                _ecsWorld.NewEntity().Get<SetLevelProgressEvent>();
            }
        }
    }
}