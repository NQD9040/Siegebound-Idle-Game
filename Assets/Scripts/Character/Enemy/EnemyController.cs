using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyScriptableObject data;
    private float currentHP;
    private float currentSpeed;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHP = data.baseHP;
        currentSpeed = data.speed;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isRunning", true);
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
    }
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Destroy(gameObject); // Destroy the enemy when HP drops to 0 or below
        }
    }
}