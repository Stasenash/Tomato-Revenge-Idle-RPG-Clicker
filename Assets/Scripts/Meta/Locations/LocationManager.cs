using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Meta.Locations
{
    public class LocationManager : MonoBehaviour
    {
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;
        
        [SerializeField] private List<Location> _locations;
        private int _currentLocation;

        public void Initialize(int currentLocation, UnityAction<Vector2Int> startLevelCallback)
        {
            _currentLocation = currentLocation;
            LocationInitialize(currentLocation, startLevelCallback);
            
            InitialMoveLocationsButton();
        }

        private void InitialMoveLocationsButton()
        {
            _nextButton.onClick.AddListener(ShowNextLocation);
            _previousButton.onClick.AddListener(ShowPreviousLocation);
            
            if (_currentLocation == _locations.Count - 1)
            {
                _nextButton.gameObject.SetActive(false);
            }

            if (_currentLocation == 0)
            {
                _previousButton.gameObject.SetActive(false);
            }
        }

        private void LocationInitialize(int currentLocation, UnityAction<Vector2Int> startLevelCallback)
        {
            for (var i = 0; i < _locations.Count; i++)
            {
                var locationNum = i;
                _locations[i].Initialize(level => startLevelCallback.Invoke(new Vector2Int(locationNum, level)));
                _locations[i].SetActive(currentLocation == locationNum);
            }
        }

        private void ShowNextLocation()
        {
            _locations[_currentLocation].SetActive(false);
            _currentLocation++;
            _locations[_currentLocation].SetActive(true);

            if (_currentLocation == _locations.Count - 1)
            {
                _nextButton.gameObject.SetActive(false);
            }

            if (_currentLocation > 0)
            {
                _previousButton.gameObject.SetActive(true);
            }
        }

        private void ShowPreviousLocation()
        {
            _locations[_currentLocation].SetActive(false);
            _currentLocation--;
            _locations[_currentLocation].SetActive(true);
            
            if (_currentLocation == 0)
            {
                _previousButton.gameObject.SetActive(false);
            }

            if (_currentLocation < _locations.Count - 1)
            {
                _nextButton.gameObject.SetActive(true);
            }
        }
    }
}