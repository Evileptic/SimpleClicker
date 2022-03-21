using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public abstract class Actor : MonoBehaviour
    {
        protected EcsEntity _ecsEntity;
        protected EcsWorld _ecsWorld;

        public void Init(EcsWorld world)
        {
            _ecsWorld = world;
            _ecsEntity = _ecsWorld.NewEntity();
            ExpandEntity(_ecsEntity);
        }

        protected abstract void ExpandEntity(EcsEntity entity);
    }
}
