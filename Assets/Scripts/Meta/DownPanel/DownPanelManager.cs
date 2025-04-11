using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.DownPanel
{
    public class DownPanelManager : MonoBehaviour
    {
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _achievementsButton;
        
        [SerializeField] private GameObject _shopWindow;
        
        [SerializeField] private GameObject _shopUnderline;
        [SerializeField] private GameObject _achievementsUnderline;

        private bool isShopOpen;
        private bool isAchievementsOpen;

        public void Initialize()
        {
            isAchievementsOpen = false;
            isShopOpen = false;
            _shopButton.onClick.AddListener(() => OpenOrCloseShop());
            _achievementsButton.onClick.AddListener(() => OpenOrCloseAchievements());
        }

        private void OpenOrCloseAchievements()
        {
            isShopOpen = false;
            _shopWindow.SetActive(isShopOpen);
            _shopUnderline.SetActive(isShopOpen);
            
            
        }

        private void OpenOrCloseShop()
        {
            isShopOpen = !isShopOpen;
            _shopWindow.SetActive(isShopOpen);
            _shopUnderline.SetActive(isShopOpen);

            isAchievementsOpen = false;
            //_achievementsWindow.SetActive(isAchievementsOpen);
            //_achievements.SetActive(isAchievementsOpen);
        }
        
        
    }
}