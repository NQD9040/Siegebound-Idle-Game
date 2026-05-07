using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Siegebound Idle/ProjectileData", order = 2)]

public class ProjectileScriptableObject : ScriptableObject
{
    [Header("Projectile Info")]
    public string projectileName = "New Projectile";
    public float speed = 10f;
    public float damage = 50f;
}