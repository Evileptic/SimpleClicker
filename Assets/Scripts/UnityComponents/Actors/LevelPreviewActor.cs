using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleClicker
{
    public class LevelPreviewActor : LevelWidgetActor
    {
        public Transform LeaderBoard;
        public TextMeshProUGUI WinrateText;
        public TextMeshProUGUI UsedBonuses;
        
        [SerializeField] private Button CloseButton;

        private void Start()
        {
            CloseButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnCloseButtonClick()
        {
            _ecsWorld.NewEntity().Get<CloseLevelPreviewEvent>();
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
