using Leopotam.Ecs;

namespace SimpleClicker
{
    public class OpenLevelPreviewSystem : IEcsRunSystem
    {
        private EcsFilter<OpenLevelPreviewEvent> _levelPreviewFilter;

        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;

        public void Run()
        {
            foreach (var index in _levelPreviewFilter)
            {
                _sceneData.AudioManager.EffectSource.PlayOneShot(
                    _staticData.LevelClickClip,
                    _staticData.LevelClickClipVolume);

                var levelPreviewRef = _sceneData.UI.LevelPreview;
                levelPreviewRef.gameObject.SetActive(true);

                var levelData = _levelPreviewFilter.Get1(index).LevelData;
                levelPreviewRef.LevelData = levelData;
                levelPreviewRef.LevelNameText.text = levelData.LevelName;
                levelPreviewRef.BackgroundImage.sprite = levelData.BackgroundImage;

                for (int i = 0; i <= levelData.Difficult.DifficultLevel; i++)
                    levelPreviewRef.DifficultSculls[i].SetActive(true);

                _ecsWorld.NewEntity().Get<ViewPlayerStatsEvent>().LevelData = levelData;
                _ecsWorld.NewEntity().Get<LoadLeaderBoardEvent>().LevelName = levelData.LevelName;
            }
        }
    }
}