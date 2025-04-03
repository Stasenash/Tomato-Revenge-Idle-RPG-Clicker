using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePanelManager : MonoBehaviour
{
    [SerializeField] private Button _loseButton;

    public event UnityAction OnLoseButtonClicked;
    public void Initialize()
    {
        _loseButton.onClick.AddListener(() => OnLoseButtonClicked?.Invoke());
    }
        
}