using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class TargetDamageSystem : IEcsRunSystem
    {
        private EcsFilter<Target, TargetDamageEvent> _targetDamageFilter;

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

                int killValue = _runtimeData.DoubleBonusEnabled
                    ? _staticData.WithDoubleBonus
                    : _staticData.WithoutDoubleBonus;

                targetEntity.Destroy();
                Object.Destroy(targetActorRef.gameObject);
                SetUIProgress(killValue);

                if (_runtimeData.KilledTargets >= _runtimeData.CurrentLevelData.Difficult.TargetsForWin)
                    _ecsWorld.NewEntity().Get<EndLevelEvent>().IsWin = true;
                else
                    _ecsWorld.NewEntity().Get<SpawnTargetEvent>();

                if (--_runtimeData.TargetBonusRemains == 0)
                    _ecsWorld.NewEntity().Get<SpawnBonusEvent>();
            }
        }

        private void SetUIProgress(int killValue)
        {
            var gameMenu = _sceneData.UI.GameMenu;
            var currentDifficult = _runtimeData.CurrentLevelData.Difficult;
            _runtimeData.KilledTargets += killValue;
            gameMenu.ProgressText.text =
                $"{_runtimeData.KilledTargets} / {currentDifficult.TargetsForWin}";
            gameMenu.ProgressImage.fillAmount =
                _runtimeData.KilledTargets / (float) currentDifficult.TargetsForWin;
        }
    }
}