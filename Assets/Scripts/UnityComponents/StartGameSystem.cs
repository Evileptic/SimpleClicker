using Leopotam.Ecs;

namespace SimpleClicker
{
    public class StartGameSystem : IEcsRunSystem
    {
        private EcsFilter<StartGameEvent> _startGameFilter;

        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;
        
        public void Run()
        {
            foreach (var index in _startGameFilter)
            {
                _sceneData.AudioManager.EffectSource.PlayOneShot(
                    _staticData.StartGameClip, 
                    _staticData.StartGameClipVolume);
                
                _sceneData.UI.LevelPreview.gameObject.SetActive(false);
                _sceneData.UI.MainMenu.SetActive(false);
                _sceneData.UI.GameMenu.gameObject.SetActive(true);
                _sceneData.PlayGround.gameObject.SetActive(true);

                var levelData = _startGameFilter.Get1(index).LevelData;
                _sceneData.PlayGround.Background.sprite = levelData.BackgroundImage;
                _sceneData.UI.GameMenu.ProgressText.text = $"0 / {levelData.Difficult.TargetsForWin}";
                _sceneData.UI.GameMenu.TargetsText.text = $"{levelData.Difficult.TargetsForWin}";
                _sceneData.UI.GameMenu.TimerText.text = $"{levelData.Difficult.SecondsForLevel}";
                _runtimeData.CurrentLevelData = levelData;

                _ecsWorld.NewEntity().Get<StartTimerEvent>().Timer = levelData.Difficult.SecondsForLevel;
                _ecsWorld.NewEntity().Get<SpawnTargetEvent>();
                _startGameFilter.GetEntity(index).Destroy();
            }
        }
    }
}