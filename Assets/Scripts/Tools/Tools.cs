using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Tools
{
    public static List<DefencerLevelInfo> Defencers;
    public static List<EnemyLevelInfo> Enemies;
    public static readonly Vector3[,] GridPositions = new Vector3[8, 4];

    public const float DefaultEventInterval = 1f;

    #region Grid functions

    public static void SetGridPositions(Vector3 originPosition = default(Vector3))
    {
        int horizontalCount = GridPositions.GetLength(0);
        int verticalCount = GridPositions.GetLength(1);

        for (int i = 0; i < horizontalCount; i++)
        {
            for (int j = 0; j < verticalCount; j++)
            {
                GridPositions[i, j] = originPosition + new Vector3(i, j, 0);
            }
        }
    }

    public static Vector3 GetRandomSpawnPositionForEnemy(Transform spawnPoint)
    {
        int verticalCount = GridPositions.GetLength(1);
        int randomIndexOfX = Random.Range(0, verticalCount);
        float spawnX = GridPositions[randomIndexOfX, 0].x;
        return new Vector3(spawnX, spawnPoint.position.y, spawnPoint.position.z);
    }

    public static Vector3 GetClosestPositionForPlacing(Vector3 mousePosition)
    {
        int horizontalCount = GridPositions.GetLength(0);
        int verticalCount = GridPositions.GetLength(1);

        Vector3 closestPosition = Vector3.zero;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < horizontalCount / 2; i++)
        {
            for (int j = 0; j < verticalCount; j++)
            {
                float distance = Vector3.Distance(mousePosition, GridPositions[i, j]);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPosition = GridPositions[i, j];
                }
            }
        }

        return closestPosition;
    }
    #endregion
    #region Level Data Functions

    public static void SetLevelProperties(LevelData levelData)
    {
        Defencers = new List<DefencerLevelInfo>();
        Enemies = new List<EnemyLevelInfo>();

        foreach (var defencer in levelData.Defencers)
        {
            Defencers.Add(new DefencerLevelInfo(defencer.DefencerType, defencer.DefencerCount));
        }
        foreach (var enemy in levelData.Enemies)
        {
            Enemies.Add(new EnemyLevelInfo(enemy.EnemyType, enemy.EnemyCount));
        }
    }

    public static void DecreaseDefencerCount(int defencerIndex)
    {
        var defencerLevelInfo = Defencers.Find(x => x.DefencerType == (DefencerType)defencerIndex);
        if (defencerLevelInfo == null)
            return;

        defencerLevelInfo.DefencerCount--;
    }
    public static bool CheckDefencerUsageCount(int defencerIndex)
    {
        if (Defencers.Count == 0)
            return false;

        defencerIndex++;
        var defencerLevelInfo = Defencers.Find(x => x.DefencerType == (DefencerType)defencerIndex);
        if (defencerLevelInfo == null)
            return false;

        return defencerLevelInfo.DefencerCount > 0;
    }
    #endregion
}
