using System.Collections.Generic;

[System.Serializable]
public class saveData
{
    private int _points;
    private int _bestScore;
    private string _ballSkinName;
    private string _backgroundSkinName;
    private string _color;
    private string[] _availableBallSkins;
    private string[] _availableBackgroundSkins;
    private string[] _availableColors;

    public saveData()
    {
        _points = 1000;
        _bestScore = 0;
        _ballSkinName = "circle";
        _backgroundSkinName = "jinx";
        _color = "0,000 0,000 0,000 1,000";
        _availableBallSkins = new string[1];
        _availableBackgroundSkins = new string[1];
        _availableColors = new string[1];
        _availableBallSkins[0] = _ballSkinName;
        _availableBackgroundSkins[0] = _backgroundSkinName;
        _availableColors[0] = _color;
    }
    public saveData(int points, int bestScore, string ballSkinName, string backgroundSkinName, string color, string[] availableBallSkins, string[] availableBackgroundSkins, string[] availableColors)
    {
        _points = points;
        _bestScore = bestScore;
        _ballSkinName = ballSkinName;
        _backgroundSkinName = backgroundSkinName;
        _color = color;
        _availableBallSkins = availableBallSkins;
        _availableBackgroundSkins = availableBackgroundSkins;
        _availableColors = availableColors;
    }
    public int GetPoints()
    {
        return _points;
    }
    public int GetBestScore()
    {
        return _bestScore;
    }
    public string GetBallSkin()
    {
        return _ballSkinName;
    }
    public string GetBackgroundSkin()
    {
        return _backgroundSkinName;
    }
    public string GetColor()
    {
        return _color;
    }
    public string[] GetAvailableBallSkins()
    {
        return _availableBallSkins;
    }
    public string[] GetAvailableBackgroundSkins()
    {
        return _availableBackgroundSkins;
    }
    public string[] GetAvailableColors()
    {
        return _availableColors;
    }
}
