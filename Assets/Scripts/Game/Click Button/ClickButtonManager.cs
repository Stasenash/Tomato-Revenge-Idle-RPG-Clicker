using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ClickButtonManager : MonoBehaviour
{
    [SerializeField] private ClickButton _clickButton;
    [SerializeField] private ClickButtonConfig _buttonConfig;

    
    public event UnityAction OnClicked;
    public void Inizialize()
    {
        _clickButton.Inizialize(_buttonConfig.DefaultSprite, _buttonConfig.ButtonColors);
        
        _clickButton.SubscribeOnClick(() => OnClicked?.Invoke());
        _clickButton.SubscribeOnClick(AnimateClick);
    }

    private void AnimateClick()
    {    
        gameObject.transform.DORotate(new Vector3(0, 0, -360), 0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear); // Линейное вращение без ускорения/замедления
    }
}