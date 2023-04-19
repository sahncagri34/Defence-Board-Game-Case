using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelItem : MonoBehaviour
{
    [SerializeField] private Button selectedButton;
    [SerializeField] private Text levelNameText;

    private int levelNumber;

    public event Action<int> OnSelected;
    public event Action<UILevelItem> OnDestroyed;

    public void Init(int levelNumber)
    {
        this.levelNumber = levelNumber;
        levelNameText.text = levelNumber.ToString();
        selectedButton.onClick.AddListener(LevelSelected);
    }

    private void LevelSelected()
    {
        OnSelected?.Invoke(levelNumber);
    }
    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
