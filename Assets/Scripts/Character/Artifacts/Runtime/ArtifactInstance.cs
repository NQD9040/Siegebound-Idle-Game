using System.Collections.Generic;

[System.Serializable]
public class ArtifactInstance
{
    public string uniqueId;
    public ArtifactData artifactData;

    public int currentLevel;
    public StatsType mainStatType;
    public float mainStatValue;

    public List<ArtifactSubStat> subStats = new();

    public bool isLocked;
    public bool isEquipped;
}