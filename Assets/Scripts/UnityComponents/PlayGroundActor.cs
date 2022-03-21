using Leopotam.Ecs;
using UnityEngine.UI;

namespace SimpleClicker
{
    public class PlayGroundActor : Actor
    {
        public Image Background;
        
        protected override void ExpandEntity(EcsEntity entity)
        {
            entity.Get<PlayGround>().ActorRef = this;
        }
    }
}