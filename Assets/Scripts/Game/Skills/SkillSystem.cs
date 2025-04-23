using System;
using System.Collections.Generic;
using Game.Configs.Enemies_Configs;
using Game.Configs.HeroConfigs;
using Game.Configs.SkillsConfigs;
using Game.Enemies;
using Game.RSPConfig;
using Global;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;

namespace Game.Skills
{
    public class SkillSystem
    {
        private readonly SaveSystem _saveSystem;
        private readonly HeroStatsConfig _heroStatsConfig;
        private readonly RSPConfig.RSPConfig _rspConfig;
        private readonly EnemyManager _enemyManager;

        public SkillSystem(EnemyManager enemyManager, SaveSystem saveSystem, HeroStatsConfig heroStatsConfig, RSPConfig.RSPConfig rspConfig)
        {
            _saveSystem = saveSystem;
            _rspConfig = rspConfig;
            _enemyManager = enemyManager;
            _heroStatsConfig = heroStatsConfig;
        }

        public void InvokeTrigger(SkillTrigger trigger)
        {
            var techniqueType = TechniqueType.Taijutsu;
            switch (trigger)
            {
                case SkillTrigger.OnTaijutsu: techniqueType = TechniqueType.Taijutsu; break;
                case SkillTrigger.OnNinjutsu: techniqueType = TechniqueType.Ninjutsu; break;
                case SkillTrigger.OnGenjutsu: techniqueType = TechniqueType.Genjutsu; break;
            }
            var damage = new DamageCalculator(_saveSystem, _heroStatsConfig).CalculateTotalDamage();
            var calculatedDamage = _rspConfig.CalculateDamage(techniqueType,
                _enemyManager.GetCurrentEnemyTechniqueType(), damage);
            _enemyManager.DamageCurrentEnemy(calculatedDamage);
        }
    }
}