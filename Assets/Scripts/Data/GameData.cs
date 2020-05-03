using UnityEngine;

[CreateAssetMenu(menuName = "GameData", fileName = "GameData")]
public class GameData : ScriptableObject
{
    [SerializeField]
    private int observableTime;
    public int ObservableTime
    {
        get
        {
            return observableTime;
        }
        set
        {
            observableTime = value;
        }
    }
}