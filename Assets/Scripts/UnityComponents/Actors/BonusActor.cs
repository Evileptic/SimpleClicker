using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleClicker
{
    public enum BonusType { Double, Size, Freeze }
    
    public class BonusActor : Actor, IPointerClickHandler
    {
        public BonusType BonusType;
        public float BonusTime;
        public RectTransform RectTransform;
        public Image BonusIcon;
        
        protected override void ExpandEntity(EcsEntity entity) => entity.Get<Bonus>().ActorRef = this;
        public void OnPointerClick(PointerEventData eventData) => _ecsEntity.Get<BonusClickEvent>();
        private void OnDestroy() => _ecsEntity.Destroy();
    }
}