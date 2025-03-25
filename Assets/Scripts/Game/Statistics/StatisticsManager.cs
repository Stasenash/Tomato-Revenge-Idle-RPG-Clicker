using System;
using System.IO;
using Game.Enemies;
using UnityEngine;

namespace Game.Statistics
{
    public class StatisticsManager : MonoBehaviour
    {
        private string filePath;
        private GameStatistic gameStats;

        public void Initialize()
        {
            filePath = Path.Combine(Application.persistentDataPath, "gameStats.json");
            LoadStats();
        }

        public void SaveStats()
        {
            string json = JsonUtility.ToJson(gameStats, true);
            File.WriteAllText(filePath, json);
        }

        public void LoadStats()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                gameStats = JsonUtility.FromJson<GameStatistic>(json);
            }
            else
            {
                gameStats = new GameStatistic();
            }
        }

        public int GetEnemyAttempts(int enemyId)
        {
            return gameStats.Enemies[enemyId].Attempts;
        } 
    
        public int GetEnemyHits(int enemyId)
        {
            return gameStats.Enemies[enemyId].Hits;
        } 
    
        public int GetEnemyDeaths(int enemyId)
        {
            return gameStats.Enemies[enemyId].Deaths;
        }

        public void UpdateEnemyStats(int enemyId, int hits, int deaths, int attempts)
        {
            var enemy = Array.Find(gameStats.Enemies, e => e.EnemyId == enemyId);
            if (enemy == null)
            {
                enemy = new EnemyStatistic { EnemyId = enemyId };
                Array.Resize(ref gameStats.Enemies, gameStats.Enemies.Length + 1);
                gameStats.Enemies[gameStats.Enemies.Length - 1] = enemy;
            }
            enemy.Hits += hits;
            enemy.Deaths += deaths;
            enemy.Attempts += attempts;
            SaveStats();
        }
    }
}
