using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public CharacterController character;
    public EnemyController enemy;

    public float enemySpawnPointX = 10f;
    public Vector2 enemySpawnPointY = new Vector2(-4f, 4f);

    public float spawnRate = 2f;
    public float eliteSpawnChance = 0.1f;
    private GameObject cursorPrefab;
    public Vector2 fixedCursorOffset = new Vector2(0.5f, 0.5f);
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnRate);
        cursorPrefab = GameObject.Find("Cursor");
        Cursor.visible = false;
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            UpgradeManager.instance.ToggleUpgradePanel();
        }

        UpdateCursor();
    }

    void UpdateCursor()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue() + fixedCursorOffset;

        cursorPrefab.transform.position = mousePos;
    }

    void SpawnEnemy()
    {
        float spawnY = Random.Range(enemySpawnPointY.x, enemySpawnPointY.y);

        Vector2 spawnPosition = new Vector2(enemySpawnPointX, spawnY);

        EnemyController spawnedEnemy =
            Instantiate(enemy, spawnPosition, Quaternion.identity);

        if (Random.value < eliteSpawnChance)
        {
            spawnedEnemy.changeToElite();
        }
    }
}