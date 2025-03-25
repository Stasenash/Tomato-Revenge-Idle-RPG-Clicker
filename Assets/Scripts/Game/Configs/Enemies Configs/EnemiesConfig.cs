using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    [CreateAssetMenu(menuName = "Configs/EnemiesConfig",fileName = "EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject
    {
        public Enemy EnemyPrefab;
        public List<EnemyData> Enemies;

        public EnemyData GetEnemy(string id)
        {
            foreach (var enemyData in Enemies)
            {
                if (enemyData.Id == id)
                    return enemyData;
            }
            Debug.LogError($"Not found enemy with id: {id}");
            return default;
        }
    }
}