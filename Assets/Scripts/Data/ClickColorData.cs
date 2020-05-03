using System;
using UnityEngine;

[Serializable]
public class ClickColorData
{
    public string ObjectType; // поле прописано в ТЗ, но нигде не используется (GeometryObjectModel хранит свой тип в виде enum FigureTypes)

    public int MinClicksCount;
    public int MaxClicksCount;
    public Color Color;
}