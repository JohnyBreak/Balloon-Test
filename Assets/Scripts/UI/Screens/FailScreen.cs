
using UnityEngine;

public class FailScreen : BaseUIScreen
{
    [SerializeField] private TMPro.TextMeshProUGUI _failText;

    private FailHandler _failHandler;

    private readonly string _failPrefix = "Skipped balloons to lose";

    public void Init(FailHandler handler, int startAmount)
    {
        _failHandler = handler;
        _failHandler.ValueChangedEvent += OnScoreChanged;
        OnScoreChanged(startAmount);
    }

    public void UnSubscribe()
    {
        if(_failHandler != null) _failHandler.ValueChangedEvent -= OnScoreChanged;
    }

    private void OnScoreChanged(int amount)
    {
        _failText.text = $"{_failPrefix} {amount}";
    }
}
