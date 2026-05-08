using UnityEngine;
using UnityEngine.InputSystem;

public enum TargetSearchMode
{
    CurrentPosition,
    SpawnPosition
}

public class PlayerController : MonoBehaviour
{
    public CharacterData playerData;

    [Header("Movement")]
    private float moveSpeed = 5f;
    public float baseMoveSpeed = 5f;

    [Header("Attack")]
    public float attackRange = 1f;
    public float baseAttackCooldown = 3f;
    private float attackCooldown = 3f;
    public LayerMask enemyLayer;

    [Header("Auto Attack")]
    public bool isAutoAttacking = false;
    public float autoMoveStopTime = 0.5f;

    [Header("Target Search")]
    public TargetSearchMode targetSearchMode =
        TargetSearchMode.CurrentPosition;

    [Header("Attack Range Visual")]
    public LineRenderer rangeRenderer;
    public int circleSegments = 50;

    private float attackTimer;
    private float stopTimer;

    private Animator animator;
    private Vector2 moveInput;

    private string[] attackAnimations = { "attack1", "attack2" };
    private int attackAnimIndex = 0;

    private EnemyController currentTarget;

    private Vector3 spawnPosition;

    void Start()
    {
        animator = GetComponent<Animator>();

        spawnPosition = transform.position;

        DrawAttackRange();
    }

    void Update()
    {
        animator.speed = playerData.attackSpeed;

        attackCooldown = baseAttackCooldown / playerData.attackSpeed;

        moveSpeed = baseMoveSpeed * playerData.attackSpeed;

        if (isAutoAttacking)
        {
            HandleAutoMovement();
        }
        else
        {
            HandleMovement();
        }

        HandleAttack();
    }

    void HandleMovement()
    {
        moveInput = Keyboard.current != null
            ? new Vector2(
                Keyboard.current.aKey.isPressed ? -1 :
                Keyboard.current.dKey.isPressed ? 1 : 0,

                Keyboard.current.sKey.isPressed ? -1 :
                Keyboard.current.wKey.isPressed ? 1 : 0
            )
            : Vector2.zero;
        
        MoveCharacter(moveInput);
    }

    void HandleAutoMovement()
    {
        stopTimer -= Time.deltaTime;

        if (stopTimer > 0)
        {
            animator.SetBool("isRunning", false);
            return;
        }

        if (currentTarget == null || !currentTarget.gameObject.activeInHierarchy)
        {
            currentTarget = FindNearestEnemy();
        }

        if (currentTarget == null)
        {
            float distanceToSpawn = Vector2.Distance(
                transform.position,
                spawnPosition
            );

            if (distanceToSpawn > 0.1f)
            {
                Vector2 backDirection =
                (
                    spawnPosition
                    - transform.position
                ).normalized;

                MoveCharacter(backDirection);
            }
            else
            {
                MoveCharacter(Vector2.zero);
                transform.localScale = new Vector3(1, 1, 1);
            }

            return;
        }

        float distance = Vector2.Distance(
            transform.position,
            currentTarget.transform.position
        );

        if (distance <= attackRange)
        {
            animator.SetBool("isRunning", false);

            stopTimer = autoMoveStopTime;

            return;
        }

        Vector2 direction =
            (
                currentTarget.transform.position
                - transform.position
            ).normalized;

        MoveCharacter(direction);
    }

    void MoveCharacter(Vector2 direction)
    {
        Vector3 move =
            new Vector3(direction.x, direction.y, 0)
            * moveSpeed
            * Time.deltaTime;

        transform.Translate(move, Space.World);

        if (direction != Vector2.zero)
        {
            animator.SetBool("isRunning", true);

            if (direction.x != 0)
            {
                transform.localScale = new Vector3(
                    direction.x < 0 ? -1 : 1,
                    1,
                    1
                );
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void HandleAttack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer > 0)
            return;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            transform.position,
            attackRange,
            enemyLayer
        );

        if (enemies.Length > 0)
        {
            string attackAnim = attackAnimations[attackAnimIndex];

            attackAnimIndex =
                (attackAnimIndex + 1) % attackAnimations.Length;

            animator.SetTrigger(attackAnim);

            attackTimer = attackCooldown;
        }
    }

    public void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            transform.position,
            attackRange,
            enemyLayer
        );

        foreach (Collider2D enemy in enemies)
        {
            EnemyController enemyController =
                enemy.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                float damage = playerData.baseAttack;

                if (Random.value < playerData.critRate)
                {
                    damage *= 1 + playerData.critDmg;
                }

                enemyController.TakeDamage(damage);
            }
        }
    }

    EnemyController FindNearestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            transform.position,
            999f,
            enemyLayer
        );

        EnemyController nearestEnemy = null;

        float nearestDistance = Mathf.Infinity;

        Vector3 searchOrigin =
            targetSearchMode == TargetSearchMode.SpawnPosition
            ? spawnPosition
            : transform.position;

        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(
                searchOrigin,
                enemy.transform.position
            );

            if (distance < nearestDistance)
            {
                nearestDistance = distance;

                nearestEnemy = enemy.GetComponent<EnemyController>();
            }
        }

        return nearestEnemy;
    }

    void DrawAttackRange()
    {
        if (rangeRenderer == null)
            return;

        rangeRenderer.positionCount = circleSegments + 1;

        float angle = 0f;

        for (int i = 0; i <= circleSegments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * attackRange;

            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * attackRange;

            rangeRenderer.SetPosition(i, new Vector3(x, y, 0));

            angle += 360f / circleSegments;
        }
    }
}