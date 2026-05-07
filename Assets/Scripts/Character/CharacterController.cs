using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public CharacterData data;
    public GameObject projectilePrefab;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        animator.speed = data.attackSpeed;
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        if (enemies.Length > 0) 
        {
            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
        }
    }
    [System.Obsolete]
    void Attack()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}
