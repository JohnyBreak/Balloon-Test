using System;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardScreen : BaseUIScreen
{
    public event Action ExitClickedEvent;

    [SerializeField] private LeaderBoard _leaderBoardPrefab;
    [SerializeField] private Button _exitButton;

    public override void ShowScreen()
    {
        _exitButton.onClick.AddListener(OnExitClicked);
        CreateLeaderBoard();
        base.ShowScreen();
    }

    private void OnExitClicked() 
    {
        ExitClickedEvent?.Invoke();

        _exitButton.onClick.RemoveListener(OnExitClicked);
    }

    private void CreateLeaderBoard() 
    {
        var board = Instantiate(_leaderBoardPrefab, _holder.transform);
        board.FillBoard();
    }
}
