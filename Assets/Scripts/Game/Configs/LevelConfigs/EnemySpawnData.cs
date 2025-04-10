using System;
using UnityEngine.Serialization;

namespace Game.Enemies {
    [Serializable]
    public struct EnemySpawnData {
        public string Id;
        public float Hp;
        public bool IsBoss;
        public float BossTime;
        [FormerlySerializedAs("Technique")] public TechniqueType techniqueType;
    }
}