using System;

public class ScoreHandler : BaseHandler
{
    public event Action<int> ScoreChangedEvent;
    private ClickHandler _clickHandler;

    public int Score => _value;

    public override void Init()
    {
    }

    public void Init(ClickHandler clickHandler) 
    {
        _clickHandler = clickHandler;
        _clickHandler.BalloonClickedByPlayerEvent += Handle;
    }

    public override void UnSubscribe() 
    {
        _clickHandler.BalloonClickedByPlayerEvent -= Handle;
    }

    protected override void Handle()
    {
    }

    protected void Handle(Balloon balloon) 
    {
        if (_value + balloon.GetScoreValue() < 0) return;

        _value += balloon.GetScoreValue();
        ScoreChangedEvent?.Invoke(_value);
    }
}
