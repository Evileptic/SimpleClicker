using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleClicker
{
    public class LevelPreviewActor : LevelWidgetActor
    {
        public Transform LeaderBoard;
        
        [SerializeField] private Button CloseButton;

        private void Start()
        {
            CloseButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnCloseButtonClick()
        {
            _ecsWorld.NewEntity().Get<LevelPreviewCloseEvent>();
        }

        protected override void OnEntryButtonClick()
        {
            _ecsWorld.NewEntity().Get<StartGameEvent>().LevelData = LevelData;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            CloseButton.onClick.RemoveListener(OnCloseButtonClick);
        }
    }
}
