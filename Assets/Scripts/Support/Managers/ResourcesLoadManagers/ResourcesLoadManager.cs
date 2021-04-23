using UnityEngine;

internal abstract class ResourcesLoadManager
{
    protected Camera _camera;
    protected Canvas _canvas;

    internal Camera Camera => _camera;
    internal Canvas Canvas => _canvas;

    internal ResourcesLoadManager()
    {
        _camera = Resources.Load<Camera>("Camera");
    }
}
