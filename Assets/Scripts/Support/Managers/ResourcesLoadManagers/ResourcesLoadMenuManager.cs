using UnityEngine;

internal class ResourcesLoadMenuManager : ResourcesLoadManager
{
    internal ResourcesLoadMenuManager()
    {
        _canvas = Resources.Load<Canvas>("CanvasMenu");
    }
}
