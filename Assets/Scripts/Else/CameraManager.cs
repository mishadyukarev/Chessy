using UnityEngine;
using static Main;

internal sealed class CameraManager
{
    private Camera _camera;

    internal CameraManager(ResourcesLoad resourcesLoad)
    {
        _camera = GameObject.Instantiate(resourcesLoad.PrefabConfig.Camera);
        _camera.gameObject.transform.position += new Vector3(7, 5.5f, -2);
    }

    internal void ToggleScene(SceneTypes sceneType)
    {
        switch (sceneType)
        {
            case SceneTypes.Menu:
                break;

            case SceneTypes.Game:
                _camera.transform.rotation = Instance.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
                break;

            default:
                break;
        }
    }
}