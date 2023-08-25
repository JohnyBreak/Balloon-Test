using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BalloonSettings", menuName = "ScriptableObjects/BalloonSettings")]
public class BalloonSettings : ScriptableObject
{
    [SerializeField] private float _balloonMoveSpeed = 3f;
    [SerializeField] private float _balloonSpawnDelay = 0.5f;

    public float BalloonMoveSpeed => _balloonMoveSpeed;
    public float BalloonSpawnDelay => _balloonSpawnDelay;

    //public void SetBalloonMoveSpeed(float value) 
    //{
    //    _balloonMoveSpeed = value;
    //}

    //public void SetBalloonSpawnSpeed(float value)
    //{
    //    _balloonSpawnDelay = value;
    //}
}
