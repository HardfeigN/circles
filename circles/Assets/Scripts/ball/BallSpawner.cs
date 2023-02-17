using UnityEngine;
    
public class BallSpawner : MonoBehaviour
{
    private GameObject _gameHostObject;
    private float _spawnHeight = 5.02f;
    private float _spawnLeftBorder = -2.83f;
    private float _spawnRightBorder = 2.83f;
    private float _radiusMinLimit = 0.4f;
    private float _radiusMaxLimit = 1.4f;
    private bool _initialized = false;

    public BallSpawner(GameObject gameHost)
    {
        if (!_initialized)
        {
            _gameHostObject = gameHost;
            _initialized = true;
        }
    }
    public void SpawnBall(Sprite sprite, Color color, float speedMultiplyer)
    {
        GameObject ballPrefab = Resources.Load<GameObject>("prefabs/ballPrefab");
        float ballRadius = Random.Range(_radiusMinLimit, _radiusMaxLimit);
        float xBallBosition = Random.Range(_spawnLeftBorder + ballRadius, _spawnRightBorder - ballRadius);
        bool isGoldenBall = (Random.Range(1, 100) < 10) ? true : false;

        GameObject ball = Instantiate(ballPrefab, new Vector3(xBallBosition, _spawnHeight + ballRadius, 0), new Quaternion());
        ball.GetComponent<Circle>().Initialization(_gameHostObject, ball, sprite, color, ballRadius, speedMultiplyer, isGoldenBall);
        ball.GetComponent<Circle>().SetBallObjectValues();
    }
}
