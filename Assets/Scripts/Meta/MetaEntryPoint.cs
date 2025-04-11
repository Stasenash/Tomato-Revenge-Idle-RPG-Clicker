using Game.Configs;
using Game.Configs.SkillsConfigs;
using Game.DownPanel;
using Global.AudioSystem;
using Global.SaveSystem;
using Meta.Locations;
using Meta.Shop;
using SceneManagement;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Progress = Global.SaveSystem.SavableObjects.Progress;

namespace Meta
{
    public class MetaEntryPoint : EntryPoint
    {
        [SerializeField] private LocationManager _locationManager;
        [SerializeField] private ShopWindow _shopWindow;
        [SerializeField] private SkillsConfig _skillsConfig;
        [SerializeField] private DownPanelManager _downPanelManager;
        
        private SaveSystem _saveSystem;
        private AudioManager _audioManager;
        private SceneLoader _sceneLoader;

        public override void Run(SceneEnterParams enterParams)
        {
            var commonObject = GameObject.FindWithTag(TAGS.COMMON_OBJECT_TAG).GetComponent<CommonObject>();
            _saveSystem = commonObject.SaveSystem;
            //_audioManager = commonObject.AudioManager;
            _sceneLoader = commonObject.SceneLoader;
            
            var progress = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
            
            _locationManager.Initialize(progress, StartLevel);
            _shopWindow.Initialize(_saveSystem, _skillsConfig);
            _downPanelManager.Initialize();
            //_audioManager.PlayClip(AudioNames.BackgroundMetaMusic)
        }

        private void StartLevel(int location, int level)
        {
            _sceneLoader.LoadGameplayScene(new GameEnterParams(location, level));
        }
    }
}