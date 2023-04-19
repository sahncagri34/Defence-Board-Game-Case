using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    [SerializeField] private Button readyButton;
    [SerializeField] private List<Button> defencerButtons;
    
    public event Action<LevelData> OnGameReady;
    public event Action<int> OnDefencePlaceHolderSelected;

    private LevelData currentLevelData;

    private void Awake() => Initilize();
    void Initilize()
    {
        readyButton.onClick.AddListener(StartGame);
        for (int i = 0; i < defencerButtons.Count; i++)
        {
            int index = i;
            defencerButtons[i].onClick.AddListener(() => OnDefencerButtonClicked(index));
        }

        DefencePlacer.OnDefencePlacerStatusChanged += DefencePlacer_OnDefencePlacerStatusChanged;
    }

    private void OnDefencerButtonClicked(int defencerIndex)
    {
        defencerIndex++;
        DecreaseDefencerCount(defencerIndex);
        ToggleDefencerButtons();
        OnDefencePlaceHolderSelected?.Invoke((defencerIndex));
    }

    void ToggleDefencerButtons()
    {
        for (int i = 0; i < defencerButtons.Count; i++)
        {
            defencerButtons[i].interactable = CheckDefencerUsageCount(i);
        }
    }

    private void StartGame()
    {
        OnGameReady?.Invoke(currentLevelData);
        ToggleDefencerButtons();
        readyButton.gameObject.SetActive(false);
    }

    public void SetLevelData(LevelData levelData)
    {
        currentLevelData = levelData;
        SetLevelProperties(levelData);
    }

    private void SetLevelProperties(LevelData levelData)
    {
       Tools.SetLevelProperties(levelData);
    }

    private bool CheckDefencerUsageCount(int defencerIndex)
    {
        return Tools.CheckDefencerUsageCount(defencerIndex);
    }
    private void DecreaseDefencerCount(int defencerIndex)
    {
        Tools.DecreaseDefencerCount(defencerIndex);
    }

    private void DefencePlacer_OnDefencePlacerStatusChanged(bool isPlacingDefence)
    {
        ToggleDefencerButtons();
    }
    private void OnDestroy() {
        DefencePlacer.OnDefencePlacerStatusChanged -= DefencePlacer_OnDefencePlacerStatusChanged;
    }
}
