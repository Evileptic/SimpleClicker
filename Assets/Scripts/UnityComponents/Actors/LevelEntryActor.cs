using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class LevelEntryActor : LevelWidgetActor
    {
        public GameObject BlockedLevelImage;
        public GameObject Sculls;

        protected override void OnEntryButtonClick()
        {
            _ecsWorld.NewEntity().Get<OpenLevelPreviewEvent>().LevelData = LevelData;
        }
    }
}