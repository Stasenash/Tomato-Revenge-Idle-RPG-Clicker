using System.Collections.Generic;
using Global;
using Global.SaveSystem;
using TMPro;
using UnityEngine;

namespace Game.Statistics
{
    public class StatisticsViewer : MonoBehaviour
    {
        [SerializeField] private StatisticsManager _statisticsManager;
        [SerializeField] private TextMeshProUGUI _statisticsText;
        private SaveSystem _saveSystem;

        public void Initialize(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _statisticsManager.Initialize(_saveSystem);
        }
        public void ShowWinStatistics()
        {
            var enemyId = DataKeeper.EnemyId;
            var reward = DataKeeper.Reward;
            var totalHits = _statisticsManager.GetEnemyHits(enemyId);
            var totalWins = _statisticsManager.GetEnemyDeaths(enemyId);
            var hits = _statisticsManager.GetEnemyTempHits(enemyId);
            _statisticsManager.ClearTemp(enemyId);

            _statisticsText.text = "Ударов по боссу: " + hits +"\nУдаров за все время: " + totalHits + "\nБосс побежден " + totalWins +
                                   " раз." + "\n " + "\nНаграда: " + reward +  " монет.";
        }

        public void ShowLoseStatistics()
        {
            var enemyId = DataKeeper.EnemyId;
            List<string> advices = new List<string>() { "Не забывайте прокачивать навыки", "Проходите обычные уровни, чтобы накопить монет" };
            var advice_num = Random.Range(0, advices.Count);
            var hits = _statisticsManager.GetEnemyTempHits(enemyId);
            var attempts = _statisticsManager.GetEnemyAttempts(enemyId);
            _statisticsManager.ClearTemp(enemyId);
            _statisticsText.text = "Ударов по боссу: " + hits +"\nВсего попыток: " + attempts + "\n " + "\nСовет: " + advices[advice_num];
            
        }
    }
}