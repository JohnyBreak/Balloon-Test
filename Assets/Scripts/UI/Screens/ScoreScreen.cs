using UnityEngine;

public class ScoreScreen : BaseUIScreen
{
    [SerializeField] private TMPro.TextMeshProUGUI _scoreText;

    private ScoreHandler _scoreHandler;

    private readonly string _scorePrefix = "Current score";

    public void Init(ScoreHandler handler) 
    {
        _scoreHandler = handler;
        _scoreHandler.ScoreChangedEvent += OnScoreChanged;
        OnScoreChanged(0);
    }

    public void UnSubscribe()
    {
        if(_scoreHandler != null) _scoreHandler.ScoreChangedEvent -= OnScoreChanged;
    }

    private void OnScoreChanged(int score) 
    {
        _scoreText.text = $"{_scorePrefix} {score}";
    }
}
