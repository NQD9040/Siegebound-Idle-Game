using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyScriptableObject data;
    private float currentHP;
    private float currentSpeed;
    private Animator animator;
    private bool isElite = false;
    private GameObject hpBar;
    public int baseCost = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        hpBar = transform.GetChild(0).gameObject;
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
        // Update HP bar
        float hpRatio = currentHP / (isElite ? data.baseHP * 4 : data.baseHP);
        hpBar.transform.localScale = new Vector3(hpRatio, 0.1f, 1);
    }
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            if (isElite)
            {
                UpgradeManager.instance.AddGold(baseCost * 4); // Elite enemies give more gold
            }
            else
            {
                UpgradeManager.instance.AddGold(baseCost); // Add gold when the enemy is defeated
            }
            Destroy(gameObject); // Destroy the enemy when HP drops to 0 or below
        }
    }
    public void changeToElite()
    {
        isElite = true;
    }
    public float getSpeed()
    {
        return currentSpeed;
    }
    public Vector2 GetMoveDirection()
    {
        return transform.localScale.x > 0
            ? Vector2.right
            : Vector2.left;
    }
}