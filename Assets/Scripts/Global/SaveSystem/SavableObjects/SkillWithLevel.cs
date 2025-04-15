using System;

namespace Global.SaveSystem.SavableObjects
{
    [Serializable]
    public class SkillWithLevel : ISavable
    {
        public string Id;
        public int Level;
    }
}