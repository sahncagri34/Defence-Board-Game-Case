using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "DEFENCE BOARD GAME-CASE/GameData", order = 0)]
public class GameData : ScriptableObject
{
    private static GameData instance;
    public static GameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameData>("Scriptables/GameData");
            }
            return instance;
        }
    }

    [SerializeField] private List<LevelData> levels;
    [SerializeField] private List<EnemyData> enemies;
    [SerializeField] private List<DefencerData> defencers;

    public List<LevelData> Levels => levels;
    public List<EnemyData> Enemies => enemies;
    public List<DefencerData> Defencers => defencers;
    
    public LevelData GetLevelData(int levelIndex)
    {
        return Levels.Find(x => x.LevelNumber == levelIndex);
    }

    public EnemyData GetEnemyData(EnemyType enemyType)
    {
        var enemyData = enemies.Find(x => x.EnemyType == enemyType);
        return enemyData;
    }

    public DefencerData GetDefencerData(DefencerType defencerType)
    {
        var defencerData = defencers.Find(x => x.DefencerType == defencerType);
        return defencerData;
    }


    [ContextMenu("Find Levels")]
    private void FindLevels()
    {
        levels = new List<LevelData>();
        var levelDatas = Resources.LoadAll<LevelData>("Scriptables/Levels");
        foreach (var levelData in levelDatas)
        {
            levels.Add(levelData);
        }
    }
}