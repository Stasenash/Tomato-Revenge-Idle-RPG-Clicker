using System.Collections.Generic;
using Extensions;

namespace Global.SaveSystem.SavableObjects
{
	public class OpenedSkills : ISavable
	{
		public List<SkillWithLevel> Skills = new();

		private Dictionary<string, SkillWithLevel> _skillsMap = new();

		public SkillWithLevel GetSkillWithLevel(string skillId)
		{
			if (_skillsMap.IsNullOrEmpty()) FillSkillsMap();

			return _skillsMap.ContainsKey(skillId) ? _skillsMap[skillId] : null;
		}

		private void FillSkillsMap()
		{
			_skillsMap = new Dictionary<string, SkillWithLevel>();

			foreach (var skillWithLevel in Skills)
			{
				_skillsMap.Add(skillWithLevel.Id, skillWithLevel);
			}
		}

		public SkillWithLevel GetOrCreateSkillWithLevel(string skillId)
		{
			var skillWithLevel = GetSkillWithLevel(skillId);

			if (skillWithLevel == null)
			{
				skillWithLevel = new SkillWithLevel()
				{
					Id = skillId,
					Level = 0,
				};

				Skills.Add(skillWithLevel);
				_skillsMap.Add(skillWithLevel.Id, skillWithLevel);
			}

			return skillWithLevel;
		}
	}
}