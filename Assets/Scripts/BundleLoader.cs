using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Newtonsoft.Json;

public class BundleLoader : MonoBehaviour
{
    #region Private

    private string path;
    private string bundleURL = "https://lemnian-whirls.000webhostapp.com/figures.unity3d";
    private int version = 15;

    [Inject]
    private IFigureCreator _figureCreator;

    private FigureNames figures;

    #endregion // Private

    private void Start()
    {
        UpdatePath();

        figures = LoadFigureNames();

        DownloadBundle();
    }

    private void UpdatePath()
    {
        path = Application.dataPath + "/Resources/figureNames.json";
    }

    private FigureNames LoadFigureNames()
    {
        string jsonData = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<FigureNames>(jsonData);
    }

    #region Download

    private void DownloadBundle()
    {
        try
        {
            while (!Caching.ready) { }

            WWW www = WWW.LoadFromCacheOrDownload(bundleURL, version);
            while (!www.isDone) { }

            AssetBundle assetBundle = www.assetBundle;

            Debug.Log("Ассет Бандлы загружены");

            DownloadFigures(assetBundle);
        }
        catch
        {
            Debug.Log("Не удалось загрузить Ассет Бандлы");
        }
    }

    private void DownloadFigures(AssetBundle assetBundle)
    {
        try
        {
            AssetBundleRequest request;

            foreach (string figureName in figures.FigureNamesList)
            {
                request = assetBundle.LoadAssetAsync(figureName, typeof(GameObject));
                _figureCreator.AddFigure(request.asset as GameObject);
            }

            Debug.Log("Фигуры загружены");
        }
        catch
        {
            Debug.Log("Не удалось загрузить фигуры");
        }

        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    #endregion // Download

    private class FigureNames
    {
        public List<string> FigureNamesList { get; set; }
    }
}