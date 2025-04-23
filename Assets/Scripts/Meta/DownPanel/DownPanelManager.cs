using Global.AudioSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.DownPanel
{
    public class DownPanelManager : MonoBehaviour
    {
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _achievementsButton;
        
        [SerializeField] private GameObject _shopWindow;
        [SerializeField] private GameObject _achievementsWindow;
        
        [SerializeField] private GameObject _shopUnderline;
        [SerializeField] private GameObject _achievementsUnderline;

        private bool isShopOpen;
        private bool isAchievementsOpen;
        private AudioManager _audioManager;

        public void Initialize(AudioManager audioManager)
        {
            isAchievementsOpen = false;
            isShopOpen = false;
            _audioManager = audioManager;
            _shopButton.onClick.AddListener(() => OpenOrCloseShop());
            _achievementsButton.onClick.AddListener(() => OpenOrCloseAchievements());
        }
        
        private void OpenOrCloseAchievements()
        {
            isShopOpen = false;
            _shopWindow.SetActive(isShopOpen);
            _shopUnderline.SetActive(isShopOpen);
            
            isAchievementsOpen = !isAchievementsOpen;
            _achievementsWindow.SetActive(isAchievementsOpen);
            _achievementsUnderline.SetActive(isAchievementsOpen);
        }

        private void OpenOrCloseShop()
        {
            _audioManager.PlayClip(AudioNames.InterfaceClick);
            isAchievementsOpen = false;
            _achievementsWindow.SetActive(isAchievementsOpen);
            _achievementsUnderline.SetActive(isAchievementsOpen);
            
            isShopOpen = !isShopOpen;
            _shopWindow.SetActive(isShopOpen);
            _shopUnderline.SetActive(isShopOpen);
        }
        
        
    }
}