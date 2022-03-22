using Leopotam.Ecs;

namespace SimpleClicker
{
    public class SetLevelsProgressSystem : IEcsRunSystem
    {
        private EcsFilter<SetLevelProgressEvent> _setLevelProgressFilter;

        private RuntimeData _runtimeData;
        
        public void Run()
        {
            foreach (var index in _setLevelProgressFilter)
            {
                for (int i = 0; i < _runtimeData.LevelEntries.Length; i++)
                {
                    var levelEntryRef = _runtimeData.LevelEntries[i];
                    if (i > _runtimeData.PlayerData.CurrentLevel)
                    {
                        levelEntryRef.Sculls.SetActive(false);
                        levelEntryRef.BlockedLevelImage.SetActive(true);
                        levelEntryRef.EntryButton.interactable = false;
                    }
                    else
                    {
                        levelEntryRef.Sculls.SetActive(true);
                        levelEntryRef.BlockedLevelImage.SetActive(false);
                        levelEntryRef.EntryButton.interactable = true;
                    }
                }
            }
        }
    }
}