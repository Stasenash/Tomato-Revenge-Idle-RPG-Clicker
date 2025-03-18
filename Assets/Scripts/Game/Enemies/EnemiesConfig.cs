using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Configs/EnemiesConfig",fileName = "EnemiesConfig")]
public class EnemiesConfig : ScriptableObject
{
    public Enemy EnemyPrefab;
    public List<EnemyData> Enemies;
}