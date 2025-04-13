using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.Profile
{
    public class ProfileWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _damageValue;
        [SerializeField] private TextMeshProUGUI _criticalChanceValue;
        [SerializeField] private TextMeshProUGUI _criticalMultiplierValue;
        [SerializeField] private TextMeshProUGUI _passiveDamageValue;
        [SerializeField] private TextMeshProUGUI _comboChanceValue;
        [SerializeField] private TextMeshProUGUI _killChanceValue;
        [SerializeField] private TextMeshProUGUI _2xChanceValue;

        [SerializeField] private Button _profileButton;
        [SerializeField] private GameObject _profilePanel;

        private bool _isProfileOpen;

        public void Initialize()
        {
            _isProfileOpen = false;
            InitializeValues();
            _profilePanel.SetActive(_isProfileOpen);
            _profileButton.onClick.AddListener(() => OpenProfile());
        }

        private void OpenProfile()
        {
            _isProfileOpen = !_isProfileOpen;
            _profilePanel.SetActive(_isProfileOpen);
        }

        private void InitializeValues()
        {
            _damageValue.text = "0";
            _criticalChanceValue.text = "0";
            _criticalMultiplierValue.text = "0";
            _passiveDamageValue.text = "0";
            _comboChanceValue.text = "0";
            _killChanceValue.text = "0";
            _2xChanceValue.text = "0";
        }
    }
}