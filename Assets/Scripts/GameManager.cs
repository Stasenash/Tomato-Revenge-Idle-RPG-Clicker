using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ClickButtonController _clickButton;
    [SerializeField] private Animator _shurikenAnimator;

    private void Awake()
    {
        _clickButton.SubscribeOnClick(AnimateClick);
    }

    private void ShowClick()
    {
        Debug.Log("clicked!");
    }
    
    private void AnimateClick()
    {
        _shurikenAnimator.SetTrigger("ShurikenAnimation");
    }
}
