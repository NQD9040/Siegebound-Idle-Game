using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Siegebound Idle/CharacterData", order = 0)]
public class CharacterData : ScriptableObject 
{
    [Header("Character Info")]
    public string charName = "Main Character";
    public float baseAttack = 50f;
    public float critRate = 0f;
    public float critDmg = 0.5f;
    public float attackSpeed = 1f;
    [Header("Upgrade Levels")]
    public int attackLevel = 1;
    public int speedLevel = 1;
    [Header("Upgrade Costs")]
    public float attackUpgradeCost = 1;
    public float speedUpgradeCost = 1;
    public float costMultiplier = 2f;
    [Header("Upgrade Increment")]
    public float attackUpgradeIncrement = 50f;
    public float speedUpgradeIncrement = 0.05f;
}