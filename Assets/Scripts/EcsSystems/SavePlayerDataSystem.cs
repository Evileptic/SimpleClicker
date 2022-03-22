using System.IO;
using System.Text;
using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class SavePlayerDataSystem : IEcsRunSystem
    {
        private EcsFilter<SavePlayerDataEvent> _loadPlayerDataFilter;

        private RuntimeData _runtimeData;
        private EcsWorld _ecsWorld;
        
        public void Run()
        {
            foreach (var index in _loadPlayerDataFilter)
            {
                string playerDataString = JsonUtility.ToJson(_runtimeData.PlayerData);
                byte[] playerDataContent = Encoding.ASCII.GetBytes(playerDataString);
                File.WriteAllBytes($"{_runtimeData.SaveDataPath}/PlayerData.json", playerDataContent);
            }
        }
    }
}