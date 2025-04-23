using System;
using DG.Tweening;
using Game.Statistics;
using Global;
using Global.AudioSystem;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

namespace Game.EndLevelWindow
{
    public class EndLevelWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _winLevelWindow;
        [SerializeField] private GameObject _loseLevelWindow;

        [SerializeField] private Button _loseRestartButton;
        [SerializeField] private Button _winNextButton;
        
        [SerializeField] private Button _winBackButton;
        [SerializeField] private Button _loseBackButton;
    
        [SerializeField] private StatisticsViewer _statisticsViewer;
        
        [SerializeField] private Button _x2AdvButton;
        private SaveSystem _saveSystem;
        private AudioManager _audioManager;

        public event UnityAction OnRestartButtonClicked;
        public event UnityAction OnNextButtonClicked;
        public event UnityAction OnBackButtonClicked;
        
        public void Initialize(SaveSystem saveSystem, AudioManager audioManager)
        {
            _saveSystem = saveSystem;
            _audioManager = audioManager;
            _statisticsViewer.Initialize(saveSystem);
            _loseRestartButton.onClick.AddListener(() => Restart());
            _winNextButton.onClick.AddListener(() => NextLevel());
            _winBackButton.onClick.AddListener((() => Back()));
            _loseBackButton.onClick.AddListener((() => Back()));

            _x2AdvButton.interactable = true;
            _x2AdvButton.onClick.AddListener(() => ShowAdvertisment());
        }

        private void ShowAdvertisment()
        {
            YG2.RewardedAdvShow("X2Button", SetReward);
            _x2AdvButton.interactable = false;
        }

        private void SetReward()
        {
            var wallet = (Wallet)_saveSystem.GetData(SavableObjectType.Wallet); 
            wallet.Coins += DataKeeper.Reward;
            _saveSystem.SaveData(SavableObjectType.Wallet);
        }

        private void Back()
        {
            OnBackButtonClicked?.Invoke();
            gameObject.SetActive(false);
        }

        public void ShowWinLevelWindow()
        {
            _audioManager.PlayOnceButShutAll(AudioNames.WinSound);
            _winLevelWindow.SetActive(true);
            _loseLevelWindow.SetActive(false);
            gameObject.SetActive(true);
            _statisticsViewer.ShowWinStatistics();
        }
    
        public void ShowLoseLevelWindow()
        {
            _audioManager.PlayOnceButShutAll(AudioNames.LoseSound);
            _winLevelWindow.SetActive(false);
            _loseLevelWindow.SetActive(true);
            gameObject.SetActive(true);
            _statisticsViewer.ShowLoseStatistics();
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