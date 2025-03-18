using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
   [SerializeField] private Image image;
   [SerializeField] private Animator _enemyAnimator;
   [SerializeField] private AudioSource _audioSource; //TODO аудио потом занести в конфиг

   private float _health;
   public event UnityAction<float> OnDamage; //ключевое слово ивент защищает от того, чтобы мы изменили это поле извне (из других классов)
   public event UnityAction OnDeath; //мы можем толькоо отписаться или подписаться
   
   public StatisticsManager statisticsManager;
   
   public void Initialize(EnemyData enemyData)
   {
      image.sprite = enemyData.Sprite;
      _health = enemyData.Health;
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
      
      AnimateDamage(); //потом как-то это отдельно сделать анимации и звуки
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
      _enemyAnimator.SetTrigger("Damage");
      //Dotween (
      // DOTween.Sequence().Append(); линку для вереницы событий (отдельный метод для запуска)
      // gameObject.transform.DOMove(0.5f);

   }
}
