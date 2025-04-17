using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace Game.RSPConfig
{
    [CreateAssetMenu(menuName = "Configs/RSPConfig", fileName = "RSPConfig")]
    public class RSPConfig : ScriptableObject
    {
        [SerializeField] private List<RSPData> _data;
        private Dictionary<TechniqueType, Dictionary<TechniqueType, float>> _dataMap;

        public float CalculateDamage(TechniqueType from, TechniqueType to, float damage)
        {
            if (_dataMap.IsNullOrEmpty()) FillDataMap();
           return _dataMap[from][to] * damage;
        }

        
        private void FillDataMap()
        {
            _dataMap = new Dictionary<TechniqueType, Dictionary<TechniqueType, float>>();

            foreach (var data in _data)
            {
                if (!_dataMap.ContainsKey(data.From))
                {
                    _dataMap[data.From] = new Dictionary<TechniqueType, float>();
                }

                _dataMap[data.From][data.To] = data.Multiplier;
            }
        }
    }
}

