using System.Collections.Generic;
using UnityEngine;
using UniRx;

public interface IGeometryObjectModel
{
    int ClickCount { get; set; }
    Color FigureColor { get; set; }
}

public class GeometryObjectModel : MonoBehaviour, IGeometryObjectModel
{
    #region Properties

    private int clickCount;
    public int ClickCount
    {
        get
        {
            return clickCount;
        }
        set
        {
            clickCount = value;
            UpdateColor();
        }
    }

    private Color figureColor;
    public Color FigureColor
    {
        get
        {
            return figureColor;
        }
        set
        {
            figureColor = value;
        }
    }

    #endregion // Properties

    #region Private

    [SerializeField]
    private FigureTypes type;

    private List<ClickColorData> cicksData = new List<ClickColorData>();
    private float observableTime;

    private Material material;
    private Timer timer;

    #endregion // Private

    private void Start()
    {
        Init();

        LoadData();

        UpdateColor();

        timer.Add(this, observableTime);
    }

    private void Init()
    {
        material = GetComponent<Renderer>().material;

        timer = new Timer();
    }

    public void Click()
    {
        ClickCount++;
    }

    private void LoadData()
    {
        GeometryObjectData data_figure = Resources.Load<GeometryObjectData>("Figures/GeometryObjectData_" + type.ToString());
        cicksData = data_figure.ClicksData;

        GameData data_game = Resources.Load<GameData>("GameData");
        observableTime = data_game.ObservableTime;
    }

    private void UpdateColor()
    {
        bool change = false;

        foreach (ClickColorData ccd in cicksData)
        {
            if (clickCount >= ccd.MinClicksCount & clickCount <= ccd.MaxClicksCount)
            {
                FigureColor = ccd.Color;
                change = true;
                break;
            }
        }

        if (change) material.color = FigureColor;
        else Debug.Log("Цвет не найден");
    }

    private void SetRandomColor()
    {
        FigureColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        material.color = FigureColor;
    }

    private class Timer
    {
        public void Add(GeometryObjectModel fastenedFigure, float stepTime)
        {
            Observable.Timer(System.TimeSpan.FromSeconds(stepTime)).Repeat().Subscribe(_ =>
            {
                fastenedFigure.SetRandomColor();

            }).AddTo(fastenedFigure);
        }
    }
}

public enum FigureTypes
{
    Cube,
    Cylinder,
    Sphere
}