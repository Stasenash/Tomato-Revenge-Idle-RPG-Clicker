using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private EnemiesConfig _enemiesConfig;
    
    private EnemyData _currentEnemyData;
    private Enemy _currentEnemy;
    private HealthBar _healthBar;

    public event UnityAction OnLevelPassed;
    
    public void Initialize(HealthBar healthBar)
    {
        _healthBar = healthBar;
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        _currentEnemyData = _enemiesConfig.Enemies[0]; //если что, тут пока просто зашит айдишник
        InitHeathBar();
        if (_currentEnemy == null)
        {
            _currentEnemy = Instantiate(_enemiesConfig.EnemyPrefab, _enemyContainer);
            _currentEnemy.OnDeath += () => OnLevelPassed?.Invoke();
            _currentEnemy.OnDamage += _healthBar.DecreaseValue;
            _currentEnemy.OnDeath += _healthBar.Hide;
        }
        
        _currentEnemy.Initialize(_currentEnemyData);
        
    }

    private void InitHeathBar()
    {
        _healthBar.Show();
        _healthBar.SetMaxValue(_currentEnemyData.Health);
    }
    
    public void DamageCurrentEnemy(float damage)
    {
        _currentEnemy.TakeDamage(damage);
        //Debug.Log("Damaged. Current health is " + _currentEnemy.GetHealth());
    }

    public void SubscribeOnCurrentEnemyDamage(UnityAction<float> callback)
    {
        if (_currentEnemy != null)
        {
            _currentEnemy.OnDamage += callback;
        }
    }
}