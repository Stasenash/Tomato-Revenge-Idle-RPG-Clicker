using System.Collections.Generic;
using Game.Skills.Data;
using UnityEngine;

namespace Game.Configs.SkillsConfigs
{
    [CreateAssetMenu(menuName = "Configs/SkillsConfig",fileName = "SkillsConfig")]
    public class SkillsConfig : ScriptableObject
    {
        public List<SkillData> Skills;
        
        private Dictionary<string, Dictionary<int, SkillDataByLevel>> _skillDataByLevelMap;
        
        public SkillDataByLevel GetSkillData(string skillId, int level)
        {
            if (_skillDataByLevelMap == null || _skillDataByLevelMap.Count == 0)
            {
                FillSkillDateMap();
            }

            return _skillDataByLevelMap[skillId][level];
        }

        private void FillSkillDateMap()
        {
            _skillDataByLevelMap = new Dictionary<string, Dictionary<int, SkillDataByLevel>>();
            foreach (var skillData in Skills)
            {
                if (!_skillDataByLevelMap.ContainsKey(skillData.Id))
                {
                    _skillDataByLevelMap[skillData.Id] = new();
                }
                foreach (var skillDataByLevel in skillData.SkillLevels)
                {
                    _skillDataByLevelMap[skillData.Id][skillDataByLevel.Level] = skillDataByLevel;
                }
            }
        }
    }
}