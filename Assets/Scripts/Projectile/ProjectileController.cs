using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public ProjectileScriptableObject data;

    private Transform target;

    [Header("Lifetime")]
    public float lifeTime = 5f;

    [Header("Arc Settings")]
    public float arcHeight = 2f;
    public float travelTime = 0.5f;

    [Header("Prediction Settings")]
    [Range(0f, 1f)]
    public float predictStrength = 0.5f;

    [Header("No Target Settings")]
    public float noTargetDistance = 5f;

    private Vector2 startPoint;
    private Vector2 controlPoint;
    private Vector2 endPoint;

    private float timer;
    public GameObject character;
    [System.Obsolete]
    void Start()
    {
        CharacterController script = character.GetComponent<CharacterController>();
        UpdateData(script);
        FindNearestEnemy();

        startPoint = transform.position;

        // ===== Có target =====
        if (target != null)
        {
            EnemyController enemyController =
                target.GetComponent<EnemyController>();

            Vector2 predictedPosition =
                (Vector2)target.position;

            // Predict nhẹ vị trí tương lai
            if (enemyController != null)
            {
                Vector2 moveDirection =
                    enemyController.GetMoveDirection();

                predictedPosition +=
                    moveDirection *
                    enemyController.getSpeed() *
                    travelTime *
                    predictStrength;
            }

            endPoint = predictedPosition;
        }
        // ===== Không có target =====
        else
        {
            endPoint =
                startPoint +
                (Vector2)transform.right *
                Random.Range(
                    noTargetDistance - 1f,
                    noTargetDistance + 1f
                );
        }

        // ===== Tạo vòng cung =====

        Vector2 midPoint =
            (startPoint + endPoint) / 2f;

        controlPoint =
            midPoint +
            Vector2.up * arcHeight;
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
            return;
        }

        timer += Time.deltaTime;

        float t = timer / travelTime;

        t = Mathf.Clamp01(t);

        // ===== Bezier Curve =====

        Vector2 p1 =
            Vector2.Lerp(startPoint, controlPoint, t);

        Vector2 p2 =
            Vector2.Lerp(controlPoint, endPoint, t);

        Vector2 curvePosition =
            Vector2.Lerp(p1, p2, t);

        transform.position = curvePosition;

        // ===== Rotate Arrow =====

        Vector2 direction = p2 - p1;

        if (direction != Vector2.zero)
        {
            transform.right = direction;
        }

        // ===== Tới điểm cuối =====

        if (t >= 1f)
        {
            Destroy(gameObject, 0.05f);
        }
    }

    void UpdateData(CharacterController controller)
    {
        // Cập nhật dữ liệu từ CharacterData
        data.damage = controller.data.baseAttack;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemyController =
                collision.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.TakeDamage(data.damage);
            }

            Destroy(gameObject);
        }
    }

    [System.Obsolete]
    void FindNearestEnemy()
    {
        EnemyController[] enemies =
            FindObjectsOfType<EnemyController>();

        if (enemies.Length == 0)
        {
            target = null;
            return;
        }

        float closestDistance = Mathf.Infinity;

        EnemyController closestEnemy = null;

        foreach (EnemyController enemy in enemies)
        {
            float distance =
                Vector2.Distance(
                    transform.position,
                    enemy.transform.position
                );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy.transform;
        }
    }
}