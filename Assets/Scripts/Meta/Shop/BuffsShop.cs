using System;
using System.Collections.Generic;
using Game.Configs.SkillsConfigs;
using Game.EndLevelWindow;
using Global.AudioSystem;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta.Shop
{
    public class BuffsShop : MonoBehaviour
    {
        [SerializeField] private Button _attackBuffButton;
        [SerializeField] private Button _critBuffButton;
        [SerializeField] private Button _x2BuffButton;
        [SerializeField] private Button _passsiveBuffButton;
        [SerializeField] private Button _instantKillBuffButton;
        
        private Dictionary<Button, int> _buttonsCost;
        private Dictionary<Button, bool> _buffsBought;
        
        private SaveSystem _saveSystem;
        private ShopWindow _shopWindow;
        private AudioManager _audioManager;
        
        public event Action OnBuffBought;

        public void Initialize(SaveSystem saveSystem, ShopWindow shopWindow, AudioManager audioManager)
        {
            _saveSystem = saveSystem;
            _audioManager = audioManager;
            _shopWindow = shopWindow;
            _shopWindow.OnChangeCoins += UpdateBuffsItems;

            _attackBuffButton.onClick.AddListener(() =>
            {
                BuyBuff(100);
                var buffs = (Buffs)_saveSystem.GetData(SavableObjectType.Buffs);
                buffs.AttackBuff = true;
                _saveSystem.SaveData(SavableObjectType.Buffs);
                UpdateBuffsItems();
            });
            
            _critBuffButton.onClick.AddListener(() =>
            {
                BuyBuff(150);
                var buffs = (Buffs)_saveSystem.GetData(SavableObjectType.Buffs);
                buffs.CritBuff = true;
                _saveSystem.SaveData(SavableObjectType.Buffs);
                UpdateBuffsItems();
            });
            
            _x2BuffButton.onClick.AddListener(() =>
            {
                BuyBuff(400);
                var buffs = (Buffs)_saveSystem.GetData(SavableObjectType.Buffs);
                buffs.X2Buff = true;
                _saveSystem.SaveData(SavableObjectType.Buffs);
                UpdateBuffsItems();
            });
            
            _passsiveBuffButton.onClick.AddListener(() =>
            {
                BuyBuff(500);
                var buffs = (Buffs)_saveSystem.GetData(SavableObjectType.Buffs);
                buffs.PassiveBuff = true;
                _saveSystem.SaveData(SavableObjectType.Buffs);
                UpdateBuffsItems();
            });
            
            _instantKillBuffButton.onClick.AddListener(() =>
            {
                BuyBuff(1000);
                var buffs = (Buffs)_saveSystem.GetData(SavableObjectType.Buffs);
                buffs.InstantKillBuff = true;
                _saveSystem.SaveData(SavableObjectType.Buffs);
                UpdateBuffsItems();
            });
            
            InitializeButtonCost();
            InitializeBuffsBought();
            UpdateBuffsItems();
        }

        private void InitializeButtonCost()
        {
            _buttonsCost = new Dictionary<Button, int>();
            _buttonsCost.Add(_attackBuffButton, 100);
            _buttonsCost.Add(_critBuffButton, 150);
            _buttonsCost.Add(_x2BuffButton, 400);
            _buttonsCost.Add(_passsiveBuffButton, 500);
            _buttonsCost.Add(_instantKillBuffButton, 1000);
        }


        private void InitializeBuffsBought()
        {
            var buffs = (Buffs) _saveSystem.GetData(SavableObjectType.Buffs);
            _buffsBought = new Dictionary<Button, bool>();
            _buffsBought.Add(_attackBuffButton, buffs.AttackBuff);
            _buffsBought.Add(_critBuffButton, buffs.CritBuff);
            _buffsBought.Add(_x2BuffButton, buffs.X2Buff);
            _buffsBought.Add(_passsiveBuffButton, buffs.PassiveBuff);
            _buffsBought.Add(_instantKillBuffButton, buffs.InstantKillBuff);
        }
        
        private void UpdateBuffsItems()
        {
            foreach (var buttonCost in _buttonsCost)
            {
                buttonCost.Key.interactable = true;
            }
            
            var wallet = (Wallet)_saveSystem.GetData(SavableObjectType.Wallet);
            foreach (var buttonCost in _buttonsCost)
            {
                if (wallet.Coins < buttonCost.Value)
                    buttonCost.Key.interactable = false;
            }

            InitializeBuffsBought();
            foreach (var buttonBought in _buffsBought)
            {
                if (buttonBought.Value)
                    buttonBought.Key.interactable = false;
            }
        }
        
        private void BuyBuff(int cost)
        {
            _audioManager.PlayClip(AudioNames.BuySound);
            var wallet = (Wallet)_saveSystem.GetData(SavableObjectType.Wallet);
            wallet.Coins -= cost;
            _saveSystem.SaveData(SavableObjectType.Wallet);
            OnBuffBought?.Invoke();
        }
    }
}