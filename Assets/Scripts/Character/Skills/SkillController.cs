using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class SkillController : MonoBehaviour
{
    public SkillData skill;
    private CharacterController characterController; // reference to the character's main controller
    private CharacterController[] targets;
    public void UseSkill(CharacterController characterController, CharacterController[] targets)
    {
        this.characterController = characterController;
        this.targets = targets;
    }
    #region Skill Targeting Methods
    public void AttackSingleTarget(CharacterController mainTarget)
    {
        if (mainTarget == null)
        {
            Debug.LogError("No target selected for single target skill!");
            return;
        }
        
    }
    public void AttackRow(CharacterController mainTarget, CharacterController[] rowTargets)
    {
        // Implement logic to apply skill effect to a row of targets
    }
    public void AttackColumn(CharacterController mainTarget, CharacterController[] columnTargets)
    {
        // Implement logic to apply skill effect to a column of targets
    }
    public void AttackAll(CharacterController mainTarget, CharacterController[] targets)
    {
        // Implement logic to apply skill effect to all targets
    }
    public void AttackCross(CharacterController mainTarget, CharacterController[] crossTargets)
    {
        // Implement logic to apply skill effect in a cross pattern
    }
    #endregion
}