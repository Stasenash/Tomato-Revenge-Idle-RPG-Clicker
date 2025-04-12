using Game.Configs.SkillsConfigs;
using Game.Skills.Data;
using Global.SaveSystem;

namespace Game.Skills
{
    public abstract class Skill
    {
        public virtual void Initialize(SkillScope scope, SkillDataByLevel skillData, SaveSystem saveSystem){}
        public virtual void OnSkillRegistered() {}
        public virtual void SkillProcess() {}
    }
}