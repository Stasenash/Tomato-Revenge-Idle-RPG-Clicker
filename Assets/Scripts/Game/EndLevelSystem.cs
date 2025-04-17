using Game.Configs;
using Game.Configs.LevelConfigs;
using Global;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using SceneManagement;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class EndLevelSystem
    {
        private readonly EndLevelWindow.EndLevelWindow _endLevelWindow;
        private readonly SaveSystem _saveSystem;
        private readonly GameEnterParams _gameEnterParams;
        private readonly LevelsConfig _levelsConfig;
    
        public event UnityAction OnStartNextLevel;

        public EndLevelSystem(EndLevelWindow.EndLevelWindow endLevelWindow, SaveSystem saveSystem,
            GameEnterParams gameEnterParams, LevelsConfig levelsConfig)
        {
            _levelsConfig = levelsConfig;
            _endLevelWindow = endLevelWindow;
            _saveSystem = saveSystem;
            _gameEnterParams = gameEnterParams;
        }
        
        public void LevelPassed(bool isPassed)
        {
            if (isPassed)
            {
                TrySaveProgress();
                if (DataKeeper.IsBoss)
                {
                    _endLevelWindow.ShowWinLevelWindow();
                }
                else
                {  
                    OnStartNextLevel?.Invoke();
                }
            }
            else if(DataKeeper.IsBoss)
            {
                _endLevelWindow.ShowLoseLevelWindow();
            }
        }
        
        private void TrySaveProgress()
        {
            var wallet = (Wallet)_saveSystem.GetData(SavableObjectType.Wallet);
            var coins = _levelsConfig.GetReward(_gameEnterParams.Location, _gameEnterParams.Level);
            wallet.Coins += coins;
            Debug.Log($"coins={wallet.Coins}");
            
            var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
            if (_gameEnterParams.Location != progress.CurrentLocation
                || _gameEnterParams.Level != progress.CurrentLevel)
                return;
            var maxLocationAndLevel = _levelsConfig.GetMaxLocationAndLevel();
            var maxLevel = _levelsConfig.GetMaxLevelOnLocation(_gameEnterParams.Location);
            
            // if (progress.CurrentLocation > maxLocationAndLevel.x || 
            //     (progress.CurrentLocation == maxLocationAndLevel.x 
            //      && progress.CurrentLevel > maxLocationAndLevel.y)) return;
            if (progress.CurrentLevel >= maxLevel)
            {
                progress.CurrentLevel = 0;
                progress.CurrentLocation++;
            }
            else
            {
                progress.CurrentLevel++;
            }
            
            _saveSystem.SaveData(SavableObjectType.Progress);
            _saveSystem.SaveData(SavableObjectType.Wallet);
        }
    }
}