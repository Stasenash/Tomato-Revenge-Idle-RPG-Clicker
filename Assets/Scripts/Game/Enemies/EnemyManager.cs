using Game.Configs;
using Game.Configs.Enemies_Configs;
using Game.Configs.LevelConfigs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private EnemiesConfig _enemiesConfig;
        
        private Enemy _currentEnemyMonoBehavior;
        private HealthBar.HealthBar _healthBar;
        private Timer.Timer _timer;
        private LevelData _levelData;
        private int _currentEnemyIndex;
        private TechniqueType _currentEnemyTechniqueType;

        public event UnityAction<bool> OnLevelPassed;
    
        public void Initialize(HealthBar.HealthBar healthBar, Timer.Timer timer)
        {
            _timer = timer;
            _healthBar = healthBar;
        }
        
        public void StartLevel(LevelData levelData)
        {
            _levelData = levelData;
            _currentEnemyIndex = -1;
            
            if (_currentEnemyMonoBehavior == null)
            {
                _currentEnemyMonoBehavior = Instantiate(_enemiesConfig.EnemyPrefab, _enemyContainer);
                _currentEnemyMonoBehavior.OnDeath += SpawnEnemy;
                _currentEnemyMonoBehavior.OnDamage += _healthBar.DecreaseValue;
            }

            SetBackground();
            SpawnEnemy();
        }

        private void SetBackground()
        {
            //TODO: узнать как лучше
            var background = GameObject.FindGameObjectWithTag(TAGS.LEVEL_BACKGROUND);
            background.gameObject.GetComponent<Image>().sprite = _levelData.Background;
        }

        private void SpawnEnemy()
        {
            _timer.Stop();
            _timer.SetActive(false);
            _currentEnemyIndex++;

            if (_currentEnemyIndex >= _levelData.Enemies.Count)
            {
                OnLevelPassed?.Invoke(true);
                _timer.Stop();
                return;
            }
            
            var currentEnemy = _levelData.Enemies[_currentEnemyIndex];
            _currentEnemyTechniqueType = currentEnemy.TechniqueType;

            if (currentEnemy.IsBoss)
            {
                _timer.SetActive(true);
                _timer.Initialize(currentEnemy.BossTime);
                _timer.OnTimerEnd += () => OnLevelPassed?.Invoke(false);
            }
            
            var _currentEnemyData = _enemiesConfig.GetEnemy(currentEnemy.Id); 
            
            InitHeathBar(currentEnemy.Hp);
            
        
            _currentEnemyMonoBehavior.Initialize(_currentEnemyData.Sprite, currentEnemy.Hp);
        
        }

        private void InitHeathBar(float health)
        {
            _healthBar.Show();
            _healthBar.SetMaxValue(health);
        }
    
        public void DamageCurrentEnemy(float damage)
        {
            _currentEnemyMonoBehavior.TakeDamage(damage);
            //Debug.Log("Damaged. Current health is " + _currentEnemy.GetHealth());
        }

        public void SubscribeOnCurrentEnemyDamage(UnityAction<float> callback)
        {
            if (_currentEnemyMonoBehavior != null)
            {
                _currentEnemyMonoBehavior.OnDamage += callback;
            }
        }


        public TechniqueType GetCurrentEnemyTechniqueType() => _currentEnemyTechniqueType;
    }
}