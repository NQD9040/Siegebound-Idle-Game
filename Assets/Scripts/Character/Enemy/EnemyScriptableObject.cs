using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Siegebound Idle/EnemyData", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Enemy Info")]
    public string enemyName = "New Enemy";
    public float baseHP = 500f;
    public float baseAttack = 50f;
    public float speed = 2f;
    public enum EnemyType { Melee, Ranged, Magic }
    public EnemyType enemyType;
}