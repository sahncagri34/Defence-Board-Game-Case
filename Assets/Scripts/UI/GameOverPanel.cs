using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    [SerializeField] private Text scoreText;
    public void SetScore(float score)
    {
        scoreText.text = string.Format("Score: {0}s", score);
    }
}
