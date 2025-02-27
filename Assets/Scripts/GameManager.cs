using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ClickButtonController _clickButton;

    private void Awake()
    {
        _clickButton.SubscribeOnClick(ShowClick);
    }

    private void ShowClick()
    {
        Debug.Log("clicked!");
    }
}
