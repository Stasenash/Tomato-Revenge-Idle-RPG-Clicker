using UnityEngine;
using UnityEngine.UI;

namespace Meta.Achievements
{
    public class AchievementsWindow : MonoBehaviour
    {
        [SerializeField] private Button _dailyTabButton;
        [SerializeField] private Button _weeklyTabButton;
        [SerializeField] private Button _eternalTabButton;
        [SerializeField] private Button _eventsTabButton;
        
        [SerializeField] private GameObject _dailyTab;
        [SerializeField] private GameObject _weeklyTab;
        [SerializeField] private GameObject _eternalTab;
        [SerializeField] private GameObject _eventsTab;

        [SerializeField] private GameObject _dailyUnderline;
        [SerializeField] private GameObject _weeklyUnderline;
        [SerializeField] private GameObject _eternalUnderline;
        [SerializeField] private GameObject _eventsUnderline;

        public void Initialize()
        {
            _dailyTabButton.onClick.AddListener(() => ShowTab(_dailyTab, _dailyUnderline));
            _weeklyTabButton.onClick.AddListener(() => ShowTab(_weeklyTab,_weeklyUnderline));
            _eternalTabButton.onClick.AddListener(() => ShowTab(_eternalTab, _eternalUnderline));
            _eventsTabButton.onClick.AddListener(() => ShowTab(_eventsTab, _eventsUnderline));
            CloseAllTabs();
            SetMainTabActive();
        }

        private void SetMainTabActive()
        {
            _dailyTab.SetActive(true);
            _dailyUnderline.SetActive(true);
        }

        private void ShowTab(GameObject tab, GameObject underline)
        {
            CloseAllTabs();
            tab.SetActive(true);
            underline.SetActive(true);
        }
        
        private void CloseAllTabs()
        {
            _dailyTab.SetActive(false);
            _weeklyTab.SetActive(false);
            _eternalTab.SetActive(false);
            _eventsTab.SetActive(false);
            
            _dailyUnderline.SetActive(false);
            _weeklyUnderline.SetActive(false);
            _eternalUnderline.SetActive(false);
            _eventsUnderline.SetActive(false);
        }
    }
}