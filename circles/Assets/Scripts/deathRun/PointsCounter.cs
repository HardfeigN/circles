using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    private GameObject _scoreTextObject;
    private int _pointsCount;
    private bool _initialized = false;

    public void Initialization(GameObject scoreObject)
    {
        if (!_initialized)
        {
            _pointsCount = 0;
            _scoreTextObject = scoreObject;
            _initialized = true;
            _scoreTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = "0";
        }
    }
    public bool IsClickOnCircle()
    {
        bool ishit = false;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        Camera cam = Camera.main;

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Ball")
            {
                _pointsCount += hit.collider.gameObject.GetComponent<Circle>().GetBallScore();
                _scoreTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = _pointsCount.ToString();
                ishit = true;
                GameObject canvas = GameObject.FindObjectOfType<Canvas>().gameObject;
                GameObject prefab = Resources.Load<GameObject>("prefabs/ballPointsText");
                GameObject pointsText = Instantiate(prefab, canvas.transform);
                pointsText.transform.position = hit.collider.gameObject.transform.position;
                pointsText.GetComponent<TMPro.TextMeshProUGUI>().text = "+" + hit.collider.gameObject.GetComponent<Circle>().GetBallScore();
                Destroy(hit.collider.gameObject);
            }
        }
        return ishit;
    }
    public int GetPointsCount()
    {
        return _pointsCount;
    }
    public int GetAmounntWon()
    {
        return _pointsCount / 100;
    }
}
