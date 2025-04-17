using DG.Tweening;
using Game.Statistics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Enemies
{
   public class Enemy : MonoBehaviour
   {
      [SerializeField] private Image image;
      [SerializeField] private AudioSource _audioSource; //TODO аудио потом занести в конфиг
      [SerializeField] private Image _image;
   
      private float _health;
      public event UnityAction<float> OnDamage; //ключевое слово ивент защищает от того, чтобы мы изменили это поле извне (из других классов)
      public event UnityAction OnDeath; //мы можем толькоо отписаться или подписаться
   
      private Color _originalColor;
      private Color _damageColor = Color.red;
   
      public StatisticsManager statisticsManager;
   
      public void Initialize(Sprite sprite, float health, TechniqueType techniqueType)
      {
         image.sprite =sprite;
         _health = health;
         _originalColor = _image.color;
         statisticsManager.Initialize();
         statisticsManager.UpdateEnemyStats(0, 0, 0, 1);
      }

      public void TakeDamage(float damage)
      {
         if (damage >= _health)
         {
            _health = 0;
            statisticsManager.UpdateEnemyStats(0, 1, 1, 0);
            OnDamage?.Invoke(damage);
            OnDeath?.Invoke();
         }
         _health -= damage;
      
         AnimateDamage(); //потом как-то это отдельно сделать звуки
         _audioSource.Play();
      
         statisticsManager.UpdateEnemyStats(0, 1, 0, 0);
         OnDamage?.Invoke(damage);
      }

      public float GetHealth()
      {
         return _health;
      }

      private void AnimateDamage()
      {
         transform.DOShakePosition(0.3f, 5f, 10, 90f);
      
         _image.DOColor(_damageColor, 0.1f)
            .OnComplete(() =>
            {
               _image.DOColor(_originalColor, 0.1f);
            });
      }
   }
}
