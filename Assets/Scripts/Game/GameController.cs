using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private ClickHandler _clickHandler;

    [SerializeField] private UIScreenManager _uiScreenManager;

    [SerializeField] private ScoreSaver _scoreSaver;

    [SerializeField] private BalloonSpawner _balloonSpawner;
    [SerializeField] private BalloonTerminator _balloonTerminator;

    [SerializeField] private GameSettings _gameSettings;

    private ScoreHandler _scoreHandler;
    private FailHandler _failHandler;

    private void Awake()
    {
        _uiScreenManager.Init();

        GameStateManager.SetState(GameStateManager.GameState.Paused);
        _uiScreenManager.ShowMenuScreen();
        _uiScreenManager.PlayClickedEvent += OnPlayClicked;
        _uiScreenManager.SaveScoreClickedEvent += OnScoreSave;
        _uiScreenManager.MenuClickedEvent += OnMenuClicked;
    }

    private void OnPlayClicked()
    {
        InitNewGame();
        StartGame();
    }

    private void InitNewGame() 
    {
        _scoreHandler = new();
        _failHandler = new();

        _scoreHandler.Init(_clickHandler);
        _uiScreenManager.InitScoreScreen(_scoreHandler);
        _failHandler.Init(_balloonTerminator, _gameSettings.FailBalloonAmount);
        _failHandler.GameOverEvent += OnGameOver;
        _uiScreenManager.InitFailScreen(_failHandler, _gameSettings.FailBalloonAmount);
    }

    public void StartGame()
    {
        GameStateManager.SetState(GameStateManager.GameState.GamePlay);
        _balloonSpawner.StartSpawn();
    }

    public void StopGame()
    {
        _balloonSpawner.StopSpawn();
        _balloonSpawner.RemoveBalloons();
    }

    private void OnGameOver()
    {
        StopGame();
        GameStateManager.SetState(GameStateManager.GameState.GameOver);

        _uiScreenManager.RestartClickedEvent += RestartGame;
        _uiScreenManager.ShowGameOverScreen(_scoreHandler.Score);
    }

    private void OnScoreSave(string name) 
    {
        _scoreSaver.OnScoreSave(name, _scoreHandler.Score);
    }

    private void RestartGame() 
    {
        _uiScreenManager.RestartClickedEvent -= RestartGame;

        EndGame();
        OnPlayClicked();
    }

    private void OnMenuClicked() 
    {
        StopGame();
        EndGame();
    }

    private void EndGame() 
    {
        _uiScreenManager.UnsubscribeScreens();

        if(_scoreHandler != null) _scoreHandler.UnSubscribe();

        _failHandler.GameOverEvent -= OnGameOver;
        _failHandler.UnSubscribe();
    }

    private void OnDestroy()
    {
        EndGame();
        _uiScreenManager.PlayClickedEvent -= OnPlayClicked;
        _uiScreenManager.SaveScoreClickedEvent -= OnScoreSave;
        _uiScreenManager.MenuClickedEvent -= OnMenuClicked;
    }
}