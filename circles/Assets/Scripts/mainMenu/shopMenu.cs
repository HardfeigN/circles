using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class shopMenu : MonoBehaviour
{
    [SerializeField] private GameObject _colorButtonObject;
    [SerializeField] private GameObject _priceSkinObject;
    [SerializeField] private GameObject _priceColorObject;
    [SerializeField] private GameObject _buttonSkinText;
    [SerializeField] private GameObject _buttonColorText;
    [SerializeField] private GameObject _backgroundObject;
    [SerializeField] private GameObject _skinObject;
    [SerializeField] private GameObject _pointTextObject;
    private GameObject _currentChangeObject;
    private List<Sprite> _skinSprites;
    private playerData _playerData;
    private Color[] _colors = { Color.white, Color.black, Color.gray, Color.red, Color.green, Color.blue, Color.cyan, Color.grey, Color.magenta, Color.yellow };
    private int _currentSkinIndex;
    private int _currentColorIndex;
    private bool _isSaving = false;
    public void StartSkinSelection(string typeName)
    {
        _playerData = playerData.LoadFromSaveData(saveLoadPData.LoadPlayerData());
        _skinSprites = new List<Sprite>();
        _currentSkinIndex = -1;
        _currentColorIndex = -1;
        LoadSkins(typeName);
        if (typeName == "ball")
        {
            _colorButtonObject.SetActive(true);
            _currentChangeObject = _skinObject;
            _skinObject.GetComponent<Image>().color = _playerData.GetColor();
            _priceSkinObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "100";
            _backgroundObject.GetComponent<Image>().sprite = _playerData.GetBackgroundSkin();
        }
        else if (typeName == "background")
        {
            _colorButtonObject.SetActive(false);
            _currentChangeObject = _backgroundObject;
            _priceSkinObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "200";
            _skinObject.GetComponent<Image>().color *= new Color(1, 1, 1, 0);
        }
        ChangeCurrentSkinIndex(true);
        ChangeCurrentColorIndex(true);
    }
    public void ChangeCurrentSkinIndex(bool next)
    {
        _currentSkinIndex += next ? +1 : -1;
        if (_currentSkinIndex < 0) _currentSkinIndex = _skinSprites.Count - 1;
        if (_currentSkinIndex >= _skinSprites.Count) _currentSkinIndex = 0;
        ChangeSkin();
        ChangeSkinButtonText();
    }
    public void ChangeCurrentColorIndex(bool next)
    {
        _currentColorIndex += next ? +1 : -1;
        if (_currentColorIndex < 0) _currentColorIndex = _colors.Length - 1;
        if (_currentColorIndex >= _colors.Length) _currentColorIndex = 0;
        ChangeColor();
        ChangeColorButtonText();
    }
    public void SelectSkin()
    {
        if (_colorButtonObject.activeSelf) _playerData.BuyBallSkin(_skinSprites[_currentSkinIndex]);
        else _playerData.BuyBackgroundSkin(_skinSprites[_currentSkinIndex]);
        SaveShopChanges();
        ChangeSkinButtonText();
    }
    public void SelectColor()
    {
        _playerData.BuyColor(_colors[_currentColorIndex]);
        SaveShopChanges();
        ChangeColorButtonText();
    }
    public bool IsSaving()
    {
        return _isSaving;
    }
    private void ChangeSkinButtonText()
    {
        string buttonText = "";
        if (_playerData.IsBallSkinAvailable(_skinSprites[_currentSkinIndex]) ||
            _playerData.IsBackgroundSkinAvailable(_skinSprites[_currentSkinIndex]))
        {
            if (_playerData.GetBallSkin() == _skinSprites[_currentSkinIndex] ||
                _playerData.GetBackgroundSkin() == _skinSprites[_currentSkinIndex]) buttonText = "Selected";
            else buttonText = "Select";
            _priceSkinObject.SetActive(false);
        }
        else
        {
            buttonText = "Buy!";
            _priceSkinObject.SetActive(true);
        }
        _buttonSkinText.GetComponent<TMPro.TextMeshProUGUI>().text = buttonText;
    }
    private void ChangeColorButtonText()
    {
        string buttonText = "";
        if (_playerData.IsColorAvailable(_colors[_currentColorIndex]))
        {
            if (_playerData.GetColor() == _colors[_currentColorIndex]) buttonText = "Selected";
            else buttonText = "Select";
            _priceColorObject.SetActive(false);
        }
        else
        {
            buttonText = "Buy!";
            _priceColorObject.SetActive(true);
        }
        _buttonColorText.GetComponent<TMPro.TextMeshProUGUI>().text = buttonText;
    }
    private void LoadSkins(string lastFolder)
    {
        _skinSprites.Clear();
        Sprite[] skins;
        string path = "skins/" + lastFolder;
        skins = Resources.LoadAll<Sprite>(path);
        foreach (Sprite skin in skins)
        {
            _skinSprites.Add(skin);
        }
    }
    private void SaveShopChanges()
    {
        _pointTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = _playerData.GetPoints().ToString();
        _isSaving = true;
        saveLoadPData.SavePlayerData(_playerData.ConvetToSaveData(), this);
        _isSaving = false;
    }
    private void ChangeSkin()
    {
        _currentChangeObject.GetComponent<Image>().sprite = _skinSprites[_currentSkinIndex];
    }
    private void ChangeColor()
    {
        _currentChangeObject.GetComponent<Image>().color = _colors[_currentColorIndex];
    }
}
