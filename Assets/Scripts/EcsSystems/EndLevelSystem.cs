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
                
                if (_endLevelFilter.Get1(index).IsWin)
                    _ecsWorld.NewEntity().Get<SaveLeaderBoardEvent>();
            }
        }
    }
}