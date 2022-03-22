using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class StartGameSystem : IEcsRunSystem
    {
        private EcsFilter<StartGameEvent> _startGameFilter;
        private EcsFilter<Bonus> _bonusFilter;

        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;
        
        public void Run()
        {
            foreach (var index in _startGameFilter)
            {
                var levelData = _startGameFilter.Get1(index).LevelData;
                var difficultData = levelData.Difficult;
                var playGround = _sceneData.PlayGround;
                var ui = _sceneData.UI;
                
                _sceneData.AudioManager.EffectSource.PlayOneShot(
                    _staticData.StartGameClip, 
                    _staticData.StartGameClipVolume);

                ui.LevelPreview.gameObject.SetActive(false);
                ui.MainMenu.SetActive(false);
                ui.GameMenu.gameObject.SetActive(true);
                ui.GameMenu.ProgressText.text = $"0 / {difficultData.TargetsForWin}";
                ui.GameMenu.TargetsText.text = $"{difficultData.TargetsForWin}";
                ui.GameMenu.TimerText.text = $"{difficultData.SecondsForLevel}";
                ui.GameMenu.ProgressImage.fillAmount = 0f;
                
                playGround.gameObject.SetActive(true);
                playGround.Background.sprite = levelData.BackgroundImage;
                
                _runtimeData.CurrentLevelData = levelData;
                _runtimeData.KilledTargets = 0;

                foreach (var bonusIndex in _bonusFilter)
                {
                    _bonusFilter.GetEntity(bonusIndex).Destroy();
                    Object.Destroy(_bonusFilter.Get1(bonusIndex).ActorRef.gameObject);
                }

                _ecsWorld.NewEntity().Get<StartTimerEvent>().Timer = difficultData.SecondsForLevel;
                _ecsWorld.NewEntity().Get<SpawnTargetEvent>();
                _startGameFilter.GetEntity(index).Destroy();
            }
        }
    }
}