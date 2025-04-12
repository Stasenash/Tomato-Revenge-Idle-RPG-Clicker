using Game.Configs.Enemies_Configs;
using Game.Configs.HeroConfigs;
using Game.Enemies;
using Game.Skills.Data;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using UnityEngine.Scripting;

namespace Game.Skills.SkillOptions
{
    [Preserve]
    public class GreatBlackSky : Skill
    {
        private SkillDataByLevel _skillData;
        private SaveSystem _saveSystem;

        public override void Initialize(SkillScope scope, SkillDataByLevel skillData, SaveSystem saveSystem)
        {
            _skillData = skillData;
            _saveSystem = saveSystem;
        }

        public override void SkillProcess()
        {
            var stats = (Stats)_saveSystem.GetData(SavableObjectType.Stats);
            stats.PassiveDamage += _skillData.Value;
            _saveSystem.SaveData(SavableObjectType.Stats);
        }
    }
}