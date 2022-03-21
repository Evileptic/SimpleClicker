using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class TargetDamageSystem : IEcsRunSystem
    {
        private EcsFilter<Target, DamageEvent> _targetDamageFilter;

        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;

        public void Run()
        {
            foreach (var index in _targetDamageFilter)
            {
                ref var targetEntity = ref _targetDamageFilter.GetEntity(index);
                var targetActorRef = _targetDamageFilter.Get1(index).ActorRef;

                int targetDamage = _runtimeData.DoubleDamageBonusEnabled
                    ? _staticData.WithBonusDamage
                    : _staticData.DefaultDamage;

                targetActorRef.HealthPoints -= targetDamage;
                if (targetActorRef.HealthPoints <= 0)
                {
                    targetEntity.Destroy();
                    Object.Destroy(targetActorRef.gameObject);
                    SetUIProgress();
                    
                    if (_runtimeData.CurrentLevelData.Difficult.TargetsForWin == _runtimeData.KilledTargets)
                        _ecsWorld.NewEntity().Get<EndLevelEvent>().IsWin = true;
                    else
                        _ecsWorld.NewEntity().Get<SpawnTargetEvent>();
                }
                else
                {
                    targetEntity.Del<DamageEvent>();
                }
            }
        }

        private void SetUIProgress()
        {
            var gameMenu = _sceneData.UI.GameMenu;
            var currentDifficult = _runtimeData.CurrentLevelData.Difficult;
            gameMenu.ProgressText.text = 
                $"{++_runtimeData.KilledTargets} / {currentDifficult.TargetsForWin}";
            gameMenu.ProgressImage.fillAmount =
                _runtimeData.KilledTargets / (float) currentDifficult.TargetsForWin;
        }
    }
}