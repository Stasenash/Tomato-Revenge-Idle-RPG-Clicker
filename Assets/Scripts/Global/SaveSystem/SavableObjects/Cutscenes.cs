using Global.SaveSystem;

namespace Global.SaveSystem.SavableObjects
{
    public class Cutscenes : ISavable
    {
        public bool IsIntroShowed = false;
        public bool IsEndingShowed = false;
        public bool IsHelperShowed = false;
    }
}