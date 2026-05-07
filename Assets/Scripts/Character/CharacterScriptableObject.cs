using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Siegebound Idle/CharacterData", order = 0)]
public class CharacterData : ScriptableObject 
{
    [Header("Character Info")]
    public string charName = "Main Character";
    public int level = 1;
    public float exp = 0f;
    public float baseHP = 1000f;
    public float baseAttack = 100f;
    public float baseDEF = 50f;
    public float critRate = 0f;
    public float critDmg = 0.5f;
    public float attackSpeed = 1f;
}