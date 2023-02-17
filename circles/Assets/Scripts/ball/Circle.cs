using UnityEngine;

public class Circle : MonoBehaviour
{
    private GameObject _ballObject;
    private GameObject _gameHostObject;
    private Sprite _sprite;
    private Color _color;
    private Vector3 _scaleVector;
    private float _radius, _speedMultiplyer;
    private int _score;
    private bool _initialized = false, _setBallValues;
    private bool _isGolden;

    public void Initialization(GameObject gameHostObject, GameObject ballObject, Sprite sprite, Color color, float radius, float speedMultiplyer, bool isGolden)
    {
        if (!_initialized)
        {
            _color = color;
            _isGolden = isGolden;
            _gameHostObject = gameHostObject;
            _ballObject = ballObject;
            _sprite = sprite;
            _radius = radius;
            _score = (int)( 100 / (_radius * 10));
            _initialized = true;
            _setBallValues = false;
            _speedMultiplyer = speedMultiplyer;
            _scaleVector = new Vector3(0.8f, 0.8f, 0.8f);
            GoldenPulse();
        }
    }
    public void SetBallObjectValues()
    {
        if (_initialized && !_setBallValues)
        {
            _ballObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            _ballObject.GetComponent<Transform>().localScale = new Vector3(_radius, _radius, 1);
            _ballObject.GetComponent<SpriteRenderer>().sprite = _sprite;
            _ballObject.GetComponent<SpriteRenderer>().color = _color;
            _ballObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, CountSpeed());
            if (_isGolden) _ballObject.GetComponent<SpriteRenderer>().color = new Color(255, 215, 0);
            _setBallValues = true;
        }
    }
    public int GetBallScore()
    {
        return _isGolden ? _score * 2 : _score;
    }
    private float CountSpeed()
    {
        float speed = (1 / (0.5f * _radius)) * _speedMultiplyer;
        speed = (speed - 4f) * 0.7f + 4f;
        return -speed; //speed Multiplyer limits [1, 3.5] => speed limits [~0.7, 8.75]
    }
    private void GoldenPulse()
    {
        if (_isGolden)
        {
            _ballObject.transform.localScale = Vector3.Lerp(new Vector3(_radius, _radius, 1), _scaleVector, 0.2f);
            _scaleVector = (_scaleVector.x < 1) ? new Vector3(1.25f, 1.25f, 1.25f) : new Vector3(0.8f, 0.8f, 0.8f);
            Invoke("GoldenPulse", 0.3f);
        }
    }
    private void OnMouseDown()
    {
        if(!_gameHostObject.GetComponent<GameHost>()._pause)
            _gameHostObject.GetComponent<GameHost>().GetpointsCounter().IsClickOnCircle();
    }
}
