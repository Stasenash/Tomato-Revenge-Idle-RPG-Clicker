using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Meta.Locations
{
    public class Location : MonoBehaviour
    {
        [SerializeField] private List<Pin> _pins;

        public void Initialize(UnityAction<int> levelStartCallback)
        {
            var currentLevel = 0;
            
            for (int i = 0; i < _pins.Count; i++)
            {
                var levelInt = i;
                PinType pinType = currentLevel > levelInt 
                    ? PinType.Passed 
                    :currentLevel == levelInt
                        ? PinType.Current 
                        : PinType.Closed;
                _pins[i].Initialize(levelInt, pinType, () => levelStartCallback?.Invoke(levelInt));
            }
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}