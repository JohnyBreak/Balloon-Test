using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnBalloonSettings", menuName = "ScriptableObjects/SpawnBalloonSettings")]
public class SpawnBalloonSettings : ScriptableObject
{
    [SerializeField, Min(0.01f)] private float _balloonMoveSpeedUp = .2f;
    [SerializeField, Min(0.01f)] private float _balloonSpawnDelayReduce = 0.05f;

    [SerializeField, Min(2)] private int _decreaseBalloonSpawnPeriod = 3;
    [SerializeField, Min(2)] private int _balloonSpeedUpPeriod = 6;
    [SerializeField, Min(2)] private int _balloonSpawnDelayReducePeriod = 10;

    public int DecreaseBalloonSpawnPeriod => _decreaseBalloonSpawnPeriod;
    public int BalloonSpeedUpPeriod => _balloonSpeedUpPeriod;
    public int BalloonSpawnDelayReducePeriod => _balloonSpawnDelayReducePeriod;

    public float BalloonMoveSpeedUp => _balloonMoveSpeedUp;
    public float BalloonSpawnDelayReduce => _balloonSpawnDelayReduce;
}
