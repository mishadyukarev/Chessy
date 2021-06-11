using UnityEngine;

internal sealed class CanvasManager
{
    private ResourcesLoad _resourcesLoad;
    private Names _names;

    private Canvas _canvas;
    internal GameObject InMenuZoneCanvasGO;
    internal GameObject InGameZoneGO;

    internal CanvasManager(ResourcesLoad resourcesLoad, Names names)
    {
        _resourcesLoad = resourcesLoad;
        _names = names;

        _canvas = GameObject.Instantiate(_resourcesLoad.Canvas);

        GameObject.Destroy(_canvas.transform.Find("InMenuZone").gameObject);
        GameObject.Destroy(_canvas.transform.Find("InGameZone").gameObject);
    }

    internal void ToggleScene(SceneTypes sceneType)
    {
        switch (sceneType)
        {
            case SceneTypes.Menu:
                InMenuZoneCanvasGO = GameObject.Instantiate(_resourcesLoad.InMenuZoneGO, _canvas.transform);
                InMenuZoneCanvasGO.name = _names.InMenuZone;
                break;

            case SceneTypes.Game:
                InGameZoneGO = GameObject.Instantiate(_resourcesLoad.InGameZoneGO, _canvas.transform);
                InGameZoneGO.name = _names.IN_GAME_CANVAS_Zone;
                break;

            default:
                break;
        }
    }
}