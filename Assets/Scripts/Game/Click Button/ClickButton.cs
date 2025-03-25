using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Click_Button
{
    public class ClickButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        public void Inizialize(Sprite defaultSprite, ColorBlock colorBlock)
        {
            _image.sprite = defaultSprite;
            _button.colors = colorBlock;
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
}
