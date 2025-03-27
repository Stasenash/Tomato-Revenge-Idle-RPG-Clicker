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
        
        public void Initialize(int levelNum, ProgressState progressState, UnityAction clickCallback)
        {
            _text.text = $"{levelNum + 1}";

            _image.color = progressState switch
            {
                ProgressState.Current => _currentLevel,
                ProgressState.Closed => _closedLevel,
                ProgressState.Passed => _passedLevel,
                _ => _image.color
            };
            
            _button.onClick.AddListener(() => clickCallback.Invoke());
        }
    }
}