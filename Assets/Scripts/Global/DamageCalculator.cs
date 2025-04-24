using System.Collections.Generic;
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
            ResetStats();
            ApplySkills();
            ApplyBuffs();
        }

        public DamageCalculator(SaveSystem.SaveSystem saveSystem, HeroStatsConfig heroHeroConfig)
        {
            _saveSystem = saveSystem;
            _heroHeroConfig = heroHeroConfig;
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

        
        public float CalculateTotalDamage()
        {
            var stats = (Stats)_saveSystem.GetData(SavableObjectType.Stats);
            float damage =  stats.Damage;
            if (CheckChance(stats.CritChance))
                damage = stats.CritMultiplier * stats.Damage;
            if (CheckChance(stats.X2Chance))
                damage += stats.Damage;
            if (CheckChance(stats.InstantKillChance))
                damage += 100000000;
            return damage;
        }

        private bool CheckChance(float chance)
        {
            var random = UnityEngine.Random.Range(0f, 1f);
            return random <= chance;
        }

        public void ApplyBuffs()
        {
            var stats = (Stats)_saveSystem.GetData(SavableObjectType.Stats);
            var buffs = (Buffs)_saveSystem.GetData(SavableObjectType.Buffs);
            if (buffs.AttackBuff)
                stats.Damage *= 1.1f;
            if (buffs.CritBuff)
                stats.CritMultiplier *= 1.5f;
            if (buffs.PassiveBuff)
                stats.PassiveDamage *= 10;
            if (buffs.X2Buff)
                stats.Damage *= 2f;
            if (buffs.InstantKillBuff)
                stats.InstantKillChance += 0.0005f;
            _saveSystem.SaveData(SavableObjectType.Stats);
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
                        break;
                    case SkillType.CritChance:
                        stats.CritChance += skill.Value;
                        break;
                    case SkillType.CritMultiplier:
                        stats.CritMultiplier += skill.Value;
                        break;
                    case SkillType.PassiveDamage:
                        stats.PassiveDamage += skill.Value;
                        break;
                    case SkillType.PassivePercents:
                        stats.PassiveDamage *= 1 + skill.Value;
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
                    case SkillType.DamagePoints:
                        stats.Damage += skill.Value;
                        break;
                    case SkillType.DamagePercents:
                        stats.Damage *= 1 + skill.Value;
                        break;
                }
            }
            _saveSystem.SaveData(SavableObjectType.Stats);
        }
    }
}