using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "GeometryObjectData", fileName = "GeometryObjectData")]
public class GeometryObjectData : ScriptableObject
{
    [SerializeField]
    private List<ClickColorData> clicksData = new List<ClickColorData>();
    public List<ClickColorData> ClicksData
    {
        get
        {
            return clicksData;
        }
        set
        {
            clicksData = value;
        }
    }
}