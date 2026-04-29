using System;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactController : MonoBehaviour
{
    public List<ArtifactInstance> inventory = new();

    public ArtifactInstance CreateArtifact(ArtifactData data)
    {
        ArtifactInstance artifact = new ArtifactInstance();
        artifact.uniqueId = Guid.NewGuid().ToString();
        artifact.artifactData = data;
        artifact.currentLevel = 0;

        // Roll main stat
        StatsData mainStat = data.possibleMainStats[
            UnityEngine.Random.Range(0, data.possibleMainStats.Count)
        ];

        artifact.mainStatType = mainStat.statsType;
        artifact.mainStatValue = mainStat.GetValue(data.artifactStar, 0);

        // Roll 4 substats
        for (int i = 0; i < 4; i++)
        {
            StatsData sub = data.possibleSubStats[
                UnityEngine.Random.Range(0, data.possibleSubStats.Count)
            ];

            float value = sub.baseValue[data.artifactStar - 1];
            artifact.subStats.Add(new ArtifactSubStat(sub.statsType, value));
        }

        inventory.Add(artifact);
        return artifact;
    }

    public void UpgradeArtifact(ArtifactInstance artifact)
    {
        if (artifact.currentLevel >= artifact.artifactData.GetMaxLevel())
            return;

        artifact.currentLevel++;

        foreach (var stat in artifact.artifactData.possibleMainStats)
        {
            if (stat.statsType == artifact.mainStatType)
            {
                artifact.mainStatValue = stat.GetValue(
                    artifact.artifactData.artifactStar,
                    artifact.currentLevel
                );
                break;
            }
        }
    }
}