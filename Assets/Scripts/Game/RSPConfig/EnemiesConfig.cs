using System.Collections.Generic;
using Extensions;
using Game.Enemies;
using NUnit.Framework;
using UnityEngine;

namespace Game.Configs.Enemies_Configs
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
            _dataMap = new();
            foreach (var data in _data)
            {
                _dataMap[data.From] ??= new();
                _dataMap[data.From][data.To] = data.Multiplier;
            }
        }
    }
}

