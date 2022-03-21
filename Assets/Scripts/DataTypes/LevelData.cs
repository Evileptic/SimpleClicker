using UnityEngine;

namespace SimpleClicker
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    {
        public string LevelName;
        public DifficultData Difficult;
        public Sprite BackgroundImage;
        public Sprite TargetImage;
    }
}