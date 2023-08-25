using System;

public class FailHandler : BaseHandler
{
    public event Action<int> ValueChangedEvent;
    public event Action GameOverEvent;
    private BalloonTerminator _balloonTerminator;

    public override void Init()
    {
    }

    public void Init(BalloonTerminator terminator, int startAmount)
    {
        _balloonTerminator = terminator;
        _value = startAmount;
        _balloonTerminator.BalloonDestroyedByTerminatorEvent += Handle;
    }

    public override void UnSubscribe()
    {
        _balloonTerminator.BalloonDestroyedByTerminatorEvent -= Handle;
    }

    protected override void Handle()
    {
    }
    protected void Handle(Balloon balloon)
    {
        _value -= balloon.GetTerminateValue();

        if (_value < 1) GameOverEvent?.Invoke();

        ValueChangedEvent?.Invoke(_value);
    }
}
