using System;
using Game.Enemies;
using Game.RSPConfig;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.HealthBar
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Image _techniqueImage;
      
        [SerializeField] private Sprite ninjutsuSprite;
        [SerializeField] private Sprite genjutsuSprite;
        [SerializeField] private Sprite taijutsuSprite;
        
        [SerializeField] private TextMeshProUGUI _healthText;

        public void SetMaxValue(float value)
        {
            _healthSlider.maxValue = value;
            _healthSlider.value = value;
            _healthText.text = _healthSlider.value.ToString();
        }
        
        public void SetSpriteForTechnique(TechniqueType techniqueType)
        {
            switch (techniqueType)
            {
                case TechniqueType.Genjutsu: _techniqueImage.sprite = genjutsuSprite; break;
                case TechniqueType.Ninjutsu: _techniqueImage.sprite = ninjutsuSprite; break;
                case TechniqueType.Taijutsu: _techniqueImage.sprite = taijutsuSprite; break;
            }
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void DecreaseValue(float value)
        {
            _healthSlider.value -= value;
            _healthText.text = (Math.Ceiling(_healthSlider.value) + 1).ToString();
        }
    }
}
