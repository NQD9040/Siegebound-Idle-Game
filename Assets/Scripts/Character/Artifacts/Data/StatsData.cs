using UnityEngine;

[CreateAssetMenu(fileName = "StatsData", menuName = "Siegebound Idle/Artifact/StatsData")]
public class StatsData : ScriptableObject
{
    public StatsType statsType;

    [Tooltip("Base value for star 1 -> 5")]
    public float[] baseValue = new float[5];

    [Tooltip("Bonus per level for star 1 -> 5")]
    public float[] bonusValuePerLevel = new float[5];

    public float GetValue(int star, int level)
    {
        int index = Mathf.Clamp(star - 1, 0, 4);
        return baseValue[index] + (bonusValuePerLevel[index] * level);
    }
}