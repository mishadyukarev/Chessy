using UnityEngine;

internal abstract class StartSpawnManager
{
    protected Camera _camera;
    protected Canvas _canvas;

    internal StartSpawnManager(ResourcesLoadManager resourcesLoadManager)
    {
        _camera = GameObject.Instantiate(resourcesLoadManager.Camera);
    }
}