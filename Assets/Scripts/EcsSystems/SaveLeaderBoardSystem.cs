using Leopotam.Ecs;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleClicker
{
    public class SaveLeaderBoardSystem : IEcsRunSystem
    {
        private EcsFilter<SaveLeaderBoardEvent> _saveLeaderBoardFilter;

        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;

        public void Run()
        {
            foreach (var index in _saveLeaderBoardFilter)
            {
                var playerNick = _staticData.PlayerNickname;
                var levelName = _runtimeData.CurrentLevelData.LevelName;
                byte[] leaderboardContent = File.ReadAllBytes($"{_runtimeData.SaveDataPath}/{levelName}.json");
                var leaderboardString = Encoding.ASCII.GetString(leaderboardContent);
                var leaderboardData = JsonUtility.FromJson<Leaderboard>(leaderboardString);
                var leaderboard = _runtimeData.Leaderboard;
                leaderboard.Clear();
                
                foreach (var leaderInfo in leaderboardData.LeaderInfos)
                    leaderboard.Add(leaderInfo.Nickname, leaderInfo.Score);
                
                if (leaderboard.ContainsKey(playerNick))
                    if (_runtimeData.PlayerTimer < leaderboard[playerNick])
                        leaderboard[playerNick] = _runtimeData.PlayerTimer;
                    else
                        return;
                else
                    leaderboard.Add(playerNick, _runtimeData.PlayerTimer);

                leaderboard = leaderboard
                    .OrderBy(x => x.Value)
                    .ToDictionary(x => x.Key, x => x.Value);
                leaderboardData.LeaderInfos = new LeaderInfo[leaderboard.Count];
                int iterator = 0;
                foreach (var leaderInfo in leaderboard)
                {
                    leaderboardData.LeaderInfos[iterator] = new LeaderInfo
                    {
                        Nickname = leaderInfo.Key,
                        Score = leaderInfo.Value
                    };
                    iterator++;
                }

                leaderboardString = JsonUtility.ToJson(leaderboardData);
                leaderboardContent = Encoding.ASCII.GetBytes(leaderboardString);
                File.WriteAllBytes($"{_runtimeData.SaveDataPath}/{levelName}.json", leaderboardContent);
                
                leaderboard.Clear();
                
                _ecsWorld.NewEntity().Get<LoadLeaderBoardEvent>().LevelName = _runtimeData.CurrentLevelData.LevelName;
            }
        }
    }
}