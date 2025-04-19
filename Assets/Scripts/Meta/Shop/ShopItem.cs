using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Meta.Shop
{
    public class ShopItem : MonoBehaviour
    {
        //[SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private Sprite _skillImage;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _cost;
        [SerializeField] private Button _button;

        [FormerlySerializedAs("_skillName")] [SerializeField] public string SkillId;
        
        public void Initialize(UnityAction<string> onClick, 
                                Sprite skillImage, 
                                int cost,
                                bool isEnoughMoney,
                                bool isMaxLevel, 
                                ShopWindow shopWindow)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(()=>onClick.Invoke(SkillId));
            
            _skillImage = skillImage;
            
            if (isMaxLevel)
            {
                _cost.gameObject.SetActive(false);
                _button.interactable = false;
                return;
            }

            if (!isEnoughMoney)
            {
                _button.onClick.RemoveAllListeners();
                _button.onClick.AddListener(shopWindow.ShowCoinsTab);
            }

            _cost.text = cost.ToString();
            _cost.color = isEnoughMoney ? Color.white : Color.red;
            //_button.interactable = isEnoughMoney;
        }
    }
}