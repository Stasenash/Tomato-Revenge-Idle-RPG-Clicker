using DG.Tweening;
using Game.Configs;
using Game.Configs.Enemies_Configs;
using Game.Configs.LevelConfigs;
using Game.RSPConfig;
using Game.Statistics;
using Global;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using EnemyData = Game.Enemies.EnemyData;

namespace Game.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private EnemiesConfig _enemiesConfig;
        [SerializeField] private StatisticsManager _statisticsManager;
        [SerializeField] private TextMeshProUGUI _damageText;
        
        private Enemy _currentEnemyMonoBehavior;
        private HealthBar.HealthBar _healthBar;
        private Timer.Timer _timer;
        private LevelData _levelData;
        private int _currentEnemyIndex;
        private TechniqueType _currentEnemyTechniqueType;
        private Image _timerImage;
        private SaveSystem _saveSystem;

        public event UnityAction<bool> OnLevelPassed;
    
        public void Initialize(HealthBar.HealthBar healthBar, Timer.Timer timer, Image timerImage, SaveSystem saveSystem)
        {
            DataKeeper.Reward = 0;
            _timer = timer;
            _timerImage = timerImage;
            _healthBar = healthBar;
            _saveSystem = saveSystem;
            
            var color = _damageText.color;
            color.a = 0f;
            _damageText.color = color;
        }
        
        public void StartLevel(LevelData levelData)
        {
            _levelData = levelData;
            _currentEnemyIndex = -1;
            
            if (_currentEnemyMonoBehavior == null)
            {
                _currentEnemyMonoBehavior = Instantiate(_enemiesConfig.EnemyPrefab, _enemyContainer);
                _currentEnemyMonoBehavior.OnDamage += _healthBar.DecreaseValue;
                _currentEnemyMonoBehavior.OnDeath += SpawnEnemy;
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
            _timerImage.gameObject.SetActive(false);
            _currentEnemyIndex++;

            if (_currentEnemyIndex >= _levelData.Enemies.Count)
            {
                CancelInvoke("PassiveDamage");
                OnLevelPassed?.Invoke(true);
                _timer.Stop();
                return;
            }
            
            var currentEnemy = _levelData.Enemies[_currentEnemyIndex];
            _currentEnemyTechniqueType = currentEnemy.TechniqueType;

            if (currentEnemy.IsBoss)
            {
                _timer.SetActive(true);
                _timerImage.gameObject.SetActive(true);
                _timer.Initialize(currentEnemy.BossTime);
                _timer.OnTimerEnd += () => OnLevelPassed?.Invoke(false);
            }
            var _currentEnemyData = _enemiesConfig.GetEnemy(currentEnemy.Id); 
            InitHeathBar(currentEnemy.Hp, _currentEnemyTechniqueType);
            DataKeeper.Reward = _levelData.Reward;
            _currentEnemyMonoBehavior.Initialize(_currentEnemyData.Sprite, currentEnemy.Hp, currentEnemy.TechniqueType, currentEnemy.Id,_statisticsManager, _saveSystem);
            InvokeRepeating("PassiveDamage", 1f, 1f);
        }

        private void InitHeathBar(float health, TechniqueType currentTechniqueType)
        {
            _healthBar.Hide();
            _healthBar.SetMaxValue(health);
            _healthBar.SetSpriteForTechnique(currentTechniqueType);
            _healthBar.Show();
        }
    
        public void DamageCurrentEnemy(float damage)
        {
            AnimateDamageText(damage);
            _currentEnemyMonoBehavior.TakeDamage(damage);
            
            //Debug.Log("Damaged. Current health is " + _currentEnemy.GetHealth());
        }

        private void PassiveDamage()
        {
            var passiveDamage = ((Stats)_saveSystem.GetData(SavableObjectType.Stats)).PassiveDamage;
            if (passiveDamage > 0)
            {
                AnimateDamageText(passiveDamage);
                _currentEnemyMonoBehavior.TakeDamage(passiveDamage);
            }
        }

        public void SubscribeOnCurrentEnemyDamage(UnityAction<float> callback)
        {
            if (_currentEnemyMonoBehavior != null)
            {
                _currentEnemyMonoBehavior.OnDamage += callback;
            }
        }


        private void AnimateDamageText(float damage)
        {
            var position = new Vector3(_damageText.transform.position.x, _damageText.transform.position.y, _damageText.transform.position.z);
            var endPosition = new Vector3(_damageText.transform.position.x, _damageText.transform.position.y + 850, _damageText.transform.position.z);
            
            var text = Instantiate(_damageText, _enemyContainer);
            
            text.transform.position = position;
            text.text = damage.ToString();
            //text.fontSize = 80;
            
            var color = text.color;
            color.a = 1f;
            text.color = color;
            
            //text.gameObject.SetActive(true);
            
            text.transform.DOMove(endPosition, 0.5f).SetEase(Ease.OutQuad).OnComplete(()=>Destroy(text));
        }

        public TechniqueType GetCurrentEnemyTechniqueType() => _currentEnemyTechniqueType;
    }
}