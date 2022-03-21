using Leopotam.Ecs;

namespace SimpleClicker
{
    public class BonusActor : Actor
    {
        protected override void ExpandEntity(EcsEntity entity)
        {
            entity.Get<Bonus>().ActorRef = this;
        }
    }
}