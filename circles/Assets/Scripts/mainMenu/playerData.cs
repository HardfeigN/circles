using UnityEngine;
using System.Collections.Generic;
using System;

public class playerData : MonoBehaviour
{
    private int _points;
    private int _bestScore;
    private Sprite _ballSkin;
    private Sprite _backgroundSkin;
    private Color _color;
    private List<Sprite> _availableBallSkins;
    private List<Sprite> _availableBackgroundSkins;
    private List<Color> _availableColors;

    public playerData()
    {
        _points = 1000;
        _bestScore = 0;
        _ballSkin = Resources.Load<Sprite>("skins/ball/circle");
        _backgroundSkin = Resources.Load<Sprite>("skins/background/jinx");
        _color = Color.black;
        _availableBallSkins = new List<Sprite>();
        _availableBackgroundSkins = new List<Sprite>();
        _availableColors = new List<Color>();
        _availableBackgroundSkins.Clear();
        _availableBallSkins.Clear();
        _availableBallSkins.Add(_ballSkin);
        _availableBackgroundSkins.Add(_backgroundSkin);
        _availableColors.Add(_color);
    }
    private playerData(int points, int bestScore, Sprite ballSkin, Sprite backgroundSkin, Color color, List<Sprite> availableBallSkins, List<Sprite> availableBackgroundSkins, List<Color> availableColors)
    {
        _points = points;
        _bestScore = bestScore;
        _ballSkin = ballSkin;
        _backgroundSkin = backgroundSkin;
        _color = color;
        _availableBallSkins = availableBallSkins;
        _availableBackgroundSkins = availableBackgroundSkins;
        _availableColors = availableColors;
    }
    public static playerData LoadFromSaveData(saveData sData)
    {
        int points = sData.GetPoints();
        int bestScore = sData.GetBestScore();
        string[] loadedBallSkins = sData.GetAvailableBallSkins();
        List<Sprite> availableBallSkins = new List<Sprite>();
        List<Sprite> availableBackgroundSkins = new List<Sprite>(0);
        List<Color> availableColors = new List<Color>(0);
        availableBackgroundSkins.Clear();
        availableBallSkins.Clear();
        availableColors.Clear();
        Sprite ballSkin = Resources.Load<Sprite>("skins/ball/" + sData.GetBallSkin());
        Sprite backgroundSkin = Resources.Load<Sprite>("skins/background/" + sData.GetBackgroundSkin());
        for(int i = 0; i < loadedBallSkins.Length; i++)
        {
            availableBallSkins.Add(Resources.Load<Sprite>("skins/ball/" + loadedBallSkins[i]));
        }
        loadedBallSkins = sData.GetAvailableBackgroundSkins();
        for (int i = 0; i < loadedBallSkins.Length; i++)
        {
            availableBackgroundSkins.Add(Resources.Load<Sprite>("skins/background/" + loadedBallSkins[i]));
        }
        float r = 0.000f, g = 0.000f, b = 0.000f, a = 0.000f;
        string[] loadedColors = sData.GetAvailableColors();
        for (int i = 0; i < loadedColors.Length; i++)
        {
            string[] rgba = loadedColors[i].Split(' ');
            r = float.Parse(rgba[0]);
            g = float.Parse(rgba[1]);
            b = float.Parse(rgba[2]);
            a = float.Parse(rgba[3]);
            availableColors.Add(new Color(r, g, b, a));
        }
        string[] colorString = sData.GetColor().Split(' ');
        r = 0.000f; g = 0.000f; b = 0.000f; a = 0.000f;
        r = float.Parse(colorString[0]);
        g = float.Parse(colorString[1]);
        b = float.Parse(colorString[2]);
        a = float.Parse(colorString[3]);

        return new playerData(points, bestScore, ballSkin, backgroundSkin, new Color(r,g,b,a), availableBallSkins, availableBackgroundSkins, availableColors);
    }
    public saveData ConvetToSaveData()
    {
        string[] availableBallSkins = new string[_availableBallSkins.Count];
        for (int i = 0; i < _availableBallSkins.Count; i++)
        {
            availableBallSkins[i] = _availableBallSkins[i].name;
        }
        string[] availableBackgroundSkins = new string[_availableBackgroundSkins.Count];
        for (int i = 0; i < _availableBackgroundSkins.Count; i++)
        {
            availableBackgroundSkins[i] = _availableBackgroundSkins[i].name;
        }
        string[] availableColors = new string[_availableColors.Count];
        for (int i = 0; i < _availableColors.Count; i++)
        {
            availableColors[i] = _availableColors[i].r + " " + _availableColors[i].g + " " + _availableColors[i].b + " " + _availableColors[i].a;
        }

        return new saveData(_points, _bestScore, _ballSkin.name, _backgroundSkin.name, _color.r + " " + _color.g + " " + _color.b + " " + _color.a, availableBallSkins, availableBackgroundSkins, availableColors);
    }
    public void AddPointsAndBestScore(HealthController HPC, PointsCounter pointsCounter)
    {
        if(HPC.GetCurrentHP() == 0)
        {
            _points += pointsCounter.GetAmounntWon();
            _bestScore = (_bestScore < pointsCounter.GetPointsCount()) ? pointsCounter.GetPointsCount() : _bestScore;
        }
    }
    public void BuyBallSkin(Sprite skin)
    {
        if (_points >= 100 && !_availableBallSkins.Contains(skin))
        {
            _points -= 100;
            _availableBallSkins.Add(skin);
            ChangeBallSkin(skin);
        } else if(_availableBallSkins.Contains(skin)) ChangeBallSkin(skin);
    }
    public void BuyBackgroundSkin(Sprite skin)
    {
        if (_points >= 200 && !_availableBackgroundSkins.Contains(skin))
        {
            _points -= 200;
            _availableBackgroundSkins.Add(skin);
            ChangeBackgroundSkin(skin);
        } else if (_availableBackgroundSkins.Contains(skin)) ChangeBackgroundSkin(skin);
    }
    public void BuyColor(Color color)
    {
        if (_points >= 100 && !_availableColors.Contains(color))
        {
            _points -= 100;
            _availableColors.Add(color);
            ChangeColor(color);
        } else if(_availableColors.Contains(color)) ChangeColor(color);
    }
    public bool IsBallSkinAvailable(Sprite skin)
    {
        return _availableBallSkins.Contains(skin) ? true : false;
    }
    public bool IsBackgroundSkinAvailable(Sprite skin)
    {
        return _availableBackgroundSkins.Contains(skin) ? true : false;
    }
    public bool IsColorAvailable(Color color)
    {
        return _availableColors.Contains(color) ? true : false;
    }
    public int GetPoints()
    {
        return _points;
    }
    public int GetBestScore()
    {
        return _bestScore;
    }
    public Sprite GetBallSkin()
    {
        return _ballSkin;
    }
    public Sprite GetBackgroundSkin()
    {
        return _backgroundSkin;
    }
    public Color GetColor()
    {
        return _color;
    }
    private void ChangeBallSkin(Sprite skin)
    {
        _ballSkin = _availableBallSkins.Contains(skin) ? skin : _ballSkin;
    }
    private void ChangeBackgroundSkin(Sprite skin)
    {
        _backgroundSkin = _availableBackgroundSkins.Contains(skin) ? skin : _backgroundSkin;
    }
    private void ChangeColor(Color color)
    {
        _color = _availableColors.Contains(color) ? color : _color;
    }
}
