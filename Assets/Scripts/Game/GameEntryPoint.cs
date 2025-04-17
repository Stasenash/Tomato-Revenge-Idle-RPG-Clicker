using Game.Click_Button;
using Game.Configs;
using Game.Configs.Enemies_Configs;
using Game.Configs.HeroConfigs;
using Game.Configs.LevelConfigs;
using Game.Configs.SkillsConfigs;
using Game.Enemies;
using Game.Skills;
using Global;
using Global.AudioSystem;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class GameEntryPoint : EntryPoint
    {
        [SerializeField] private ClickButtonManager _clickButtonManager;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private HealthBar.HealthBar _healthBar;
        [SerializeField] private Timer.Timer _timer;
        [SerializeField] private Image _timerImage;
        [SerializeField] private EndLevelWindow.EndLevelWindow _endLevelWindow;
        [SerializeField] private LevelsConfig _levelsConfig;
        [SerializeField] private GamePanelManager _gamePanelManager;
        [SerializeField] private SkillsConfig _skillsConfig;
        [SerializeField] private HeroStatsConfig _heroStatsConfig;
        [SerializeField] private RSPConfig.RSPConfig _rspConfig;

        [SerializeField] private TextMeshProUGUI _levelText;
        
        private GameEnterParams _gameEnterParams;
        private SaveSystem _saveSystem;
        private SkillSystem _skillSystem;
        private EndLevelSystem _endLevelSystem;
        private AudioManager _audioManager;
        private SceneLoader _sceneLoader;

        private void StartLevel()
        {
            var _maxLocationAndLevel = _levelsConfig.GetMaxLocationAndLevel();
            
            var locationTitle = GetLevelAndLocationTitle();
            _levelText.text = locationTitle;
            
            var location = _gameEnterParams.Location;
            var level = _gameEnterParams.Level;

            if (location > _maxLocationAndLevel.x ||
                (location == _maxLocationAndLevel.x && level > _maxLocationAndLevel.y))
            {
                location = _maxLocationAndLevel.x;
                level = _maxLocationAndLevel.y;
            }
            
            var levelData = _levelsConfig.GetLevel(location, level);
            
            _isLevelHasABoss(levelData);
            
            _enemyManager.StartLevel(levelData);
        }

        private void _isLevelHasABoss(LevelData levelData)
        {
            DataKeeper.IsBoss = false;
            foreach (var enemy in levelData.Enemies)
            {
                if (enemy.IsBoss)
                    DataKeeper.IsBoss = true;
            }
        }

        private string GetLevelAndLocationTitle()
        {
            return _levelsConfig.GetLocationName(_gameEnterParams.Location) + " - уровень: " + (_gameEnterParams.Level + 1);
        }
        public void StartNextLevel()
        {
            var locationTitle = GetLevelAndLocationTitle();
            _levelText.text = locationTitle;
            GameEnterParams gameParams = _gameEnterParams;
            if (_gameEnterParams.Level >= _levelsConfig.GetMaxLevelOnLocation(_gameEnterParams.Location))
            {
                if (_gameEnterParams.Location >= _levelsConfig.GetMaxLocationNum())
                {
                    Debug.Log("Game passed");
                    //todo: возврат на карту
                }
                else
                {
                    gameParams = new GameEnterParams(_gameEnterParams.Location + 1, 0);
                }
            }
            else
            {
                gameParams = new GameEnterParams(_gameEnterParams.Location, _gameEnterParams.Level + 1);
            } 
            _sceneLoader.LoadGameplayScene(gameParams);
            
            
        }

        public override void Run(SceneEnterParams enterParams)
        {
            var commonObject = GameObject.FindWithTag(TAGS.COMMON_OBJECT_TAG).GetComponent<CommonObject>();
            _saveSystem = commonObject.SaveSystem;
            _audioManager = commonObject.AudioManager;
            _sceneLoader = commonObject.SceneLoader;
            
            if (enterParams is not GameEnterParams gameEnterParams)
            {
                Debug.LogError("trouble with Game Enter params");
                return;
            }
            _gameEnterParams = gameEnterParams;
            _enemyManager.Initialize(_healthBar, _timer, _timerImage);
            _endLevelWindow.Initialize();  
            _gamePanelManager.Initialize();
            
            var openedSkills = (OpenedSkills)_saveSystem.GetData(SavableObjectType.OpenedSkills);
            _skillSystem = new SkillSystem(_enemyManager, _saveSystem, _heroStatsConfig, _rspConfig);
            _clickButtonManager.Inizialize(_skillSystem);
            
            
            _endLevelSystem =
                new EndLevelSystem(_endLevelWindow, _saveSystem, _gameEnterParams, _levelsConfig);
            _enemyManager.OnLevelPassed += _endLevelSystem.LevelPassed;
            
            _endLevelWindow.OnRestartButtonClicked += RestartLevel; //подписка на кнопку рестарта
            
            _endLevelWindow.OnNextButtonClicked += StartNextLevel;
            _endLevelSystem.OnStartNextLevel += StartNextLevel;
            
            _endLevelWindow.OnBackButtonClicked += ReturnToMap;
            _gamePanelManager.OnLoseButtonClicked += ReturnToMap;

            StartLevel();
        }

        private void ReturnToMap()
        {
            _sceneLoader.LoadMetaScene();
        }

        public void RestartLevel()
        {
            _sceneLoader.LoadGameplayScene(_gameEnterParams);
        }
    }
}
