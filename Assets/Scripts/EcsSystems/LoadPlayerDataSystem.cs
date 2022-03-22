using Leopotam.Ecs;
using System.IO;
using System.Text;
using UnityEngine;

namespace SimpleClicker
{
    public class LoadPlayerDataSystem : IEcsRunSystem
    {
        private EcsFilter<LoadPlayerDataEvent> _loadPlayerDataFilter;

        private RuntimeData _runtimeData;
        
        public void Run()
        {
            foreach (var index in _loadPlayerDataFilter)
            {
                if (!Directory.Exists(_runtimeData.SaveDataPath))
                    return;
                
                byte[] playerDataContent =
                    File.ReadAllBytes($"{_runtimeData.SaveDataPath}/PlayerData.json");
                var playerDataString = Encoding.ASCII.GetString(playerDataContent);
                _runtimeData.PlayerData = JsonUtility.FromJson<PlayerData>(playerDataString);
            }
        }
    }
}