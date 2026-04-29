[System.Serializable]
public class ArtifactSubStat
{
    public StatsType statType;
    public float value;

    public ArtifactSubStat(StatsType statType, float value)
    {
        this.statType = statType;
        this.value = value;
    }
}