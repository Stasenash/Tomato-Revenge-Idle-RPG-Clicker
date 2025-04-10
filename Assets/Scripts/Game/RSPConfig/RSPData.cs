using System;
using Game.Enemies;

namespace Game.Configs.Enemies_Configs
{
    [Serializable]
    public struct RSPData
    {
        public TechniqueType From;
        public TechniqueType To;
        public float Multiplier;
    }
}