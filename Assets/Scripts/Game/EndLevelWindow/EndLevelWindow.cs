using Game.Statistics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.EndLevelWindow
{
    public class EndLevelWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _winLevelWindow;
        [SerializeField] private GameObject _loseLevelWindow;

        [SerializeField] private Button _loseRestartButton;
        [SerializeField] private Button _winRestartButton;
    
        [SerializeField] private AudioSource _loseSound;
        [SerializeField] private AudioSource _winSound;
    
        [SerializeField] private StatisticsViewer _statisticsViewer;
    
        public event UnityAction OnRestartButtonClicked;

        public void Initialize()
        {
            _loseRestartButton.onClick.AddListener(() => Restart());
            _winRestartButton.onClick.AddListener(() => Restart());
        }
    
        public void ShowWinLevelWindow()
        {
            _winLevelWindow.SetActive(true);
            _loseLevelWindow.SetActive(false);
            gameObject.SetActive(true);
            _statisticsViewer.ShowWinStatistics();
            _winSound.Play();
        }
    
        public void ShowLoseLevelWindow()
        {
            _winLevelWindow.SetActive(false);
            _loseLevelWindow.SetActive(true);
            gameObject.SetActive(true);
            _statisticsViewer.ShowLoseStatistics();
            _loseSound.Play();
        }

        private void Restart()
        {
            OnRestartButtonClicked?.Invoke();
            gameObject.SetActive(false);
        }
    }
}