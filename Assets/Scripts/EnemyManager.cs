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

    public void Initialize(HealthBar healthBar)
    {
        _healthBar = healthBar;
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        _currentEnemyData = _enemiesConfig.Enemies[0];
        _currentEnemy = Instantiate(_enemiesConfig.EnemyPrefab, _enemyContainer);
        _currentEnemy.Initialize(_currentEnemyData);
        
        _healthBar.SetMaxValue(_currentEnemyData.Health);
        _currentEnemy.OnDamage += _healthBar.DecreaseValue;
    }
    
    public void DamageCurrentEnemy(float damage)
    {
        _currentEnemy.TakeDamage(damage);
        Debug.Log("Damaged. Current health is " + _currentEnemy.GetHealth());
    }

    public void SubscribeOnCurrentEnemyDamage(UnityAction<float> callback)
    {
        if (_currentEnemy != null)
        {
            _currentEnemy.OnDamage += callback;
        }
    }
}