using TMPro;
using UnityEngine;

namespace Game.Statistics
{
    public class StatisticsViewer : MonoBehaviour
    {
        [SerializeField] private StatisticsManager _statisticsManager;
        [SerializeField] private TextMeshProUGUI _statisticsText;

        public void ShowWinStatistics()
        {
            _statisticsManager.Initialize();
            int hits = _statisticsManager.GetEnemyHits(0);
            int wins = _statisticsManager.GetEnemyDeaths(0);

            _statisticsText.text = "Всего ударов: " + hits + "\nБосс побежден " + wins +
                                   " раз.";
        }

        public void ShowLoseStatistics()
        {
            _statisticsManager.Initialize();
            int attempts = _statisticsManager.GetEnemyAttempts(0);
            _statisticsText.text = "Всего попыток: " + attempts;
        }
    }
}