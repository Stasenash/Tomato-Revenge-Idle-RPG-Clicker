using System;
using Game.RSPConfig;

namespace Game.Configs.LevelConfigs {
    [Serializable]
    public struct EnemySpawnData {
        public string Id;
        public float Hp;
        public bool IsBoss;
        public float BossTime;
        public TechniqueType TechniqueType;
    }
}