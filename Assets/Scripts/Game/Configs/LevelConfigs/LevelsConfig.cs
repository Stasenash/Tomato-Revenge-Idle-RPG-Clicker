using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Configs.LevelConfigs {
    [CreateAssetMenu(menuName="Configs/LevelsConfig", fileName = "LevelsConfig")]
    public class LevelsConfig : ScriptableObject {
        [SerializeField] private List<LevelData> _levels;
        private Dictionary<int, Dictionary<int, LevelData>> _levelsMap;

        public LevelData GetLevel(int location, int level)
        {
            if (_levelsMap.IsNullOrEmpty())
                FillLevelMap();
            return _levelsMap[location][level];
        }

        public int GetMaxLevelOnLocation(int location)
        {
            if (_levelsMap.IsNullOrEmpty()) FillLevelMap();
            var maxLevel = -1;
            foreach (var levelNumber in _levelsMap[location].Keys)
            {
                if (levelNumber <= maxLevel) continue;
                maxLevel = levelNumber;
            }
            return maxLevel;
        }

        //TODO: переписать метод под словари
        public int GetMaxLocationNum()
        {
            var maxLocation = -1;
            foreach (var levelsData in _levels) {
                if (levelsData.Location > maxLocation) maxLocation = levelsData.Location;
            }

            return maxLocation;
        }

        public Vector2Int GetMaxLocationAndLevel()
        {
            var locationAndLevel = new Vector2Int();
            locationAndLevel.x = GetMaxLocationNum();
            locationAndLevel.y = GetMaxLevelOnLocation(locationAndLevel.x);
            return locationAndLevel;
        }

        public string GetLocationName(int location)
        {
            switch (location)
            {
                case 0: return "Деревня грибов";
                case 1: return "Деревня орехов";
                case 2: return "Деревня овощей";
                case 3: return "Деревня фруктов";
            }

            return "Незнакомая локация";
        }

        private void FillLevelMap()
        {
            _levelsMap = new();
            foreach (var levelData in _levels)
            {
                var locationMap = _levelsMap.GetOrCreate(levelData.Location);
                locationMap[levelData.LevelNumber] = levelData;
            }
        }

        public int GetReward(int location, int level)
        {
            return _levelsMap[location][level].Reward;
        }
    }
    
}