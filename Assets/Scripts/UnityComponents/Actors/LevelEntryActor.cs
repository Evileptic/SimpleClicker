using Leopotam.Ecs;

namespace SimpleClicker
{
    public class LevelEntryActor : LevelWidgetActor
    {
        protected override void OnEntryButtonClick()
        {
            _ecsWorld.NewEntity().Get<OpenLevelPreviewEvent>().LevelData = LevelData;
        }
    }
}