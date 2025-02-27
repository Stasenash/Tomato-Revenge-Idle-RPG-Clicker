using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickButtonController : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;

    public void Inizialize() //
    {
        //TODO: инициализация палитры кнопки
        //TODO: визуальные изменения кнопки при клике
        return;
    }
    public void SubscribeOnClick(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }

    public void UnsubscribeOnClick(UnityAction action)
    {
        _button.onClick.RemoveListener(action);
    }
    
    //не используем гет компонент, потому что он пробегает по всем элементам и это долго
}
