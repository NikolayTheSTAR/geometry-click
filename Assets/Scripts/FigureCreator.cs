using System.Collections.Generic;
using UnityEngine;

public interface IFigureCreator
{
    void CreateFigure(Vector3 position);

    void CreateFigure(GameObject figureObject, Vector3 position);

    void AddFigure(GameObject figure);
}

public class FigureCreator : MonoBehaviour, IFigureCreator
{
    #region Private

    private List<GameObject> figures = new List<GameObject>();

    #endregion // Private

    private GameObject GetRandomFigure()
    {
        int i = Random.Range(0, figures.Count);
        return figures[i];
    }

    public void CreateFigure(Vector3 position)
    {
        GameObject figureObject = GetRandomFigure();
        Instantiate(figureObject, position, figureObject.transform.rotation, transform);
    }

    public void CreateFigure(GameObject figureObject, Vector3 position)
    {
        Instantiate(figureObject, position, figureObject.transform.rotation, transform);
    }

    public void AddFigure(GameObject figure)
    {
        figures.Add(figure);
    }
}