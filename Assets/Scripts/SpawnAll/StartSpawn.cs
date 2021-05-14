using UnityEngine;

internal abstract class StartSpawn
{
    protected Camera _camera;
    protected Canvas _canvas;

    internal StartSpawn(ResourcesLoad resourcesLoadManager)
    {
        _camera = GameObject.Instantiate(resourcesLoadManager.Camera);
    }
}