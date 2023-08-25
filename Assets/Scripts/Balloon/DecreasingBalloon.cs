using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreasingBalloon : Balloon
{
    public override int GetScoreValue()
    {
        return -1;
    }

    public override int GetTerminateValue()
    {
        return 0;
    }
    public override void BackToPool()
    {
        DecreasingBalloonPool.Instance.DisableObject(this);
    }
}
