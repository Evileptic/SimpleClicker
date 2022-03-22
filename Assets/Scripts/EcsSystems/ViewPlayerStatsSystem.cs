using Leopotam.Ecs;

namespace SimpleClicker
{
    public class ViewPlayerStatsSystem : IEcsRunSystem
    {
        private EcsFilter<ViewPlayerStatsEvent> _setWinRateFilter;
        
        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;
        
        public void Run()
        {
            foreach (var index in _setWinRateFilter)
            {
                var levelData = _setWinRateFilter.Get1(index).LevelData;
                var playerData = _runtimeData.PlayerData;

                int winRate = 0;
                var playerLevelData = playerData.PlayerLevelsData[levelData.Id];
                if (playerLevelData.Loses > 0 || playerLevelData.Wins > 0)
                    winRate = 100 / (playerLevelData.Loses + playerLevelData.Wins) * playerLevelData.Wins;

                _sceneData.UI.LevelPreview.WinrateText.text = $"Winrate: {winRate} %";
                _sceneData.UI.LevelPreview.UsedBonuses.text = $"Used bonuses: {playerLevelData.UsedBonuses}";
            }
        }
    }
}