using System.Collections.Generic;
using Game.Configs.SkillsConfigs;
using Global.AudioSystem;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta.Shop
{
    public class SkillShop : MonoBehaviour
    {
        [SerializeField] private List<ShopItem> _shopItems;
        private Dictionary<string, ShopItem> _itemsMap;
        
        private Wallet _wallet;
        private OpenedSkills _openedSkills;
        private SkillsConfig _skillsConfig;
        private SaveSystem _saveSystem;
        private ShopWindow _shopWindow;
        private AudioManager _audioManager;

        public event UnityAction OnSkillsChanged;

        public void Initialize(SaveSystem saveSystem, SkillsConfig skillsConfig, ShopWindow shopWindow, AudioManager audioManager)
        {
            _saveSystem = saveSystem;
            _audioManager = audioManager;
            _wallet = (Wallet)saveSystem.GetData(SavableObjectType.Wallet);
            _openedSkills = (OpenedSkills)saveSystem.GetData(SavableObjectType.OpenedSkills);
            _skillsConfig = skillsConfig;
            _shopWindow = shopWindow;
            
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
                    skillData.Sprite, skillDataByLevel.Cost, 
                    _wallet.Coins >= skillDataByLevel.Cost, 
                    skillData.isMaxLevel(skillWithLevel.Level), _shopWindow);
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
            _audioManager.PlayClip(AudioNames.BuySound);
            var skillWithLevel = _openedSkills.GetOrCreateSkillWithLevel(skillId);
            skillWithLevel.Level++;
            _wallet.Coins -= cost;
            OnSkillsChanged?.Invoke();
            _saveSystem.SaveData(SavableObjectType.Wallet);
            _saveSystem.SaveData(SavableObjectType.OpenedSkills);
            ShowShopItems();
        }
    }
}