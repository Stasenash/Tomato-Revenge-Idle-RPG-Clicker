using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Configs.LevelConfigs {
    [CreateAssetMenu(menuName="Configs/LevelsConfig", fileName = "LevelsConfig")]
    public class LevelsConfig : ScriptableObject {
        public List<LevelData> Levels;
        
        public LevelData GetLevel(int location, int level) {
            foreach (var levelsData in Levels) {
                if (levelsData.Location != location || levelsData.LevelNumber != level) continue;
                return levelsData;
            }
            
            Debug.LogError($"Not found Level data for location {location} and level {level}");
            return default;
        }

        public int GetMaxLevelOnLocation()
        {
            return Levels.Count - 1;
        }

        public int GetMaxLocationNum()
        {
            var maxLocation = 0;
            foreach (var levelsData in Levels) {
                if (levelsData.Location > maxLocation) maxLocation = levelsData.Location;
            }

            return maxLocation;
        }
    }
    
}