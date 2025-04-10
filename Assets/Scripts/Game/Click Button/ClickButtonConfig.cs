using Game.Enemies;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Click_Button
{
    [CreateAssetMenu(menuName = "Configs/ClickButtonConfig",fileName = "ClickButtonConfig")]
    public class ClickButtonConfig : ScriptableObject
    {
        public Sprite TaijutsuSprite;
        public Sprite NinjutsuSprite;
        public Sprite GenjutsuSprite;
        public ColorBlock TaijutsuColors;
        public ColorBlock GenjutsuColors;
        public ColorBlock NinjutsuColors;
    }
}