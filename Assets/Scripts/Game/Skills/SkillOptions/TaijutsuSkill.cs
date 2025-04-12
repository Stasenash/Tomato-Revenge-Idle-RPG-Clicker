using Game.Configs.Enemies_Configs;
using Game.Configs.SkillsConfigs;
using Game.Enemies;
using Game.Skills.Data;
using Global.SaveSystem;
using UnityEngine.Scripting;

namespace Game.Skills.SkillOptions
{
    //preserve позволяет не стирать сборщиком мусора
    [Preserve]
    public class TaijutsuSkill : Skill
    {
        //TODO: сделать красивый отдельный классик чтобы переисползовать
        private EnemyManager _enemyManager;
        private SkillDataByLevel _skillData;
        private RSPConfig _rspConfig;

        public override void Initialize(SkillScope scope, SkillDataByLevel skillData, SaveSystem saveSystem)
        {
            _enemyManager = scope.EnemyManager;
            _skillData = skillData;
            _rspConfig = scope.RSPConfig;
        }

        public override void SkillProcess()
        {
            var calculatedDamage = _rspConfig.CalculateDamage(TechniqueType.Taijutsu,
                _enemyManager.GetCurrentEnemyTechniqueType(), _skillData.Value);
            _enemyManager.DamageCurrentEnemy(_skillData.Value);
            _enemyManager.DamageCurrentEnemy(calculatedDamage);
        }
    }
}