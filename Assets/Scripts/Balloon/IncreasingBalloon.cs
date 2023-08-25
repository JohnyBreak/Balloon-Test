public class IncreasingBalloon : Balloon
{
    public override int GetScoreValue()
    {
        return 1;
    }

    public override int GetTerminateValue()
    {
        return 1;
    }
    public override void BackToPool()
    {
        IncreasingBalloonPool.Instance.DisableObject(this);
    }
}
