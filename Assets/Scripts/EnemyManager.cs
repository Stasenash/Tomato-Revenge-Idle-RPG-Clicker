using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private EnemiesConfig _enemiesConfig;
    
    private EnemyData _currentEnemyData;
    private Enemy _currentEnemy;

    public void Initialize()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        _currentEnemyData = _enemiesConfig.Enemies[0];
        _currentEnemy = Instantiate(_enemiesConfig.EnemyPrefab, _enemyContainer);
        _currentEnemy.Initialize(_currentEnemyData);
    }
    
    public void DamageCurrentEnemy(float damage)
    {
        _currentEnemy.TakeDamage(damage);
        Debug.Log("Damaged. Current health is " + _currentEnemy.GetHealth());
    }
}