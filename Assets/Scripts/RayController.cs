using UnityEngine;
using Zenject;

public class RayController : MonoBehaviour
{
    #region Private

    private const float RAY_DISTANCE = 50;

    private Ray ray;
    private RaycastHit hit;

    [Inject]
    private IFigureCreator _figureCreator;

    #endregion // Private

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            try
            {
                CreateRay();

                if (Physics.Raycast(ray, out hit, RAY_DISTANCE))
                {
                    switch (hit.collider.tag)
                    {
                        case "Field":
                            _figureCreator.CreateFigure(hit.point);
                            break;

                        case "Figure":
                            hit.collider.gameObject.GetComponent<GeometryObjectModel>().Click();
                            break;
                    }
                }
            }
            catch
            {
                Debug.Log("Не удалось расчитать Ray");
            }
        }
    }

    private void CreateRay()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}