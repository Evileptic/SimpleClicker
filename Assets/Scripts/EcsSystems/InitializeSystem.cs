using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class InitializeSystem : IEcsInitSystem
    {
        private RuntimeData _runtimeData;
        private EcsWorld _ecsWorld;
        
        public void Init()
        {
            _runtimeData.MainCamera = Camera.main;

            var actors = Object.FindObjectsOfType<Actor>(true);
            foreach (var actor in actors)
                actor.Init(_ecsWorld);

            _ecsWorld.NewEntity().Get<GenerateMenuEvent>();
        }
    }
}