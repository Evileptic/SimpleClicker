using Leopotam.Ecs;

namespace SimpleClicker
{
    public class LevelPreviewSystem : IEcsRunSystem
    {
        private EcsFilter<LevelPreviewEvent> _levelPreviewFilter;
        private EcsFilter<LevelPreviewCloseEvent> _levelPreviewCloseFilter;

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

                _ecsWorld.NewEntity().Get<LoadLeaderBoardEvent>().LevelName = levelData.LevelName;
                _levelPreviewFilter.GetEntity(index).Destroy();
            }

            foreach (var index in _levelPreviewCloseFilter)
            {
                var levelPreviewRef = _sceneData.UI.LevelPreview;
                levelPreviewRef.gameObject.SetActive(false);
                
                for (int i = 0; i < levelPreviewRef.DifficultSculls.Length; i++)
                    levelPreviewRef.DifficultSculls[i].SetActive(false);
                
                _sceneData.AudioManager.EffectSource.PlayOneShot(
                    _staticData.LevelClickClip, 
                    _staticData.LevelClickClipVolume);
                
                _levelPreviewCloseFilter.GetEntity(index).Destroy();
            }
        }
    }
}