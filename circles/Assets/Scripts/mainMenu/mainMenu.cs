using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pointTextObject;
    [SerializeField] private GameObject _bestScoreTextObject;
    playerData _playerData;

    private void Awake()
    {
        _playerData = playerData.LoadFromSaveData(saveLoadPData.LoadPlayerData());
        _pointTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = _playerData.GetPoints().ToString();
        _bestScoreTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Best score: " + _playerData.GetBestScore().ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
