using Leopotam.Ecs;

namespace SimpleClicker
{
    public class EndLevelSystem : IEcsRunSystem
    {
        private EcsFilter<EndLevelEvent> _endLevelFilter;

        private RuntimeData _runtimeData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;
        
        public void Run()
        {
            foreach (var index in _endLevelFilter)
            {
                _ecsWorld.NewEntity().Get<StopTimerEvent>();
                
                _sceneData.PlayGround.gameObject.SetActive(false);
                _sceneData.UI.GameMenu.gameObject.SetActive(false);
                _sceneData.UI.MainMenu.SetActive(true);
                _sceneData.UI.LevelPreview.gameObject.SetActive(true);

                var playerData = _runtimeData.PlayerData;
                if (_endLevelFilter.Get1(index).IsWin)
                {
                    if (_runtimeData.CurrentLevelData.Id == playerData.CurrentLevel)
                    {
                        playerData.CurrentLevel++;
                        _ecsWorld.NewEntity().Get<SetLevelProgressEvent>();
                    }

                    playerData.PlayerLevelsData[_runtimeData.CurrentLevelData.Id].Wins++;
                    _ecsWorld.NewEntity().Get<SaveLeaderBoardEvent>();
                }
                else
                {
                    playerData.PlayerLevelsData[_runtimeData.CurrentLevelData.Id].Loses++;
                }

                _ecsWorld.NewEntity().Get<SetWinRateEvent>().LevelId = _runtimeData.CurrentLevelData.Id;
                _ecsWorld.NewEntity().Get<SavePlayerDataEvent>();
            }
        }
    }
}