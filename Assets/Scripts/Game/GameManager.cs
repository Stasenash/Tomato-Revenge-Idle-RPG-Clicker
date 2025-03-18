using System;
using SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : EntryPoint
{
    [SerializeField] private ClickButtonManager _clickButtonManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Timer _timer;
    [SerializeField] private EndLevelWindow _endLevelWindow;
    
    public const string SCENE_LOADER_TAG = "SceneLoader";

    private void StartLevel()
    {
        _timer.Initialize(10f);
        _enemyManager.SpawnEnemy();
        _timer.OnTimerEnd += _endLevelWindow.ShowLoseLevelWindow;
    }

    public override void Run(SceneEnterParams enterParams)
    {
        _clickButtonManager.Inizialize();
        _enemyManager.Initialize(_healthBar);
        _endLevelWindow.Initialize();  
        
        _clickButtonManager.OnClicked += () => _enemyManager.DamageCurrentEnemy(1f);
        _endLevelWindow.OnRestartButtonClicked += RestartLevel; //подписка на кнопку рестарта
        _enemyManager.OnLevelPassed += () =>
        {
            _endLevelWindow.ShowWinLevelWindow();
            _timer.Stop();
        };

        StartLevel();
    }

    public void RestartLevel()
    {
        var sceneLoader = GameObject.FindWithTag(SCENE_LOADER_TAG).GetComponent<SceneLoader>();
        sceneLoader.LoadGameplayScene();
    }
}
