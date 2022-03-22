using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleClicker
{
    public enum BonusType { Double, Size, Freeze }
    
    public class BonusActor : Actor, IPointerClickHandler
    {
        public BonusType BonusType;
        public float BonusTime;
        public RectTransform RectTransform;
        
        protected override void ExpandEntity(EcsEntity entity)
        {
            entity.Get<Bonus>().ActorRef = this;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _ecsEntity.Get<BonusClickEvent>();
        }
    }
}