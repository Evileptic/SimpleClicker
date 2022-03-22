using System.IO;
using System.Text;
using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class LoadLeaderBoardSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<LoadLeaderBoardEvent> _loadLeaderBoardFilter;
        
        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;

        public void Init()
        {
            if (!Directory.Exists(_runtimeData.SaveDataPath))
                Directory.CreateDirectory(_runtimeData.SaveDataPath);
        }
        
        public void Run()
        {
            foreach (var index in _loadLeaderBoardFilter)
            {
                foreach (Transform leaderPreview in _sceneData.UI.LevelPreview.LeaderBoard)
                    Object.Destroy(leaderPreview.gameObject);
                
                var path = $"{_runtimeData.SaveDataPath}/{_loadLeaderBoardFilter.Get1(index).LevelName}.json";
                Leaderboard leaderboardData = null;
                if (!File.Exists(path))
                    leaderboardData = CreateFakeLeaderBoard(path);
                
                if (leaderboardData == null)
                {
                    byte[] leaderboardContent = File.ReadAllBytes(path);
                    var leaderboardString = Encoding.ASCII.GetString(leaderboardContent);
                    leaderboardData = JsonUtility.FromJson<Leaderboard>(leaderboardString);
                }

                foreach (var leaderInfo in leaderboardData.LeaderInfos)
                {
                    var leaderView = Object.Instantiate(_staticData.LeaderViewPrefab,
                        _sceneData.UI.LevelPreview.LeaderBoard);
                    leaderView.NicknameText.text = leaderInfo.Nickname;
                    leaderView.ScoreText.text = $"{leaderInfo.Score:F1}";
                }
            }
        }

        private Leaderboard CreateFakeLeaderBoard(string path)
        {
            Leaderboard leaderboardData = new Leaderboard
            {
                LeaderInfos = new LeaderInfo[_staticData.FakeLeaderBoaed.Length]
            };
            
            for (int i = 0; i < _staticData.FakeLeaderBoaed.Length; i++)
            {
                leaderboardData.LeaderInfos[i] = new LeaderInfo()
                {
                    Nickname = _staticData.FakeLeaderBoaed[i].Nickname,
                    Score = _staticData.FakeLeaderBoaed[i].Score
                };
            }
            
            string leaderboardString = JsonUtility.ToJson(leaderboardData);
            byte[] leaderboardContent = Encoding.ASCII.GetBytes(leaderboardString);
            File.WriteAllBytes(path, leaderboardContent);
            return leaderboardData;
        }
    }
}