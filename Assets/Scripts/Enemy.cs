using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
   [SerializeField] private Image image;
   [SerializeField] private Animator _enemyAnimator;

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
      AnimateDamage();
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
   }
}
