using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactData", menuName = "Siegebound Idle/Artifact/ArtifactData")]
public class ArtifactData : ScriptableObject
{
    [Header("Artifact Info")]
    public string artifactName;
    public ArtifactType artifactType;
    public int artifactStar = 1;

    [Header("Stat Pools")]
    public List<StatsData> possibleMainStats;
    public List<StatsData> possibleSubStats;

    [Header("Visual")]
    public GameObject artifactPrefab;
    public Sprite icon;

    public int GetMaxLevel()
    {
        switch (artifactStar)
        {
            case 1: return 4;
            case 2: return 4;
            case 3: return 12;
            case 4: return 16;
            case 5: return 20;
            default: return 4;
        }
    }
}