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
        
        private bool isShopOpen = false;
        private bool isAchievementsOpen = false;

        public void Initialize()
        {
            _shopButton.onClick.AddListener(() => OpenOrCloseShop());
            _achievementsButton.onClick.AddListener(() => OpenOrCloseAchievements());
        }

        private void OpenOrCloseAchievements()
        {
            Debug.Log("Clicked achievements button");
        }

        private void OpenOrCloseShop()
        {
            isShopOpen = !isShopOpen;
            _shopWindow.SetActive(isShopOpen);
            _shopUnderline.SetActive(isShopOpen);
        }
    }
}