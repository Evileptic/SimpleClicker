using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SimpleClicker
{
    public abstract class LevelWidgetActor : Actor
    {
        public LevelData LevelData;
        public GameObject[] DifficultSculls;
        public Image BackgroundImage;
        public TextMeshProUGUI LevelNameText;

        [SerializeField] private Button EntryButton;

        protected override void ExpandEntity(EcsEntity entity)
        {
            entity.Get<LevelWidget>().ActorRef = this;
            EntryButton.onClick.AddListener(OnEntryButtonClick);
        }

        protected abstract void OnEntryButtonClick();

        protected virtual void OnDestroy()
        {
            EntryButton.onClick.RemoveListener(OnEntryButtonClick);
        }
    }
}
