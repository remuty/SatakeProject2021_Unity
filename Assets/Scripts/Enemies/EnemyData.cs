using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateEnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHp;
    public int atk;
    public int speed;
    public int point;
}
