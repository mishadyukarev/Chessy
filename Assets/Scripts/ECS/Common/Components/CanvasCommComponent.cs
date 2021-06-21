using System;
using UnityEngine;

internal struct CanvasCommComponent
{
    private Canvas _canvas;
    private GameObject _inMenuZoneGO;
    private GameObject _inGameZoneGO;

    internal void SetCanvas(Canvas canvas)
    {
        _canvas = canvas;
        _inMenuZoneGO = _canvas.transform.Find("InMenuZone").gameObject;
        _inGameZoneGO = _canvas.transform.Find("InGameZone").gameObject;
        GameObject.Destroy(_inMenuZoneGO);
        GameObject.Destroy(_inGameZoneGO);
    }
    internal void SetZoneUI(SceneTypes sceneType, ResourcesCommComponent resourcesCommComponent)
    {
        switch (sceneType)
        {
            case SceneTypes.Menu:
                _inMenuZoneGO = GameObject.Instantiate(resourcesCommComponent.InMenuZoneGO, _canvas.transform);
                break;

            case SceneTypes.Game:
                _inGameZoneGO = GameObject.Instantiate(resourcesCommComponent.InGameZoneGO, _canvas.transform);
                break;

            default:
                break;
        }
    }

    internal void DestroyZoneUI(SceneTypes sceneType)
    {
        switch (sceneType)
        {
            case SceneTypes.Menu:
                GameObject.Destroy(_inMenuZoneGO);
                break;

            case SceneTypes.Game:
                GameObject.Destroy(_inGameZoneGO);
                break;

            default:
                break;
        }
    }

    internal T FindUnderParent<T>(SceneTypes sceneType, string nameGO)
    {
        switch (sceneType)
        {
            case SceneTypes.Menu:
                return _inMenuZoneGO.transform.Find(nameGO).GetComponent<T>();

            case SceneTypes.Game:
                return _inGameZoneGO.transform.Find(nameGO).GetComponent<T>();

            default:
                throw new Exception();
        }
    }

    internal GameObject FindUnderParent(SceneTypes sceneType, string nameGO)
    {
        switch (sceneType)
        {
            case SceneTypes.Menu:
                return _inMenuZoneGO.transform.Find(nameGO).gameObject;

            case SceneTypes.Game:
                return _inGameZoneGO.transform.Find(nameGO).gameObject;

            default:
                throw new Exception();
        }
    }
}
