using UnityEngine;
using System.Collections;

public class CharacterSkillController : MonoBehaviour
{
    public SkillData normalAttack;
    public SkillData elementalSkill;
    public SkillData elementalBurst;
    private CharacterController characterController; // reference to the character's main controller
    private CharacterController[] targets; // reference to the current targets of the skill
    void Start()
    {
        characterController = GetComponent<CharacterController>(); // get the character controller component
    }
    public void SetTarget(CharacterController[] targets)
    {
        this.targets = targets;
    }
    public void UseSkill(int skillIndex)
    {
        SkillData skill = null;

        switch (skillIndex)
        {
            case 0:
                skill = normalAttack;
                break;
            case 1:
                skill = elementalSkill;
                break;
            case 2:
                skill = elementalBurst;
                break;
            default:
                Debug.LogError("Invalid skill index!");
                return;
        }

        // Here you would implement the logic to apply the skill's effect based on its properties
        Debug.Log($"Using skill: {skill.skillName}");
    }
}