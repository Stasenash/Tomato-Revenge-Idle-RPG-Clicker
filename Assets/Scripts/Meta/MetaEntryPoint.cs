using Game.Configs;
using Game.Configs.HeroConfigs;
using Game.Configs.SkillsConfigs;
using Game.DownPanel;
using Global;
using Global.AudioSystem;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Meta.Achievements;
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
            _shopWindow.Initialize();
            _achievementsWindow.Initialize();
            _profileWindow.Initialize((Stats)_saveSystem.GetData(SavableObjectType.Stats));
            _skillShop.Initialize(_saveSystem, _skillsConfig);
            _skillShopWindow.Initialize();
            _skillShop.OnSkillsChanged += () =>
            {
                new DamageCalculator(_heroStatsConfig, _saveSystem, _skillsConfig).ApplySkills();
                _saveSystem.SaveData(SavableObjectType.Stats);
                //перерисовка статов
                _profileWindow.UpdateValues((Stats)_saveSystem.GetData(SavableObjectType.Stats));
            };
            
            var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
            
            _locationManager.Initialize(progress, StartLevel);
            //_audioManager.PlayClip(AudioNames.BackgroundMetaMusic)
            
            
        }

        private void StartLevel(int location, int level)
        {
            //todo: перед загрузкой уровня надо менять дамаг и все такое
            _sceneLoader.LoadGameplayScene(new GameEnterParams(location, level));
        }
    }
}