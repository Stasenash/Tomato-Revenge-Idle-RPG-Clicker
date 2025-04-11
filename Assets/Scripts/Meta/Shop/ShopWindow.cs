using System.Collections.Generic;
using Game.Configs.SkillsConfigs;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta.Shop
{
    public class ShopWindow : MonoBehaviour
    {
        [SerializeField] private Button _skillsTabButton;
        [SerializeField] private Button _buffsTabButton;
        [SerializeField] private Button _itemsTabButton;
        [SerializeField] private Button _coinsTabButton;
        
        [SerializeField] private GameObject _skillsTab;
        [SerializeField] private GameObject _buffsTab;
        [SerializeField] private GameObject _itemsTab;
        [SerializeField] private GameObject _coinsTab;

        [SerializeField] private GameObject _skillsUnderline;
        [SerializeField] private GameObject _buffsUnderline;
        [SerializeField] private GameObject _itemsUnderline;
        [SerializeField] private GameObject _coinsUnderline;

        public void Initialize()
        {
            _skillsTabButton.onClick.AddListener(() => ShowTab(_skillsTab, _skillsUnderline));
            _buffsTabButton.onClick.AddListener(() => ShowTab(_buffsTab,_buffsUnderline));
            _itemsTabButton.onClick.AddListener(() => ShowTab(_itemsTab, _itemsUnderline));
            _coinsTabButton.onClick.AddListener(() => ShowTab(_coinsTab, _coinsUnderline));
            CloseAllTabs();
            SetMainTabActive();
        }

        private void SetMainTabActive()
        {
            _skillsTab.SetActive(true);
            _skillsUnderline.SetActive(true);
        }

        private void ShowTab(GameObject tab, GameObject underline)
        {
            CloseAllTabs();
            tab.SetActive(true);
            underline.SetActive(true);
        }
        
        private void CloseAllTabs()
        {
            _itemsTab.SetActive(false);
            _skillsTab.SetActive(false);
            _buffsTab.SetActive(false);
            _coinsTab.SetActive(false);
            
            _skillsUnderline.SetActive(false);
            _buffsUnderline.SetActive(false);
            _itemsUnderline.SetActive(false);
            _coinsUnderline.SetActive(false);
        }
    }
}