using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleClicker
{
    public class TargetActor : Actor, IPointerClickHandler
    {
        public Image TargetImage;
        public RectTransform RectTransform;
        
        protected override void ExpandEntity(EcsEntity entity)
        {
            entity.Get<Target>().ActorRef = this;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _ecsEntity.Get<DamageEvent>();
        }
    }
}