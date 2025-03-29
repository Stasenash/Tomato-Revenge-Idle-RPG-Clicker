using Game.Click_Button;
using Game.Configs;
using Game.Configs.LevelConfigs;
using Game.Enemies;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using SceneManagement;
using UnityEngine;

namespace Game
{
    public class GameManager : EntryPoint
    {
        [SerializeField] private ClickButtonManager _clickButtonManager;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private HealthBar.HealthBar _healthBar;
        [SerializeField] private Timer.Timer _timer;
        [SerializeField] private EndLevelWindow.EndLevelWindow _endLevelWindow;
        [SerializeField] private LevelsConfig _levelsConfig;

        private GameEnterParams _gameEnterParams;
        private SaveSystem _saveSystem;
        
        private void StartLevel()
        {
            var levelData = _levelsConfig.GetLevel(_gameEnterParams.Location, _gameEnterParams.Level);
            _enemyManager.StartLevel(levelData);
        }

        public override void Run(SceneEnterParams enterParams)
        {
            _saveSystem = FindFirstObjectByType<SaveSystem>();
            if (enterParams is not GameEnterParams gameEnterParams)
            {
                Debug.LogError("trouble with Game Enter params");
                return;
            }
            _gameEnterParams = gameEnterParams;
            
            _clickButtonManager.Inizialize();
            _enemyManager.Initialize(_healthBar, _timer);
            _endLevelWindow.Initialize();  
        
            _clickButtonManager.OnClicked += () => _enemyManager.DamageCurrentEnemy(1f);
            _endLevelWindow.OnRestartButtonClicked += RestartLevel; //подписка на кнопку рестарта
            _enemyManager.OnLevelPassed += LevelPassed;

            StartLevel();
        }

        public void LevelPassed(bool isPassed)
        {
            if (isPassed)
            {
                TrySaveProgress();
                _endLevelWindow.ShowWinLevelWindow();
            }
            else
            {
                _endLevelWindow.ShowLoseLevelWindow();
            }
        }

        private void TrySaveProgress()
        {
            var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
            if (_gameEnterParams.Location != progress.CurrentLocation
                || _gameEnterParams.Level != progress.CurrentLevel)
                return;
            var maxLevel = _levelsConfig.GetMaxLevelOnLocation(progress.CurrentLevel);

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
        }

        public void RestartLevel()
        {
            var sceneLoader = GameObject.FindWithTag(TAGS.SCENE_LOADER_TAG).GetComponent<SceneLoader>();
            sceneLoader.LoadGameplayScene(_gameEnterParams);
        }
    }
}
