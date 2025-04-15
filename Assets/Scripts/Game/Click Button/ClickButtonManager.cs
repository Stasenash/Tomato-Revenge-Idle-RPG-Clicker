using DG.Tweening;
using Game.Skills;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Click_Button
{
    public class ClickButtonManager : MonoBehaviour
    {
        [SerializeField] private ClickButton _taijutsuButton;
        [SerializeField] private ClickButton _ninjutsuButton;
        [SerializeField] private ClickButton _genjutsuButton;
        
        [SerializeField] private ClickButtonConfig _buttonConfig;
        
        public void Inizialize(SkillSystem skillSystem)
        {
            _taijutsuButton.Inizialize(_buttonConfig.TaijutsuSprite, _buttonConfig.TaijutsuColors);
            _ninjutsuButton.Inizialize(_buttonConfig.NinjutsuSprite, _buttonConfig.NinjutsuColors);
            _genjutsuButton.Inizialize(_buttonConfig.GenjutsuSprite, _buttonConfig.GenjutsuColors);
        
            _taijutsuButton.SubscribeOnClick(()=>
            {
                skillSystem.InvokeTrigger(SkillTrigger.OnTaijutsu);
            });
            _ninjutsuButton.SubscribeOnClick(()=>skillSystem.InvokeTrigger(SkillTrigger.OnNinjutsu));
            _genjutsuButton.SubscribeOnClick(()=>skillSystem.InvokeTrigger(SkillTrigger.OnGenjutsu));
            
            //_clickButton.SubscribeOnClick(AnimateClick);
        }

        //TODO: придумать для анимаций
        private void AnimateClick()
        {    
            gameObject.transform.DORotate(new Vector3(0, 0, -360), 0.1f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear); // Линейное вращение без ускорения/замедления
        }
    }
}