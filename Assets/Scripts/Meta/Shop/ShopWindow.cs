using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Configs.SkillsConfigs;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

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
        
        [SerializeField] private TextMeshProUGUI _coinsText;
        
        [SerializeField] private Button _advButton;
        
        private Sequence _sequence;
        private SaveSystem _saveSystem;

        public void Initialize(int coins, SaveSystem saveSystem)
        {
            _skillsTabButton.onClick.AddListener(() => ShowTab(_skillsTab, _skillsUnderline));
            _buffsTabButton.onClick.AddListener(() => ShowTab(_buffsTab,_buffsUnderline));
            _itemsTabButton.onClick.AddListener(() => ShowTab(_itemsTab, _itemsUnderline));
            _coinsTabButton.onClick.AddListener(() => ShowTab(_coinsTab, _coinsUnderline));
            
            _saveSystem = saveSystem;
            
            SetCoinsText(coins);
            CloseAllTabs();
            SetMainTabActive();
            
            _advButton.onClick.AddListener(ShowAdvertisment);
        }

        private void ShowAdvertisment()
        {
            YG2.RewardedAdvShow("BuyButton", SetReward);
            _advButton.interactable = false;
            _sequence = DOTween.Sequence().AppendInterval(120f).OnComplete(()=> { _advButton.interactable = true; });
        }

        private void SetReward()
        {
            var wallet = (Wallet)_saveSystem.GetData(SavableObjectType.Wallet); 
            wallet.Coins += 100;
            Debug.Log(wallet.Coins);
            SetCoinsText(wallet.Coins);
            _saveSystem.SaveData(SavableObjectType.Wallet);
        }

        public void SetCoinsText(int coins)
        {
            _coinsText.text = ": " + coins;
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

        public void ShowCoinsTab()
        {
            CloseAllTabs();
            _coinsTab.SetActive(true);
            _coinsUnderline.SetActive(true);
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

        public void OnDestroy()
        {
            _sequence.Kill();
        }
    }
}