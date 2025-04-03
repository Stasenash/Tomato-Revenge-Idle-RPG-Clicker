using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class DotweenHelper : MonoBehaviour
{
    public void RemoveAllTweens()
    {
        // Отменяем все активные твины
        DOTween.KillAll();

        // Очищаем все твины
        DOTween.Clear();

        // Удаляем компонент DOTween, если он есть на GameObject
        var dotweenComponent = GameObject.FindObjectOfType<DOTweenComponent>();
        if (dotweenComponent != null)
        {
            Destroy(dotweenComponent);
        }
    }
}