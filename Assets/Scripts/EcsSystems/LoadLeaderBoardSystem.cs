using System.IO;
using System.Text;
using Leopotam.Ecs;
using UnityEngine;

namespace SimpleClicker
{
    public class LoadLeaderBoardSystem : IEcsRunSystem
    {
        private EcsFilter<LoadLeaderBoardEvent> _loadLeaderBoardFilter;
        
        private RuntimeData _runtimeData;
        private StaticData _staticData;
        private SceneData _sceneData;
        private EcsWorld _ecsWorld;

        public void Run()
        {
            foreach (var index in _loadLeaderBoardFilter)
            {
                if (!Directory.Exists(_runtimeData.SaveDataPath))
                {
                    _loadLeaderBoardFilter.GetEntity(index).Destroy();
                    return;
                }

                foreach (Transform leaderPreview in _sceneData.UI.LevelPreview.LeaderBoard)
                    Object.Destroy(leaderPreview.gameObject);
                
                byte[] leaderboardContent =
                    File.ReadAllBytes($"{_runtimeData.SaveDataPath}/{_loadLeaderBoardFilter.Get1(index).LevelName}.json");
                var leaderboardString = Encoding.ASCII.GetString(leaderboardContent);
                var leaderboardData = JsonUtility.FromJson<Leaderboard>(leaderboardString);
                
                foreach (var leaderInfo in leaderboardData.LeaderInfos)
                {
                    var leaderView = Object.Instantiate(_staticData.LeaderViewPrefab,
                        _sceneData.UI.LevelPreview.LeaderBoard);
                    leaderView.NicknameText.text = leaderInfo.Nickname;
                    leaderView.ScoreText.text = $"{leaderInfo.Score:F1}";
                }

                _loadLeaderBoardFilter.GetEntity(index).Destroy();
            }
        }
    }
}