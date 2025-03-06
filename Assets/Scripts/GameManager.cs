using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ClickButtonManager _clickButtonManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Timer _timer;
    [SerializeField] private EndLevelWindow _endLevelWindow;
    
    private void Awake()
    {
        _clickButtonManager.Inizialize();
        _enemyManager.Initialize(_healthBar);
        _endLevelWindow.Initialize();  
        
        _clickButtonManager.OnClicked += () => _enemyManager.DamageCurrentEnemy(1f);
        _endLevelWindow.OnRestartButtonClicked += StartLevel; //подписка на кнопку рестарта
        _enemyManager.OnLevelPassed += () =>
        {
            _endLevelWindow.ShowWinLevelWindow();
            _timer.Stop();
        };
        

        
        StartLevel();
    }

    private void StartLevel()
    {
        _timer.Initialize(10f);
        _enemyManager.SpawnEnemy();
        _timer.OnTimerEnd += _endLevelWindow.ShowLoseLevelWindow;
    }
}
