﻿using System.Collections.Generic;
using Game.Configs.HeroConfigs;
using Game.Configs.SkillsConfigs;
using Game.Skills;
using Game.Skills.Data;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;

namespace Global
{
    public class DamageCalculator
    {
        private readonly SaveSystem.SaveSystem _saveSystem;
        private readonly HeroStatsConfig _heroHeroConfig;
        private readonly SkillsConfig _skillsConfig;
        
        public DamageCalculator(HeroStatsConfig heroConfig, SaveSystem.SaveSystem saveSystem, SkillsConfig skillsConfig)
        {
            _heroHeroConfig = heroConfig;
            _saveSystem = saveSystem;
            _skillsConfig = skillsConfig;
        }

        public void ResetStats()
        {
            var stats = (Stats)_saveSystem.GetData(SavableObjectType.Stats);
            stats.Damage = _heroHeroConfig.BaseDamage;
            stats.CritChance = _heroHeroConfig.BaseCritChance;
            stats.CritMultiplier = _heroHeroConfig.BaseCritMultiplier;
            stats.PassiveDamage = _heroHeroConfig.BasePassiveDamage;
            
            _saveSystem.SaveData(SavableObjectType.Stats);
        }


        public Dictionary<string, int> FillSkillsWithMaxLevelMap()
        {
            var skills = (OpenedSkills)_saveSystem.GetData(SavableObjectType.OpenedSkills);

            // Создаем словарь для хранения максимальных уровней навыков
            var maxSkillLevels = new Dictionary<string, int>();

            foreach (var skillWithLevel in skills.Skills)
            {
                // Если ID навыка уже есть в словаре, обновляем уровень, если новый уровень выше
                if (maxSkillLevels.ContainsKey(skillWithLevel.Id))
                {
                    if (skillWithLevel.Level > maxSkillLevels[skillWithLevel.Id])
                    {
                        maxSkillLevels[skillWithLevel.Id] = skillWithLevel.Level;
                    }
                }
                else
                {
                    // Если ID навыка нет в словаре, добавляем его
                    maxSkillLevels[skillWithLevel.Id] = skillWithLevel.Level;
                }
            }

            return maxSkillLevels;
        }
        public void ApplySkills()
        {
            ResetStats();
            var stats = (Stats)_saveSystem.GetData(SavableObjectType.Stats);
            var listSkillDataByLevel = new List<SkillDataByLevel>();
            var maxSkillLevels = FillSkillsWithMaxLevelMap();
            // Теперь у нас есть максимальные уровни для каждого уникального ID
            foreach (var skill in maxSkillLevels)
            {
                listSkillDataByLevel.Add(_skillsConfig.GetSkillData(skill.Key, skill.Value));
            }

            foreach (var skill in listSkillDataByLevel)
            {
                switch (skill.SkillType)
                {
                    case SkillType.Active:
                        stats.Damage += skill.Value;
                        break;
                    case SkillType.CritChance:
                        stats.CritChance += skill.Value + _heroHeroConfig.BaseCritChance;
                        break;
                    case SkillType.CritMultiplier:
                        stats.CritMultiplier += skill.Value + _heroHeroConfig.BaseCritMultiplier;
                        break;
                    case SkillType.PassiveDamage:
                        stats.PassiveDamage += skill.Value + _heroHeroConfig.BasePassiveDamage;
                        break;
                    case SkillType.ComboChance:
                        stats.ComboChance = skill.Value;
                        break;
                    case SkillType.InstantKillChance:
                        stats.InstantKillChance = skill.Value;
                        break;
                    case SkillType.X2Chance:
                        stats.X2Chance = skill.Value;
                        break;
                    case SkillType.DamagePercents:
                        stats.Damage += (1 + skill.Value) * _heroHeroConfig.BaseDamage;
                        break;
                    case SkillType.DamagePoints:
                        stats.Damage += skill.Value + _heroHeroConfig.BaseDamage;
                        break;
                }
            }
        }
    }
}