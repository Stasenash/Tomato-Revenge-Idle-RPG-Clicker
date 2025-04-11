using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Skills.Data
{
    [Serializable]
    public struct SkillData
    {
        public string Id;
        public Sprite Sprite;
        public List<SkillDataByLevel> SkillLevels;

        public SkillDataByLevel GetSkillDataByLevel(int level)
        {
            return SkillLevels.Find(x => x.Level == level);
        }

        public bool isMaxLevel(int level)
        {
            return SkillLevels.Max(x => x.Level) == level;
        }
    }
}