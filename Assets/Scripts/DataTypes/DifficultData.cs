using UnityEngine;

namespace SimpleClicker
{
    [CreateAssetMenu]
    public class DifficultData : ScriptableObject
    {
        public int DifficultLevel;
        public int TargetsHealthPoints;
        public int SecondsForLevel;
        public int TargetsForWin;
    }
}
