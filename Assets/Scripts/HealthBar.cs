using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;

    public void SetMaxValue(float value)
    {
        _healthSlider.maxValue = value;
        _healthSlider.value = value;
    }

    public void DecreaseValue(float value)
    {
        _healthSlider.value -= value;
    }
    
    
}
