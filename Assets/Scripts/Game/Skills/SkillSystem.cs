using System;
using System.Collections.Generic;
using Game.Configs.HeroConfigs;
using Game.Configs.SkillsConfigs;
using Game.Enemies;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using UnityEditor.Experimental.GraphView;

namespace Game.Skills
{
    public class SkillSystem
    {
        private SkillScope _scope;
        private SkillsConfig _skillsConfig;
        
        private Dictionary<SkillTrigger, List<Skill>> _skillsByTrigger;
        private readonly SaveSystem _saveSystem;

        public SkillSystem(OpenedSkills openedSkills, SkillsConfig skillsConfig, EnemyManager enemyManager, SaveSystem saveSystem)
        {
            _skillsByTrigger = new();
            _skillsConfig = skillsConfig;
            _saveSystem = saveSystem;
            _scope = new()
            {
                EnemyManager = enemyManager
            };
            
            foreach (var skill in openedSkills.Skills)
            {
                RegisterSkill(skill);
            }
        }

        public void InvokeTrigger(SkillTrigger trigger)
        {
            if (!_skillsByTrigger.ContainsKey(trigger))
                return;
            var skillsToActivate = _skillsByTrigger[trigger];
            foreach (var skill in skillsToActivate)
            {
                skill.SkillProcess();
            }
        }
        
        
        private void RegisterSkill(SkillWithLevel skill)
        {
            var skillData = _skillsConfig.GetSkillData(skill.Id, skill.Level);
            
            var skillType = Type.GetType($"Game.Skills.SkillOptions.{skill.Id}");
            if (skillType == null)
            {
                throw new Exception($"Skill {skill.Id} not found");
            }

            if (Activator.CreateInstance(skillType) is not Skill skillInstance)
            {
                throw new Exception($"Cannot create skill with Id: {skill.Id}");
            }
            
            skillInstance.Initialize(_scope, skillData, _saveSystem);

            if (!_skillsByTrigger.ContainsKey(skillData.Trigger))
            {
                _skillsByTrigger[skillData.Trigger] = new();
            }
            
            _skillsByTrigger[skillData.Trigger].Add(skillInstance);
            skillInstance.OnSkillRegistered();
        }
    }
}