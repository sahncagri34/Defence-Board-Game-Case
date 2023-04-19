using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "DEFENCE BOARD GAME-CASE/LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public int LevelNumber;
    public float SpawnInterval;
    public List<DefencerLevelInfo> Defencers;
    public List<EnemyLevelInfo> Enemies;

    public int GetEnemyCount()
    {
        int enemyCount = 0;
        foreach (var enemy in Enemies)
        {
            enemyCount += enemy.EnemyCount;
        }
        return enemyCount;
    }

}

[System.Serializable]
public class EnemyLevelInfo
{
    public EnemyType EnemyType;
    public int EnemyCount;

    public EnemyLevelInfo(EnemyType enemyType, int enemyCount)
    {
        EnemyType = enemyType;
        EnemyCount = enemyCount;
    }
}

[System.Serializable]
public class DefencerLevelInfo
{
    public DefencerType DefencerType;
    public int DefencerCount;

    public DefencerLevelInfo(DefencerType defencerType, int defencerCount)
    {
        DefencerType = defencerType;
        DefencerCount = defencerCount;
    }
}