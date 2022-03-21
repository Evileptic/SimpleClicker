using UnityEngine;

namespace SimpleClicker
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        [Header("Properties")]
        public LevelData[] Levels;
        public DifficultData DefaultDifficult;
        public int DefaultDamage;
        public int WithBonusDamage;

        [Header("Prefabs")] 
        public LevelEntryActor LevelEntryPrefab;
        public TargetActor TargetPrefab;
        public BonusActor DoubleDamageBonusPrefab;
        public BonusActor DoubleSizeBonusPrefab;
        public BonusActor TargetStunBonusPrefab;

        [Header("AudioClips")] 
        public AudioClip LevelClickClip;
        public float LevelClickClipVolume;
        public AudioClip StartGameClip;
        public float StartGameClipVolume;
    }
}