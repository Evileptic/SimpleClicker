using System;
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
        private StaticData _staticData;
        private EcsWorld _ecsWorld;

        public void Run()
        {
            foreach (var index in _loadPlayerDataFilter)
            {
                var path = $"{_runtimeData.SaveDataPath}/PlayerData.json";
                if (!File.Exists(path))
                {
                    _runtimeData.PlayerData = CreatePlayerData(path);
                }
                else
                {
                    byte[] playerDataContent = File.ReadAllBytes(path);
                    var playerDataString = Encoding.ASCII.GetString(playerDataContent);
                    _runtimeData.PlayerData = JsonUtility.FromJson<PlayerData>(playerDataString);
                }

                ref var playerLevelsData = ref _runtimeData.PlayerData.PlayerLevelsData;
                if (_staticData.Levels.Length > playerLevelsData.Length)
                {
                    int oldArrayLength = playerLevelsData.Length;
                    Array.Resize(ref playerLevelsData, _staticData.Levels.Length);
                    int arraySpaces = playerLevelsData.Length - oldArrayLength;
                    for (int i = 0; i < arraySpaces; i++)
                        playerLevelsData[oldArrayLength + i] = new PlayerLevelData();
                    _ecsWorld.NewEntity().Get<SavePlayerDataEvent>();
                }
            }
        }

        private PlayerData CreatePlayerData(string path)
        {
            PlayerData playerData = new PlayerData
            {
                PlayerLevelsData = new PlayerLevelData[1]
            };
            
            string playerDataString = JsonUtility.ToJson(playerData);
            byte[] playerDataContent = Encoding.ASCII.GetBytes(playerDataString);
            File.WriteAllBytes(path, playerDataContent);
            return playerData;
        }
    }
}