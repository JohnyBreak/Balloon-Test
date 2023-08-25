using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Balloon : MonoBehaviour, IPoolable
{
    //[SerializeField] 
    private float _speed = 5;

    public void Init(float speed) 
    {
        _speed = speed;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (GameStateManager.CurrentGameState != GameStateManager.GameState.GamePlay) return;

        transform.position += Vector3.up * _speed * Time.deltaTime;
    }

    public void Click() 
    {
        BackToPool();
    }

    public abstract void BackToPool();
    //{
    //    IncreasingBalloonPool.Instance.DisableObject(this);
    //}

    public abstract int GetScoreValue();
    public abstract int GetTerminateValue();
}
