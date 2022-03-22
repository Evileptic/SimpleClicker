using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleClicker
{
    public class ConfigMenuActor : Actor
    {
        public Toggle SfxToggle;
        public Toggle MusicToggle;
        public Button OpenButton;
        public Button CloseButton;
        public Animator Animator;

        protected override void ExpandEntity(EcsEntity entity)
        {
            entity.Get<ConfigMenu>().ActorRef = this;
            SfxToggle.onValueChanged.AddListener(OnToggleClick);
            MusicToggle.onValueChanged.AddListener(OnToggleClick);
            OpenButton.onClick.AddListener(OnOpenButtonClick);
            CloseButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnOpenButtonClick() => _ecsWorld.NewEntity().Get<ConfigMenuOpenEvent>();
        private void OnCloseButtonClick() => _ecsWorld.NewEntity().Get<ConfigMenuCloseEvent>();
        private void OnToggleClick(bool value) => _ecsEntity.Get<ChangeAudioSettingsEvent>();
        
        private void OnDestroy()
        {
            SfxToggle.onValueChanged.RemoveListener(OnToggleClick);
            MusicToggle.onValueChanged.RemoveListener(OnToggleClick);
            OpenButton.onClick.RemoveListener(OnOpenButtonClick);
            CloseButton.onClick.RemoveListener(OnCloseButtonClick);
        }
    }
}