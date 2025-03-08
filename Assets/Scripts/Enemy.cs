using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
   [SerializeField] private Image image;
   [SerializeField] private Animator _enemyAnimator;

   private float _health;
   public event UnityAction<float> OnDamage; //ключевое слово ивент защищает от того, чтобы мы изменили это поле извне (из других классов)
   public event UnityAction OnDeath; //мы можем толькоо отписаться или подписаться
   
   public void Initialize(EnemyData enemyData)
   {
      image.sprite = enemyData.Sprite;
      _health = enemyData.Health;
   }

   public void TakeDamage(float damage)
   {
      if (damage >= _health)
      {
         _health = 0;
         
         OnDamage?.Invoke(damage);
         OnDeath?.Invoke();
      }
      _health -= damage;
      AnimateDamage();
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
