using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterController character;
    public EnemyController enemy;
    public float enemySpawnPointX = 10f;
    public Vector2 enemySpawnPointY = new Vector2(-4f, 4f);
    public float spawnRate = 2f;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnRate);
    }

    void Update()
    {

    }
    void SpawnEnemy()
    {
        float spawnX = enemySpawnPointX;
        float spawnY = Random.Range(enemySpawnPointY.x, enemySpawnPointY.y);
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
}