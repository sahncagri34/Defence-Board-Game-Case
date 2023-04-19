using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelSelectionPanel : BasePanel
{
    [SerializeField] private UILevelItem levelItemPrefab;
    [SerializeField] private Transform levelItemParent;

    private List<LevelData> levels = new List<LevelData>();

    private void Start()
    {
        SetLevels();
    }

    private void SetLevels()
    {
        levels = GameData.Instance.Levels;

        foreach (var level in levels)
        {
            var levelItem = Instantiate(levelItemPrefab, levelItemParent);
            levelItem.Init(level.LevelNumber);
            levelItem.OnSelected += OnLevelItemSelected;
            levelItem.OnDestroyed += OnLevelItemDestroyed;
        }
    }

    private void OnLevelItemSelected(int LevelNumber)
    {
        var gamePanel = UIManager.Instance.PushPanel<GamePanel>();

        var levelData = GameData.Instance.GetLevelData(LevelNumber);

        gamePanel.SetLevelData(levelData);
    }

    private void OnLevelItemDestroyed(UILevelItem levelItem)
    {
        levelItem.OnSelected -= OnLevelItemSelected;
        levelItem.OnDestroyed -= OnLevelItemDestroyed;
    }

}
