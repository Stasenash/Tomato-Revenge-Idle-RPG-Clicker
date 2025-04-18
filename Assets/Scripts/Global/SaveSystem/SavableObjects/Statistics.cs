using System;
using System.Collections.Generic;
using Extensions;
using Game.Statistics;

namespace Global.SaveSystem.SavableObjects
{
    [Serializable]
    public class Statistics : ISavable
    {
        public List<EnemyStatistic> EnemiesStatistics = new();

        private Dictionary<string, EnemyStatistic> _enemiesMap = new();

        public EnemyStatistic GetEnemiesStatistics(string enemyId)
        {
            if (_enemiesMap.IsNullOrEmpty()) FillEnemiesMap();

            return _enemiesMap.ContainsKey(enemyId) ? _enemiesMap[enemyId] : null;
        }

        private void FillEnemiesMap()
        {
            _enemiesMap = new Dictionary<string, EnemyStatistic>();

            foreach (var enemy in EnemiesStatistics)
            {
                _enemiesMap.Add(enemy.EnemyId, enemy);
            }
        }

        public EnemyStatistic GetOrCreateEnemyStatistics(string enemyId)
        {
            var enemiesStatistics = GetEnemiesStatistics(enemyId);

            if (enemiesStatistics == null)
            {
                enemiesStatistics = new EnemyStatistic()
                {
                    EnemyId = enemyId,
                };

                EnemiesStatistics.Add(enemiesStatistics);
                _enemiesMap.Add(enemiesStatistics.EnemyId, enemiesStatistics);
            }

            return enemiesStatistics;
        }
    }
}