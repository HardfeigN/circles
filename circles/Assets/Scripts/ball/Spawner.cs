using System.Collections;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    private GameObject _scoreTextObject;
    private BallSpawner _ballSpawner;
    private Sprite _circleSprite;
    private Color _circleColor;
    private float _spawnRate;
    private float _speedMultiplyer;
    private bool _initialized = false;

    public void Initialization(Sprite circleSprite, Color circleColor, GameObject gameHostObject)
    {
        if (!_initialized)
        {
            _scoreTextObject = gameHostObject.GetComponent<GameHost>().GetScoreObject();
            _circleSprite = circleSprite;
            _circleColor = circleColor;
            _spawnRate = 1.3f;
            _speedMultiplyer = 1.5f;
            _ballSpawner = new BallSpawner(gameHostObject);
            _initialized = true;
        }
    }
    public IEnumerator StartSpawner()
    {
        while (_initialized)
        {
            ChangeSpawnRate();
            ChangeSpeedMultiplyer();
            _ballSpawner.SpawnBall(_circleSprite, _circleColor, _speedMultiplyer);
            yield return new WaitForSeconds(_spawnRate);
        }
    }
    private float CountGain()
    {
        return (GetPointsCount() - GetPointsCount() % 100) / 1000;
    }
    private float GetPointsCount()
    {
        string pointsCount;
        pointsCount = _scoreTextObject.GetComponent<TMPro.TextMeshProUGUI>().text.ToString();
        return Convert.ToInt32(pointsCount);
    }
    private void ChangeSpeedMultiplyer()
    {
        _speedMultiplyer = 1f + CountGain();
        if (_speedMultiplyer > 3.5) _speedMultiplyer = 3.5f;
    }
    private void ChangeSpawnRate()
    {
        _spawnRate = 1.3f - 0.5f * CountGain();
        if (_spawnRate < 0.2f) _spawnRate = 0.2f;
    }
}
