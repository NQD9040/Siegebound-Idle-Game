using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Siegebound Idle/SkillData", order = 0)]
public class SkillData : ScriptableObject
{
    public enum TargetPattern
    {
        Single,
        Row,
        Column,
        All,
        Cross,
        Random
    }
    public enum TargetSide
    {
        Ally,
        Enemy,
        Self
    }
    public enum EffectType
    {
        Damage,
        Heal,
        Burn,
        Stun,
        Shield,
        BuffAttack
    }
    public enum StatScaling
    {
        Attack,
        DEF,
        HP
    }
    [Header("Skill Info")]
    public string skillName = "New Skill";
    public string skillDescription = "";
    public TargetPattern targetPattern;
    public TargetSide targetSide;
    public EffectType effectType;
    public StatScaling statScaling;
    public float skillScale = 1f;
    public float skillBonus = 0f; // in case we want to add a flat bonus to the skill's effect
    public int energyCost = 0;
    public float effectChance = 1f; // chance for the skill's effect to apply (for non-damage effects)
    public bool isElementalInfused = false;
}