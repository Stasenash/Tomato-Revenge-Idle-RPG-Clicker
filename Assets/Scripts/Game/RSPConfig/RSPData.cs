using System;

namespace Game.RSPConfig
{
    [Serializable]
    public struct RSPData
    {
        public TechniqueType From;
        public TechniqueType To;
        public float Multiplier;
    }
}