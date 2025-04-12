using System;
using UnityEngine;

namespace Game.Configs.HeroConfigs
{
    [CreateAssetMenu(menuName = "Configs/HeroStatsConfig", fileName = "HeroStatsConfig")]
    public class HeroStatsConfig : ScriptableObject
    {
        public float BaseDamage;
        public float BaseCritChance;
        public float BaseCritMultiplier;
        public float BasePassiveDamage;
    }
}