using UnityEngine;
using UnityEngine.UI;

public class GameHost : MonoBehaviour
{
    [SerializeField] private GameObject _scoreTextObject;
    [SerializeField] private GameObject _heartImageObject;
    [SerializeField] private GameObject _parentObject;
    [SerializeField] private GameObject _loseMenuObject;
    [SerializeField] private GameObject _backgroungObject;
    [SerializeField] private GameObject _bestScoreTextObject;
    private GameObject _spawnerObject;
    private PointsCounter _pointsCounter;
    private Spawner _spawner;
    private HealthController _healthController;
    private playerData _playerData;
    private Coroutine _coroutine;
    private bool _isSaving = false;

    public bool _pause { get; private set; }

    void Start()
    {
        Application.targetFrameRate = 60;
        StartGame();
    }
    public void StartGame()
    {
        _spawnerObject = new GameObject("SP");
        _pointsCounter = new PointsCounter();
        _healthController = new HealthController();
        _playerData = playerData.LoadFromSaveData(saveLoadPData.LoadPlayerData());
        _spawner = _spawnerObject.AddComponent<Spawner>();
        _backgroungObject.GetComponent<Image>().sprite = _playerData.GetBackgroundSkin();
        _pointsCounter.Initialization(_scoreTextObject);
        _healthController.Initialization(_heartImageObject, _parentObject, 3);
        _spawner.Initialization(_playerData.GetBallSkin(), _playerData.GetColor(), gameObject);
        _coroutine = StartCoroutine(_spawner.StartSpawner());
        _bestScoreTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Best score: " + _playerData.GetBestScore().ToString();
    }
    public void LoseGame()
    {
        if (_healthController.GetCurrentHP() == 0)
        {
            StopCoroutine(_coroutine);
            Destroy(_spawnerObject);
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (GameObject ball in balls)
            {
                Destroy(ball);
            }
            _playerData.AddPointsAndBestScore(_healthController, _pointsCounter);
            _isSaving = true;
            saveLoadPData.SavePlayerData(_playerData.ConvetToSaveData(), this);
            _isSaving = false;
            _loseMenuObject.GetComponent<Image>().GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "" + _pointsCounter.GetPointsCount();
            _loseMenuObject.SetActive(true);
        }
    }
    public void SetPause(bool pause)
    {
        _pause = pause;
        Time.timeScale = (_pause) ? 0f : 1f;
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach ( GameObject ball in balls)
        {
            ball.transform.position += (_pause) ? new Vector3(0, 0, -90) : new Vector3(0, 0, 90);
        }
    }
    public PointsCounter GetpointsCounter()
    {
        return _pointsCounter;
    }
    public GameObject GetScoreObject()
    {
        return _scoreTextObject;
    }
    public bool IsSaving()
    {
        return _isSaving;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Destroy(collision.gameObject);
            _healthController.BrokeHeart();
        }
    }
}
