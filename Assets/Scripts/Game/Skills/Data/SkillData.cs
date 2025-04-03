using System;
using System.Collections.Generic;

namespace Game.Skills.Data
{
    [Serializable]
    public struct SkillData
    {
        public string Id;
        public List<SkillDataByLevel> SkillLevels;
    }
}