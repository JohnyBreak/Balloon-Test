using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] private BalloonSettings _balloonSettings;
    [SerializeField] private SpawnBalloonSettings _spawnSettings;
    private Vector2 _volume;
    private Vector2 _centerPos;

    private Coroutine _spawnCoroutine;

    private float _delayBetweenSpawns = 0.1f;
    private float _balloonSpeed = 0.1f;
    private int _decreasingBalloonSpawnCount = 0;
    private int _totalSpawnCount = 0;

    void Awake()
    {
        SetUp();
    }

    //private void Start()
    //{
    //    _increasingPool = (IncreasingBalloonPool)IncreasingBalloonPool.Instance;
    //    _decreasingPool = (DecreasingBalloonPool)DecreasingBalloonPool.Instance;
    //}

    private void SetUp()
    {
        SetUpPosition();
        SetUpSize();
    }

    private void SetUpPosition()
    {
        var collider = GetComponent<BoxCollider2D>();
        float screenHeight = 2f * Camera.main.orthographicSize;
        float Y = screenHeight / 2; 

        Vector3 tempPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, -Y));
        tempPosition.z = 0;
        tempPosition.y -= collider.transform.localScale.y;// * 2.5f;
        transform.localPosition = tempPosition;
        _centerPos = transform.position;
    }

    private void SetUpSize() 
    {
        var collider = GetComponent<BoxCollider2D>();

        var screenWidth = (Camera.main.orthographicSize * 2.0 * Screen.width / Screen.height) - 1;
        collider.transform.localScale = new Vector3((float)screenWidth, collider.transform.localScale.y, collider.transform.localScale.z);
        _volume = new Vector2(collider.size.x * transform.localScale.x / 2, collider.size.y * transform.localScale.y / 2);
    }

    private void SetVariables()
    {
        _delayBetweenSpawns = _balloonSettings.BalloonSpawnDelay;
        _balloonSpeed = _balloonSettings.BalloonMoveSpeed;
    }

    public void StartSpawn() 
    {
        if (_spawnCoroutine != null)
        {
            StopSpawn();
        }
        SetVariables();
        _spawnCoroutine = StartCoroutine(SpawnRoutine(.5f));
    }

    public void StopSpawn()
    {
        if (_spawnCoroutine == null) return;

        StopCoroutine(_spawnCoroutine);
        _spawnCoroutine = null;
    }

    private IEnumerator SpawnRoutine(float delayBeforeStart) 
    {
        yield return new WaitForSeconds(delayBeforeStart);

        while (true) 
        {
            SpawnBalloon();
            yield return new WaitForSeconds(_delayBetweenSpawns);
        }
    }

    private void SpawnBalloon()
    {
        Vector3 startPos = new Vector3(UnityEngine.Random.Range(_centerPos.x - _volume.x, _centerPos.x + _volume.x),
        UnityEngine.Random.Range(_centerPos.y - _volume.y, _centerPos.y + _volume.y), 0);

        if (_totalSpawnCount % _spawnSettings.BalloonSpeedUpPeriod == 0) 
        {
            _balloonSpeed += _spawnSettings.BalloonMoveSpeedUp;
        }

        if (_totalSpawnCount % _spawnSettings.BalloonSpawnDelayReducePeriod == 0)
        {
            _delayBetweenSpawns -= _spawnSettings.BalloonSpawnDelayReduce;
            if (_delayBetweenSpawns < 0.05f) _delayBetweenSpawns = 0.05f;
        }

        Balloon balloon;
        if (_decreasingBalloonSpawnCount % _spawnSettings.DecreaseBalloonSpawnPeriod == 0)
        {
            _decreasingBalloonSpawnCount = 0;
            balloon = DecreasingBalloonPool.Instance.GetPooledObject();
        }
        else 
        {
            balloon = IncreasingBalloonPool.Instance.GetPooledObject();
        }
        balloon.Init(_balloonSpeed);
        balloon.transform.position = startPos;
        balloon.gameObject.SetActive(true);
        _decreasingBalloonSpawnCount++;
        _totalSpawnCount++;
    }

    public void RemoveBalloons()
    {
        IncreasingBalloonPool.Instance.DisableAllObjects();
        DecreasingBalloonPool.Instance.DisableAllObjects();
    }
}
