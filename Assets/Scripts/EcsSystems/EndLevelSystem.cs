using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class EndLevelSystem : IEcsRunSystem
    {
        private EcsFilter<EndLevelEvent> _endLevelFilter;
        private EcsFilter<Target> _targetFilter;
        private EcsFilter<Bonus> _bonusFilter;

        private RuntimeData _runtimeData;
        private StaticData _staticData;
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

                _runtimeData.TargetBonusRemains = _staticData.TargetsForBonus;
                
                foreach (var targetIndex in _targetFilter)
                    if (_targetFilter.Get1(targetIndex).ActorRef)
                        Object.Destroy(_targetFilter.Get1(targetIndex).ActorRef.gameObject);
                foreach (var bonusIndex in _bonusFilter)
                    if(_bonusFilter.Get1(bonusIndex).ActorRef)
                        Object.Destroy(_bonusFilter.Get1(bonusIndex).ActorRef.gameObject);
                
                _ecsWorld.NewEntity().Get<ViewPlayerStatsEvent>().LevelData = _runtimeData.CurrentLevelData;
                _ecsWorld.NewEntity().Get<SavePlayerDataEvent>();
            }
        }
    }
}