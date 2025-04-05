using Game.Configs.SkillsConfigs;
using Game.Enemies;
using Game.Skills.Data;
using UnityEngine.Scripting;

namespace Game.Skills.SkillOptions
{
    [Preserve]
    public class ExtraDamageSkill : Skill
    {
        private EnemyManager _enemyManager;
        private SkillDataByLevel _skillData;
        public override void Initialize(SkillScope scope, SkillDataByLevel skillData)
        {
            _enemyManager = scope.EnemyManager;
            _skillData = skillData;
        }

        public override void SkillProcess()
        {
            _enemyManager.DamageCurrentEnemy(_skillData.Value);
        }
    }
}