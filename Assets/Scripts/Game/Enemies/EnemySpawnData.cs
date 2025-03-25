using System;

namespace Game.Enemies {
    [Serializable]
    public struct EnemySpawnData {
        public string Id;
        public float Hp;
        public bool IsBoss;
        public float BossTime;
    }
}