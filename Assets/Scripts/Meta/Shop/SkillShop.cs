using System.Collections.Generic;
using Game.Configs.SkillsConfigs;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.Shop
{
    public class SkillShop : MonoBehaviour
    {
        [SerializeField] private Button _ninjutsuTabButton;
        [SerializeField] private Button _taijutsuTabButton;
        [SerializeField] private Button _genjutsuTabButton;
        
        [SerializeField] private List<ShopItem> _shopItems;
        private Dictionary<string, ShopItem> _itemsMap;
        
        private Wallet _wallet;
        private OpenedSkills _openedSkills;
        private SkillsConfig _skillsConfig;
        private SaveSystem _saveSystem;
        

        public void Initialize(SaveSystem saveSystem, SkillsConfig skillsConfig)
        {
            _saveSystem = saveSystem;
            _wallet = (Wallet)saveSystem.GetData(SavableObjectType.Wallet);
            _openedSkills = (OpenedSkills)saveSystem.GetData(SavableObjectType.OpenedSkills);
            _skillsConfig = skillsConfig;
            InitializeItemMap();
            ShowShopItems();
        }
        
        private void ShowShopItems()
        {
            foreach (var skillData in _skillsConfig.Skills)
            {
                var skillWithLevel = _openedSkills.GetOrCreateSkillWithLevel(skillData.Id);
                var skillDataByLevel = skillData.GetSkillDataByLevel(skillWithLevel.Level);
                
                if (!_itemsMap.ContainsKey(skillData.Id)) continue; 
                _itemsMap[skillData.Id].Initialize((skillId)=>SkillUpgrade(skillId, skillDataByLevel.Cost), 
                    skillData.Sprite, "", skillDataByLevel.Cost, 
                    _wallet.Coins >= skillDataByLevel.Cost, 
                    skillData.isMaxLevel(skillWithLevel.Level));
            }
        }

        private void InitializeItemMap()
        {
            _itemsMap = new();

            foreach (var shopItem in _shopItems)
            {
                _itemsMap[shopItem.SkillId] = shopItem;
            }
        }

        private void SkillUpgrade(string skillId, int cost)
        {
            var skillWithLevel = _openedSkills.GetOrCreateSkillWithLevel(skillId);
            skillWithLevel.Level++;
            _wallet.Coins -= cost;
           
            _saveSystem.SaveData(SavableObjectType.Wallet);
            _saveSystem.SaveData(SavableObjectType.OpenedSkills);
            ShowShopItems();
        }
    }
}