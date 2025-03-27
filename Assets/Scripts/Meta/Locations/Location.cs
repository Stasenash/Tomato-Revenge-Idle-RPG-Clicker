using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Meta.Locations
{
    public class Location : MonoBehaviour
    {
        [SerializeField] private List<Pin> _pins;

        public void Initialize(ProgressState locationState, int currentLevel, UnityAction<int> levelStartCallback)
        {
            for (int i = 0; i < _pins.Count; i++)
            {
                var levelInt = i;

                var pinState = locationState switch
                {
                    ProgressState.Passed => ProgressState.Passed,
                    ProgressState.Closed => ProgressState.Closed,
                    _ => currentLevel > levelInt ? ProgressState.Passed :
                        currentLevel == levelInt ? ProgressState.Current : ProgressState.Closed
                };

                if (pinState == ProgressState.Closed)
                {
                    _pins[i].Initialize(levelInt, pinState, null);
                }

                _pins[i].Initialize(levelInt, pinState, () => levelStartCallback?.Invoke(levelInt));
            }
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}