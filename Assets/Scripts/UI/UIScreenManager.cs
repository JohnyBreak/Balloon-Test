using System;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenManager : MonoBehaviour
{
    public event Action PlayClickedEvent;
    public event Action RestartClickedEvent;

    public event Action MenuClickedEvent;

    public event Action<string> SaveScoreClickedEvent;

    [SerializeField] private ScoreScreen _scoreScreen;
    [SerializeField] private FailScreen _failScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private MenuScreen _menuScreen;
    [SerializeField] private LeaderBoardScreen _leaderBoardScreen;

    private List<BaseUIScreen> _screens = new List<BaseUIScreen>();

    private void Awake()
    {
        _screens.Add(_scoreScreen);
        _screens.Add(_failScreen);
        _screens.Add(_gameOverScreen);
        _screens.Add(_menuScreen);
        _screens.Add(_leaderBoardScreen);
    }

    public void Init()
    {
        _menuScreen.PlayClickedEvent += OnPlayClicked;
        _menuScreen.ShowLBClickedEvent += OnShowLBClicked;

        _gameOverScreen.MenuClickedEvent += OnMenuClicked;
        _gameOverScreen.SaveScoreClickedEvent += OnSaveScoreClicked;

        _leaderBoardScreen.ExitClickedEvent += ShowMenuScreen;
    }

    public void InitScoreScreen(ScoreHandler handler)
    {
        _scoreScreen.Init(handler);
    }

    public void InitFailScreen(FailHandler handler, int amount)
    {
        _failScreen.Init(handler, amount);
    }

    private void ShowGamePlayUI()
    {
        ShowScreens(new BaseUIScreen[] { _failScreen, _scoreScreen });
    }

    private void ShowLB() 
    {
        ShowScreen(_leaderBoardScreen);
    }

    public void ShowMenuScreen()
    {
        ShowScreen(_menuScreen);
    }

    public void ShowGameOverScreen(int score)
    {
        ShowScreen(_gameOverScreen);

        _gameOverScreen.RestartClickedEvent += OnRestartClicked;
        _gameOverScreen.SetScoreText(score.ToString());
        
    }

    private void OnPlayClicked()
    {
        ShowGamePlayUI();
        PlayClickedEvent?.Invoke();
    }

    private void OnShowLBClicked()
    {
        ShowLB();
    }

    private void OnRestartClicked()
    {
        ShowGamePlayUI();
        RestartClickedEvent?.Invoke();
        _gameOverScreen.RestartClickedEvent -= OnRestartClicked;
    }

    private void OnMenuClicked()
    {
        ShowMenuScreen();
        MenuClickedEvent?.Invoke();
    }

    private void OnSaveScoreClicked(string name) 
    {
        SaveScoreClickedEvent?.Invoke(name);
    }

    public void UnsubscribeScreens()
    {
        _failScreen.UnSubscribe();
        _scoreScreen.UnSubscribe();
    }

    private void ShowScreen(BaseUIScreen screenToShow) 
    {
        foreach (var screen in _screens)
        {
            screen.HideScreen();
        }
        screenToShow.ShowScreen();
    }

    private void ShowScreens(BaseUIScreen[] screensToShow)
    {
        foreach (var screen in _screens)
        {
            screen.HideScreen();
        }
        foreach (var screen in screensToShow) 
        {
            screen.ShowScreen();
        }
    }

    private void OnDestroy()
    {
        _menuScreen.PlayClickedEvent -= OnPlayClicked;
        _menuScreen.ShowLBClickedEvent -= OnShowLBClicked;

        _gameOverScreen.MenuClickedEvent -= OnMenuClicked;
        _gameOverScreen.SaveScoreClickedEvent -= OnSaveScoreClicked;

        _leaderBoardScreen.ExitClickedEvent += ShowMenuScreen;
    }
}
