using DG.Tweening;
using Game.RSPConfig;
using Game.Statistics;
using Global;
using Global.AudioSystem;
using Global.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Enemies
{
   public class Enemy : MonoBehaviour
   {
      [SerializeField] private Image image;
      [SerializeField] private Image _image;
   
      private float _health;
      public event UnityAction<float> OnDamage; //ключевое слово ивент защищает от того, чтобы мы изменили это поле извне (из других классов)
      public event UnityAction OnDeath; //мы можем толькоо отписаться или подписаться
   
      private Color _originalColor;
      private Color _damageColor = Color.red;

      private string _enemyId;
      
      private StatisticsManager _statisticsManager;
      private AudioManager _audioManager;

      public void Initialize(Sprite sprite, float health, TechniqueType techniqueType, string currentEnemyId, StatisticsManager statisticsManager, SaveSystem saveSystem, AudioManager audioManager)
      {
         _enemyId = currentEnemyId;
         image.sprite =sprite;
         _health = health;
         _originalColor = _image.color;
         _audioManager = audioManager;
         DataKeeper.EnemyId = currentEnemyId;
         _statisticsManager = statisticsManager;
         _statisticsManager.Initialize(saveSystem);
         _statisticsManager.UpdateEnemyStats(0,0,1);
         
         transform.DOScale(Vector3.one, 1f).From(Vector3.zero);
      }

      public void TakeDamage(float damage)
      {
         if (damage >= _health)
         {
            _health = 0;
            OnDamage?.Invoke(damage);
            _statisticsManager.UpdateEnemyStats(1,1,0);
            OnDeath?.Invoke();
            return;
         }
         _health -= damage;

         _audioManager.PlayClip(AudioNames.DamageSound);
         AnimateDamage();
      
         _statisticsManager.UpdateEnemyStats(1,0,0);
         OnDamage?.Invoke(damage);
      }

      public float GetHealth()
      {
         return _health;
      }

      private void AnimateDamage()
      {
         var initialPosition = transform.localPosition;
         transform.DOShakePosition(0.3f, 5f, 10, 90f).SetRelative(true).OnComplete(() =>
         {
            transform.localPosition = initialPosition;
         });

         _image.DOColor(_damageColor, 0.1f)
            .OnComplete(() =>
            {
               _image.DOColor(_originalColor, 0.1f);
            });
      }
   }
}
