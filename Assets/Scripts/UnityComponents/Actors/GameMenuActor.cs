using Leopotam.Ecs;
using TMPro;
using UnityEngine.UI;

namespace SimpleClicker
{
    public class GameMenuActor : Actor
    {
        public TextMeshProUGUI TimerText;
        public TextMeshProUGUI TargetsText;
        public TextMeshProUGUI ProgressText;
        public Image ProgressImage;
        public Image BonusImage;
        
        protected override void ExpandEntity(EcsEntity entity)
        {
            entity.Get<GameMenu>().ActorRef = this;
        }
    }
}