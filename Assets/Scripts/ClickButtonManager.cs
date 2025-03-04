using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class ClickButtonManager : MonoBehaviour
    {
        [SerializeField] private ClickButton _clickButton;
        [SerializeField] private ClickButtonConfig _buttonConfig;
        [SerializeField] private Animator _shurikenAnimator;

        
        public event UnityAction OnClicked;
        public void Inizialize()
        {
            _clickButton.Inizialize(_buttonConfig.DefaultSprite, _buttonConfig.ButtonColors);
            _clickButton.SubscribeOnClick(() => OnClicked?.Invoke());
            
            
        }
        
        private void ShowClick()
        {
            Debug.Log("clicked!");
        }
    
        private void AnimateClick()
        {
            _shurikenAnimator.SetTrigger("ShurikenAnimation");
        }
    }
}