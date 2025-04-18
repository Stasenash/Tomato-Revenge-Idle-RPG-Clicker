﻿using System;
using System.Collections.Generic;
using Game.Enemies;
using UnityEngine;

namespace Game.Configs.LevelConfigs {
    [Serializable]
    public struct LevelData {
        public int Location;
        public int LevelNumber;
        public List<EnemySpawnData> Enemies;
        public int Reward;
        public Sprite Background;
    }
}