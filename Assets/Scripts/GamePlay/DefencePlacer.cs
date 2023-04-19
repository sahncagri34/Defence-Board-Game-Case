using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefencePlacer : MonoBehaviour
{
    public static event Action<bool> OnDefencePlacerStatusChanged;

    [SerializeField] private Defencer defencePrefab;

    private Camera mainCamera;


    private bool _isPlacingDefence = false;
    public bool IsPlacingDefence
    {
        get => _isPlacingDefence;
        set
        {
            OnDefencePlacerStatusChanged?.Invoke(value);
            currentDefencerData = null;
            _isPlacingDefence = value;
        }
    }

    private GamePanel gamePanel;
    private DefencerData currentDefencerData;

    private void Awake() {
        mainCamera = Camera.main;
    }
    private void Start()
    {
        gamePanel = UIManager.Instance.GetPanel<GamePanel>();

        gamePanel.OnDefencePlaceHolderSelected += GamePanel_OnDefencePlaceHolderSelected;
    }
    private void Update()
    {
        if (!IsPlacingDefence)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 placePosition =  Tools.GetClosestPositionForPlacing(mousePosition);
            PlaceDefence(placePosition);
            IsPlacingDefence = false;
        }
    }

    private void PlaceDefence(Vector3 mousePosition)
    {
        if (currentDefencerData == null)
            return;

        var defenceInstance = Instantiate(defencePrefab, mousePosition, Quaternion.identity);

        defenceInstance.Set(currentDefencerData);
    }

    private void GamePanel_OnDefencePlaceHolderSelected(int defencerIndex)
    {
        IsPlacingDefence = true;
        currentDefencerData = GameData.Instance.GetDefencerData((DefencerType)defencerIndex);
    }
    private void OnDestroy()
    {
        gamePanel.OnDefencePlaceHolderSelected -= GamePanel_OnDefencePlaceHolderSelected;
    }
}
