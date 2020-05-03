using System.Collections.Generic;
using UnityEngine;

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

    private Material material;

    private float observableTime;

    #endregion // Private

    private void Start()
    {
        Init();

        LoadData();

        UpdateColor();

        Timers.AddTimer(this, observableTime);
    }

    private void Init()
    {
        material = GetComponent<Renderer>().material;
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

    public void SetRandomColor()
    {
        FigureColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        material.color = FigureColor;
    }
}

public enum FigureTypes
{
    Cube,
    Cylinder,
    Sphere
}