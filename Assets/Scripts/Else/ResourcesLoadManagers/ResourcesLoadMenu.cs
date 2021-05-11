using UnityEngine;

internal class ResourcesLoadMenu : ResourcesLoad
{
    internal ResourcesLoadMenu()
    {
        _canvas = Resources.Load<Canvas>("CanvasMenu");
    }
}
