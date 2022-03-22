using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class BonusSystem : IEcsRunSystem
    {
        private EcsFilter<Bonus, BonusClickEvent> _bonusClickFilter;
        private EcsFilter<WaitForBonusEndFlag> _waitForBonusEndFilter;

        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private EcsWorld _ecsWorld;
        
        public void Run()
        {
            foreach (var index in _bonusClickFilter)
            {
                var bonusActorRef = _bonusClickFilter.Get1(index).ActorRef;
                switch (bonusActorRef.BonusType)
                {
                    case BonusType.Double:
                        _runtimeData.DoubleBonusEnabled = true;
                        break;
                    case BonusType.Size:
                        _runtimeData.SizeBonusEnabled = true;
                        break;
                    case BonusType.Freeze:
                        _runtimeData.FreezeBonusEnabled = true;
                        break;
                }
                Object.Destroy(bonusActorRef.gameObject);
                _runtimeData.BonusMode = true;
                _runtimeData.PlayerData.PlayerLevelsData[_runtimeData.CurrentLevelData.Id].UsedBonuses++;
                _ecsWorld.NewEntity().Get<WaitForBonusEndFlag>().WaitTimer = bonusActorRef.BonusTime;
            }

            foreach (var index in _waitForBonusEndFilter)
            {
                ref var timerComp = ref _waitForBonusEndFilter.Get1(index);
                if (timerComp.WaitTimer <= 0f)
                {
                    _runtimeData.BonusMode = false;
                    _runtimeData.DoubleBonusEnabled = false;
                    _runtimeData.SizeBonusEnabled = false;
                    _runtimeData.FreezeBonusEnabled = false;
                    _runtimeData.TargetBonusRemains = _staticData.TargetsForBonus;
                    _waitForBonusEndFilter.GetEntity(index).Destroy();
                }
                else
                {
                    timerComp.WaitTimer -= Time.deltaTime;
                }
            }
        }
    }
}