using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class TimerSystem : IEcsRunSystem
    {
        private EcsFilter<StartTimerEvent> _startTimerEventFilter;
        private EcsFilter<StartTimerFlag> _startTimerFlagFilter;
        private EcsFilter<StopTimerEvent> _stopTimerEventFilter;

        private RuntimeData _runtimeData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;

        private float timer;

        public void Run()
        {
            foreach (var index in _startTimerEventFilter)
            {
                ClearTimers();
                timer = _startTimerEventFilter.Get1(index).Timer + 1f;
                _ecsWorld.NewEntity().Get<StartTimerFlag>();
                _startTimerEventFilter.GetEntity(index).Destroy();
            }

            foreach (var index in _startTimerFlagFilter)
            {
                if (timer <= 0f)
                {
                    // WIN OR LOSE
                    _startTimerFlagFilter.GetEntity(index).Destroy();
                }
                else
                {
                    _sceneData.UI.GameMenu.TimerText.text = $"{(int) timer}";
                    _runtimeData.PlayerTimer += Time.deltaTime;
                    timer -= Time.deltaTime;
                }
            }

            foreach (var index in _stopTimerEventFilter)
            {
                ClearTimers();
                _stopTimerEventFilter.GetEntity(index).Destroy();
            }
        }

        private void ClearTimers()
        {
            if (_startTimerFlagFilter.GetEntitiesCount() > 0)
                foreach (var timerIndex in _startTimerFlagFilter)
                    _startTimerFlagFilter.GetEntity(timerIndex).Destroy();
            _runtimeData.PlayerTimer = 0f;
        }
    }
}