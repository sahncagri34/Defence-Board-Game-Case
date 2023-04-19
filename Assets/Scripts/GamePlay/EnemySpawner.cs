using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform enemySpawnPoint;

    private List<Enemy> spawnedEnemies = new List<Enemy>();
    private LevelData currentLevelData;
    private GamePanel gamePanel;
    private bool isGameFinished = false;
    private int destroyedEnemyCount = 0;


    private void Awake() {
        EventsHandler.OnGameFinished += EventsHandler_OnGameFinished;
    }
    private void Start()
    {
        gamePanel = UIManager.Instance.GetPanel<GamePanel>();
        gamePanel.OnGameReady += GamePanel_OnGameReady;
    }

    private async void GamePanel_OnGameReady(LevelData levelData)
    {
        gamePanel.OnGameReady -= GamePanel_OnGameReady;   
        currentLevelData = levelData;

        await Wave();
    }

    private async Task Wave()
    {
        foreach (var enemyData in currentLevelData.Enemies)
        {
            for (int i = 0; i < enemyData.EnemyCount; i++)
            {
                if (isGameFinished)
                {
                   return;
                }
                var enemy = GetEnemy(enemyData.EnemyType);
                var enemyInternalData = GameData.Instance.GetEnemyData(enemyData.EnemyType);
                Vector3 spawnPosition = Tools.GetRandomSpawnPositionForEnemy(enemySpawnPoint);
                enemy.Set(enemyInternalData,spawnPosition);

                await Task.Delay((int)currentLevelData.SpawnInterval * 1000);
            }
        }
    }

    private Enemy GetEnemy(EnemyType enemyType)
    {
        var enemy =  spawnedEnemies.Find(x => x.Compare(enemyType) && !x.gameObject.activeSelf);
        if (enemy == null)
        {
            enemy = SpawnEnemy();
        }
        enemy.OnDestroyed += Enemy_OnDestroyed;
        return enemy;
    }

    private Enemy SpawnEnemy()
    {
        var enemyInstance = Instantiate(enemyPrefab);
        spawnedEnemies.Add(enemyInstance);
        
        return enemyInstance;
    }

    private void EventsHandler_OnGameFinished()
    {
        isGameFinished = true;
    }
    private void Enemy_OnDestroyed(Enemy enemy)
    {
        enemy.OnDestroyed -= Enemy_OnDestroyed;
        destroyedEnemyCount++;
        CheckAnyEnemyLeft();
    }
    void CheckAnyEnemyLeft()
    {
        if (destroyedEnemyCount == currentLevelData.GetEnemyCount())
        {
            EventsHandler.InvokeOnGameFinished();
        }
    }
    private void OnDestroy() {
        EventsHandler.OnGameFinished -= EventsHandler_OnGameFinished;
    }
}
