﻿using Game.Configs.SkillsConfigs;
using Game.Enemies;
using Game.Skills.Data;
using Global.SaveSystem;
using UnityEngine.Scripting;

namespace Game.Skills.SkillOptions
{
    //preserve позволяет не стирать сборщиком мусора
    [Preserve]
    public class ExtraDamageSkill : Skill
    {
        private EnemyManager _enemyManager;
        private SkillDataByLevel _skillData;
        private SaveSystem _saveSystem;

        public override void Initialize(SkillScope scope, SkillDataByLevel skillData, SaveSystem saveSystem)
        {
            _enemyManager = scope.EnemyManager;
            _skillData = skillData;
            _saveSystem = saveSystem;
        }

        public override void SkillProcess()
        {
            _enemyManager.DamageCurrentEnemy(_skillData.Value);
        }
    }
}