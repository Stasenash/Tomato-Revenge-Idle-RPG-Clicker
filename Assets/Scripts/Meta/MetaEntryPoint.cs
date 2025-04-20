using Game.Configs;
using Game.Configs.HeroConfigs;
using Game.Configs.SkillsConfigs;
using Global;
using Global.AudioSystem;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Meta.Achievements;
using Meta.Cutscene;
using Meta.DownPanel;
using Meta.Locations;
using Meta.Profile;
using Meta.Shop;
using SceneManagement;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Progress = Global.SaveSystem.SavableObjects.Progress;

namespace Meta
{
    public class MetaEntryPoint : EntryPoint
    {
        [SerializeField] private LocationManager _locationManager;
        [SerializeField] private ShopWindow _shopWindow;
        [SerializeField] private AchievementsWindow _achievementsWindow;
        [SerializeField] private SkillsConfig _skillsConfig;
        [SerializeField] private DownPanelManager _downPanelManager;
        [SerializeField] private ProfileWindow _profileWindow;
        [SerializeField] private SkillShop _skillShop;
        [SerializeField] private SkillShopWindow _skillShopWindow;
        [SerializeField] private HeroStatsConfig _heroStatsConfig;
        [SerializeField] private IntroCutscene _introCutscene;
        
        private SaveSystem _saveSystem;
        private AudioManager _audioManager;
        private SceneLoader _sceneLoader;

        public override void Run(SceneEnterParams enterParams)
        {
            var commonObject = GameObject.FindWithTag(TAGS.COMMON_OBJECT_TAG).GetComponent<CommonObject>();
            _saveSystem = commonObject.SaveSystem;
            //_audioManager = commonObject.AudioManager;
            _sceneLoader = commonObject.SceneLoader;
            
            _downPanelManager.Initialize();
            var wallet = (Wallet) _saveSystem.GetData(SavableObjectType.Wallet);
            _shopWindow.Initialize(wallet.Coins);
            
            _achievementsWindow.Initialize();
            _profileWindow.Initialize((Stats)_saveSystem.GetData(SavableObjectType.Stats));
            _skillShop.Initialize(_saveSystem, _skillsConfig, _shopWindow);
            _skillShopWindow.Initialize();
            UpdateStats();
            _skillShop.OnSkillsChanged += () =>
            {
                UpdateStats();
                _shopWindow.SetCoinsText(((Wallet)_saveSystem.GetData(SavableObjectType.Wallet)).Coins);
            };
            
            var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
            
            _locationManager.Initialize(progress, StartLevel);

            var intros = (Cutscenes)_saveSystem.GetData(SavableObjectType.Cutscenes);
            _introCutscene.Initialize();
            _introCutscene.gameObject.SetActive(!intros.IsIntroShowed);
            if (!intros.IsIntroShowed)
            {
                _introCutscene.ShowIntroCutscene();
                intros.IsIntroShowed = true;
                _saveSystem.SaveData(SavableObjectType.Cutscenes);
            }
            //_audioManager.PlayClip(AudioNames.BackgroundMetaMusic)
        }

        private void UpdateStats()
        {
            new DamageCalculator(_heroStatsConfig, _saveSystem, _skillsConfig);
            _saveSystem.SaveData(SavableObjectType.Stats);
            //перерисовка статов
            _profileWindow.UpdateValues((Stats)_saveSystem.GetData(SavableObjectType.Stats));
        }

        private void StartLevel(int location, int level)
        {
            _sceneLoader.LoadGameplayScene(new GameEnterParams(location, level));
        }
    }
}