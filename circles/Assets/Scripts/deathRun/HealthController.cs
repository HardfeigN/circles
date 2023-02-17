using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private GameObject _gameHostObject;
    private GameObject _parentImageObject;
    private GameObject[] _heartsGameObjects;
    private Sprite _heartSprite;
    private Sprite _brokenHeartSprite;
    private int _healthPointCount;
    private int _currentHP;
    private bool _initialized = false;

    public void Initialization(GameObject parentImageObject, GameObject gameHostObject, int healthPointCount)
    {
        if (!_initialized)
        {
            _heartSprite = Resources.Load<Sprite>("gui images/heart");
            _brokenHeartSprite = Resources.Load<Sprite>("gui images/broken-heart");
            _healthPointCount = healthPointCount;
            _currentHP = _healthPointCount;
            _gameHostObject = gameHostObject;
            _parentImageObject = parentImageObject;
            _heartsGameObjects = new GameObject[_healthPointCount];
            SetHeartsOnGameScreen();
            _initialized = true;
        }
    }
    public void BrokeHeart()
    {
        if (_currentHP <= _healthPointCount && _currentHP > 0)
        {
            _heartsGameObjects[_currentHP - 1].GetComponent<Image>().sprite = _brokenHeartSprite;
            _heartsGameObjects[_currentHP - 1].GetComponent<Image>().color = new Color(1f, 1f, 1f, .5f);
            _heartsGameObjects[_currentHP - 1].GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
            _currentHP--;
        }
        if (_currentHP == 0) _gameHostObject.GetComponent<GameHost>().LoseGame();
    }
    public int GetCurrentHP()
    {
        return _currentHP;
    }
    private void SetHeartsOnGameScreen()
    {
        GameObject heartPrefab = Resources.Load<GameObject>("prefabs/heartPrefab");
        for (int i = 0; i < _healthPointCount; i++)
        {
            _heartsGameObjects[i] = Instantiate(heartPrefab, _parentImageObject.transform.position, new Quaternion());
            _heartsGameObjects[i].transform.position += new Vector3(i * 100, 1, 1);
            _heartsGameObjects[i].GetComponent<Image>().sprite = _heartSprite;
            _heartsGameObjects[i].GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
            _heartsGameObjects[i].transform.SetParent(_parentImageObject.transform, false);
        }
    }
}
