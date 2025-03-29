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
        [SerializeField] private Button _winNextButton;
        
        [SerializeField] private Button _winBackButton;
    
        [SerializeField] private AudioSource _loseSound;
        [SerializeField] private AudioSource _winSound;
    
        [SerializeField] private StatisticsViewer _statisticsViewer;
    
        public event UnityAction OnRestartButtonClicked;
        public event UnityAction OnNextButtonClicked;
        public event UnityAction OnBackButtonClicked;
        
        public void Initialize()
        {
            _loseRestartButton.onClick.AddListener(() => Restart());
            _winNextButton.onClick.AddListener(() => NextLevel());
            _winBackButton.onClick.AddListener((() => Back()));
        }

        private void Back()
        {
            OnBackButtonClicked?.Invoke();
            gameObject.SetActive(false);
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

        private void NextLevel()
        {
            OnNextButtonClicked?.Invoke();
            gameObject.SetActive(false);
        }
    }
}