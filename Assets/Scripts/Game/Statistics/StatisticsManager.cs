using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Game.Enemies;
using Global;
using Global.SaveSystem;
using UnityEngine;

namespace Game.Statistics
{
    public class StatisticsManager : MonoBehaviour
    {
        private SaveSystem _saveSystem;

        public void Initialize(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        public int GetEnemyAttempts(string enemyId)
        {
            var gameStats = (Global.SaveSystem.SavableObjects.Statistics)_saveSystem.GetData(SavableObjectType.Statistics);
            foreach (var enemyStatistic in gameStats.EnemiesStatistics)
            {
                if (enemyStatistic.EnemyId == enemyId)
                    return enemyStatistic.TotalAttempts;
            }

            return 0;
        } 
    
        public int GetEnemyHits(string enemyId)
        {
            var gameStats = (Global.SaveSystem.SavableObjects.Statistics)_saveSystem.GetData(SavableObjectType.Statistics);
            foreach (var enemyStatistic in gameStats.EnemiesStatistics)
            {
                if (enemyStatistic.EnemyId == enemyId)
                    return enemyStatistic.TotalHits;
            }

            return 0;
        } 
    
        public int GetEnemyDeaths(string enemyId)
        {
            var gameStats = (Global.SaveSystem.SavableObjects.Statistics)_saveSystem.GetData(SavableObjectType.Statistics);
            foreach (var enemyStatistic in gameStats.EnemiesStatistics)
            {
                if (enemyStatistic.EnemyId == enemyId)
                    return enemyStatistic.TotalDeaths;
            }

            return 0;
        }

        public void UpdateEnemyStats(int hits, int deaths, int attempts)
        {
            var gameStats = (Global.SaveSystem.SavableObjects.Statistics)_saveSystem.GetData(SavableObjectType.Statistics);
            if (DataKeeper.IsBoss)
            {
                var enemyId = DataKeeper.EnemyId;
                var enemy = gameStats.GetOrCreateEnemyStatistics(enemyId);
                
                enemy.TotalAttempts += attempts;
                enemy.TotalDeaths += deaths;
                enemy.TotalHits += hits;
                enemy.TempHits += hits;
                _saveSystem.SaveData(SavableObjectType.Statistics);
            }
        }

        public int GetEnemyTempHits(string enemyId)
        {
            var gameStats = (Global.SaveSystem.SavableObjects.Statistics)_saveSystem.GetData(SavableObjectType.Statistics);
            foreach (var enemyStatistic in gameStats.EnemiesStatistics)
            {
                if (enemyStatistic.EnemyId == enemyId)
                    return enemyStatistic.TempHits;
            }

            return 0;
        }

        public void ClearTemp(string enemyId)
        {
            var gameStats = (Global.SaveSystem.SavableObjects.Statistics)_saveSystem.GetData(SavableObjectType.Statistics);
            foreach (var enemyStatistic in gameStats.EnemiesStatistics)
            {
                if (enemyStatistic.EnemyId == enemyId)
                {
                    enemyStatistic.TempHits = 0;
                }
            }
            _saveSystem.SaveData(SavableObjectType.Statistics);
        }
    }
}
