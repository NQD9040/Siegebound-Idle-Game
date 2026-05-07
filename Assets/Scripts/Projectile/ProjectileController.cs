using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public ProjectileScriptableObject data;

    private Transform target;

    public float lifeTime = 5f;

    [System.Obsolete]
    void Start()
    {
        FindNearestEnemy();
    }

    void Update()
    {
        // Nếu target còn sống -> bay theo target
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.position,
                data.speed * Time.deltaTime
            );
        }
        else
        {
            // Nếu target chết -> bay tiếp theo hướng hiện tại
            transform.Translate(Vector2.right * data.speed * Time.deltaTime);
        }

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
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
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();

        if (enemies.Length == 0)
        {
            target = null;
            return;
        }

        float closestDistance = Mathf.Infinity;
        EnemyController closestEnemy = null;

        foreach (EnemyController enemy in enemies)
        {
            float distance = Vector2.Distance(
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

            // Xoay đầu đạn theo hướng target
            Vector2 direction = (target.position - transform.position).normalized;
            transform.right = direction;
        }
    }
}