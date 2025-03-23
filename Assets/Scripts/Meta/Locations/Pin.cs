using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

namespace Meta.Locations
{
    public class Pin : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text; //если нам нужен номер уровня

        [SerializeField] private Color _passedLevel;
        [SerializeField] private Color _closedLevel;
        [SerializeField] private Color _currentLevel;
        
        public void Initialize(int levelNum, PinType pinType, UnityAction clickCallback)
        {
            _text.text = $"{levelNum + 1}";

            _image.color = pinType switch
            {
                PinType.Current => _currentLevel,
                PinType.Closed => _closedLevel,
                PinType.Passed => _passedLevel,
                _ => _image.color
            };
            
            _button.onClick.AddListener(() => clickCallback.Invoke());
        }
    }
}