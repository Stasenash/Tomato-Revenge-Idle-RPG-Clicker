using System;

namespace Game.Skills.Data
{
    [Serializable]
    public struct SkillDataByLevel
    {
        public int Level;
        public float Value;
        public SkillTrigger Trigger;
        public int Cost;
        public SkillType SkillType;
    }
}