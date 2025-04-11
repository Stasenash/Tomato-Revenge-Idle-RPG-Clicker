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
                                string description, 
                                int cost,
                                bool isEnoughMoney,
                                bool isMaxLevel)
        {
            _button.onClick.AddListener(()=>onClick.Invoke(SkillId));
           // _label.text = label;
            _skillImage = skillImage;
            _description.text = description;
            
            if (isMaxLevel)
            {
                _cost.gameObject.SetActive(false);
                _button.interactable = false;
                return;
            }
            
            _cost.text = cost.ToString();
            _cost.color = isEnoughMoney ? Color.black : Color.red;
            _button.interactable = isEnoughMoney;

            
        }
    }
}