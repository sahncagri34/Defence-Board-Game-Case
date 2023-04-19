using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform initialSpawnPoint;
    private GamePanel gamePanel;
    private Stopwatch stopWatch = new Stopwatch();

    private void Awake() => Tools.SetGridPositions(initialSpawnPoint.position);
    private void Start()
    {
        gamePanel = UIManager.Instance.GetPanel<GamePanel>();

        EventsHandler.OnGameFinished += EventsHandler_OnGameFinished;
        gamePanel.OnGameReady += GamePanel_OnGameReady;
    }
    private void OnDestroy()
    {
        EventsHandler.OnGameFinished -= EventsHandler_OnGameFinished;
        gamePanel.OnGameReady -= GamePanel_OnGameReady;
    }

    private void EventsHandler_OnGameFinished()
    {
        stopWatch.Stop();
        var gameOverPanel = UIManager.Instance.PushPanel<GameOverPanel>();
        gameOverPanel.SetScore(stopWatch.ElapsedTicks / Stopwatch.Frequency);
    }

    private void GamePanel_OnGameReady(LevelData levelData)
    {
        stopWatch.Start();
    }
}
