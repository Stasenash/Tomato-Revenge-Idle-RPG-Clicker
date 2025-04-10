using System.Collections.Generic;

namespace Global.SaveSystem.SavableObjects
{
    public class OpenedSkills : ISavable
    {
        public List<SkillWithLevel> Skills = new()
        {
            new()
            {
                Id="ExtraDamageSkill", 
                Level = 1
            }
        };

        //TODO: переввести на словарь по-хорошему, но когда мы будем изменять кол-во скиллов, то придется менять и словарь (подумоть)
        public SkillWithLevel GetOrCreateSkillWithLevel(string skillId)
        {
            foreach (var skillWithLevel in Skills)
            {
                if (skillWithLevel.Id == skillId)
                {
                    return skillWithLevel;
                }
            }

            var newSkill = new SkillWithLevel()
            {
                Id = skillId,
                Level = 0
            };
            Skills.Add(newSkill);
            return newSkill;
        }
    }
}