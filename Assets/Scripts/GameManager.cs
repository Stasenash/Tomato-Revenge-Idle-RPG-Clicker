using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ClickButtonManager _clickButtonManager;
    [SerializeField] private EnemyManager _enemyManager;
    private void Awake()
    {
        _clickButtonManager.Inizialize();
        _enemyManager.Initialize();
        
        _clickButtonManager.OnClicked += () => _enemyManager.DamageCurrentEnemy(1f);
    }
}
