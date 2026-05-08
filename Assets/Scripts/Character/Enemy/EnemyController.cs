using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyScriptableObject data;
    private float currentHP;
    private float currentSpeed;
    private Animator animator;
    private bool isElite = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        if (isElite)
        {
            transform.localScale *= 2f; // Elite enemies are 50% larger
            currentHP = data.baseHP * 4; // Elite enemies have four times the HP
            currentSpeed = data.speed * 0.5f; // Elite enemies are 50% slower
            animator.speed = 0.5f; // Decrease the animation speed to match the new movement speed
        }
        else
        {
            currentHP = data.baseHP;
            currentSpeed = data.speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isRunning", true);
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
        if (transform.position.x < -10) // Destroy the enemy if it goes off-screen
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Destroy(gameObject); // Destroy the enemy when HP drops to 0 or below
        }
    }
    public void changeToElite()
    {
        isElite = true;
    }
}