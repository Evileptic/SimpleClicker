using UnityEngine;

namespace SimpleClicker
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        [Header("Properties")]
        public LevelData[] Levels;
        public DifficultData DefaultDifficult;
        public int WithoutDoubleBonus;
        public int WithDoubleBonus;
        public string PlayerNickname;
        public string SaveDataFolder;
        public int TargetsForBonus;
        public float SizeBonusMultiplier;
        public float SpawnLimitShift;
        public Sprite DefaultBonusSprite;
        public LeaderInfo[] FakeLeaderBoaed;

        [Header("Prefabs")] 
        public LevelEntryActor LevelEntryPrefab;
        public GameObject BlockedEntryPrefab;
        public TargetActor TargetPrefab;
        public LeaderView LeaderViewPrefab;
        public BonusActor[] BonusActors;

        [Header("AudioClips")] 
        public AudioClip LevelClickClip;
        public float LevelClickClipVolume;
        public AudioClip StartGameClip;
        public float StartGameClipVolume;
        public AudioClip BeatClip;
        public float BeatClipVolume;
        public AudioClip BonusClip;
        public float BonusClipVolume;
    }
}