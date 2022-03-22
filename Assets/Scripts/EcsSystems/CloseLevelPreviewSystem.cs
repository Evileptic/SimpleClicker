using Leopotam.Ecs;

namespace SimpleClicker
{
    public class CloseLevelPreviewSystem : IEcsRunSystem
    {
        private EcsFilter<CloseLevelPreviewEvent> _levelPreviewCloseFilter;
        
        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;
        
        public void Run()
        {
            foreach (var index in _levelPreviewCloseFilter)
            {
                var levelPreviewRef = _sceneData.UI.LevelPreview;
                levelPreviewRef.gameObject.SetActive(false);

                for (int i = 0; i < levelPreviewRef.DifficultSculls.Length; i++)
                    levelPreviewRef.DifficultSculls[i].SetActive(false);

                _sceneData.AudioManager.EffectSource.PlayOneShot(
                    _staticData.LevelClickClip,
                    _staticData.LevelClickClipVolume);
            }
        }
    }
}