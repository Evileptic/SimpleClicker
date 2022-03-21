using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class GenerateMenuSystem : IEcsRunSystem
    {
        private EcsFilter<GenerateMenuEvent> _generateMenuFilter;

        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;

        public void Run()
        {
            foreach (var index in _generateMenuFilter)
            {
                foreach (var levelData in _staticData.Levels)
                {
                    var levelEntryRef = Object.Instantiate(
                        _staticData.LevelEntryPrefab, 
                        _sceneData.UI.LevelGridContainer);
                    levelEntryRef.Init(_ecsWorld);

                    levelEntryRef.LevelData = levelData;
                    levelEntryRef.LevelNameText.text = levelData.LevelName;
                    levelEntryRef.BackgroundImage.sprite = levelData.BackgroundImage;

                    for (int i = 0; i <= levelData.Difficult.DifficultLevel; i++)
                        levelEntryRef.DifficultSculls[i].SetActive(true);
                }

                // HACK: FAKE LEVELS FILL FOR SCROLL VIEW PRESENT
                for (int i = 0; i < 19; i++)
                    Object.Instantiate(_staticData.BlockedEntryPrefab, _sceneData.UI.LevelGridContainer);
                
                _generateMenuFilter.GetEntity(index).Destroy();
            }
        }
    }
}