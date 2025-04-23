using Global.AudioSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.Shop
{
    public class SkillShopWindow : MonoBehaviour
    {
        [SerializeField] private Button _ninjutsuTabButton;
        [SerializeField] private Button _taijutsuTabButton;
        [SerializeField] private Button _genjutsuTabButton;
        
        [SerializeField] private GameObject _ninjutsuTab;
        [SerializeField] private GameObject _taijutsuTab;
        [SerializeField] private GameObject _genjutsuTab;

        [SerializeField] private GameObject _ninjutsuUnderline;
        [SerializeField] private GameObject _taijutsuUnderline;
        [SerializeField] private GameObject _genjutsuUnderline;
        private AudioManager _audioManager;

        public void Initialize(AudioManager audioManager)
        {
            _ninjutsuTabButton.onClick.AddListener(()=> ShowTab(_ninjutsuTab, _ninjutsuUnderline));
            _taijutsuTabButton.onClick.AddListener(()=> ShowTab(_taijutsuTab, _taijutsuUnderline));
            _genjutsuTabButton.onClick.AddListener(()=> ShowTab(_genjutsuTab, _genjutsuUnderline));
            _audioManager = audioManager;
            ShowTab(_ninjutsuTab, _ninjutsuUnderline);
        }

        private void ShowTab(GameObject tab, GameObject underline)
        {
            _audioManager.PlayClip(AudioNames.InterfaceClick);
            CloseAllTabs();
            tab.SetActive(true);
            underline.SetActive(true);
        }

        private void CloseAllTabs()
        {
            _ninjutsuTab.SetActive(false);
            _taijutsuTab.SetActive(false);
            _genjutsuTab.SetActive(false);
            
            _ninjutsuUnderline.SetActive(false);
            _taijutsuUnderline.SetActive(false);
            _genjutsuUnderline.SetActive(false);
        }
    }
}