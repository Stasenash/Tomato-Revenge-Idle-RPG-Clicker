using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Progress = Global.SaveSystem.SavableObjects.Progress;

namespace Meta.Locations
{
    public class LocationManager : MonoBehaviour
    {
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;
        
        [SerializeField] private List<Location> _locations;
        private int _currentLocation;

        public void Initialize(Progress progress, UnityAction<int, int> startLevelCallback)
        {
            _currentLocation = progress.CurrentLocation;
            InitLocations(progress, startLevelCallback);
            
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

        private void InitLocations(Progress progress, UnityAction<int, int> startLevelCallback)
        {
            for (var i = 0; i < _locations.Count; i++)
            {
                var locationNum = i;

                ProgressState isLocationPassed = progress.CurrentLocation > locationNum
                    ? ProgressState.Passed
                    : progress.CurrentLocation == locationNum
                        ? ProgressState.Current
                        : ProgressState.Closed;

                //var isLocationPassed = progress.CurrentLocation > locationNum;
                var currentLevel = progress.CurrentLevel;
                
                
                _locations[i].Initialize(isLocationPassed, currentLevel, level => startLevelCallback.Invoke(locationNum, level));
                _locations[i].SetActive(progress.CurrentLocation == locationNum);
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