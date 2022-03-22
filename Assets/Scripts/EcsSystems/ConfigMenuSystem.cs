using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class ConfigMenuSystem : IEcsRunSystem
    {
        private EcsFilter<ConfigMenuOpenEvent> _configMenuOpenFilter;
        private EcsFilter<LoadAudioSettingsEvent> _loadAudioSettingsFilter;
        private EcsFilter<ConfigMenu, ChangeAudioSettingsEvent> _changeAudioSettingsFilter;
        private EcsFilter<ConfigMenuCloseEvent> _configMenuCloseFilter;

        private StaticData _staticData;
        private SceneData _sceneData;
        private static readonly int AppearTrigger = Animator.StringToHash("Appear");
        private static readonly int DisappearTrigger = Animator.StringToHash("Disappear");

        public void Run()
        {
            foreach (var index in _configMenuOpenFilter)
            {
                _sceneData.UI.ConfigMenu.CloseButton.gameObject.SetActive(true);
                _sceneData.AudioManager.EffectSource.PlayOneShot(
                    _staticData.LevelClickClip, 
                    _staticData.LevelClickClipVolume);
                _sceneData.UI.ConfigMenu.Animator.SetTrigger(AppearTrigger);
                _configMenuOpenFilter.GetEntity(index).Destroy();
            }
            
            foreach (var index in _loadAudioSettingsFilter)
            {
                var sfxIsOn = PlayerPrefs.GetInt("SFX", 1);
                var musicIsOn = PlayerPrefs.GetInt("MUSIC", 1);
                
                _sceneData.AudioManager.EffectSource.volume = sfxIsOn;
                _sceneData.AudioManager.MusicSource.volume = musicIsOn;

                _sceneData.UI.ConfigMenu.SfxToggle.isOn = sfxIsOn > 0;
                _sceneData.UI.ConfigMenu.MusicToggle.isOn = musicIsOn > 0;
                
                _loadAudioSettingsFilter.GetEntity(index).Destroy();
            }
            
            foreach (var index in _changeAudioSettingsFilter)
            {
                ref var configMenuEntity = ref _changeAudioSettingsFilter.GetEntity(index);
                var configMenuActorRef = _changeAudioSettingsFilter.Get1(index).ActorRef;

                var sfxIsOn = configMenuActorRef.SfxToggle.isOn ? 1 : 0;
                var musicIsOn = configMenuActorRef.MusicToggle.isOn ? 1 : 0;

                _sceneData.AudioManager.EffectSource.volume = sfxIsOn;
                _sceneData.AudioManager.MusicSource.volume = musicIsOn;
                
                PlayerPrefs.SetInt("SFX", sfxIsOn);
                PlayerPrefs.SetInt("MUSIC", musicIsOn);
                
                configMenuEntity.Del<ChangeAudioSettingsEvent>();
            }

            foreach (var index in _configMenuCloseFilter)
            {
                _sceneData.UI.ConfigMenu.CloseButton.gameObject.SetActive(false);
                _sceneData.AudioManager.EffectSource.PlayOneShot(
                    _staticData.LevelClickClip, 
                    _staticData.LevelClickClipVolume);
                _sceneData.UI.ConfigMenu.Animator.SetTrigger(DisappearTrigger);
                _configMenuCloseFilter.GetEntity(index).Destroy();;
            }
        }
    }
}