using UnityEngine;

internal abstract class ResourcesLoad
{
    protected Camera _camera;
    protected Canvas _canvas;

    internal Camera Camera => _camera;
    internal Canvas Canvas => _canvas;

    internal ResourcesLoad()
    {
        _camera = Resources.Load<Camera>("Camera");
    }
}
