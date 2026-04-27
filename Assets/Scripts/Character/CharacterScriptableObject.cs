using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Siegebound Idle/CharacterData", order = 0)]
public class CharacterData : ScriptableObject 
{
    [Header("Character Info")]
    public string charName = "New Character";
    public int level = 1;
    public float exp = 0f;
    public float baseHP = 1000f;
    public float baseAttack = 100f;
    public float baseDEF = 50f;
    public float critRate = 0.05f;
    public float critDmg = 0.5f;
    public enum WeaponType { Sword, Bow, Catalyst, Polearm, Claymore }
    public WeaponType weaponType;
    public enum Element { Pyro, Hydro, Electro, Cryo }
    public Element element;
    [Header("Level Up Info")]
    public float expToNextLevel = 100f;
    public float hpPerLevel = 100f;
    public float attackPerLevel = 10f;
    public float defPerLevel = 5f;
    public int[] maxLevel = new int[7] { 20, 40, 50, 60, 70, 80, 90 };
    public float expMultiplier = 1.07f; // multiplier for exp required to level up
    public int levelRank = 1; // based on maxLevel array, determines the current rank of the character
    [Header("Character Object")]
    public GameObject characterPrefab;
}