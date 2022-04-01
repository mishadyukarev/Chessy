using UnityEditor;
using UnityEngine;

namespace Chessy.Common.Interface
{
    public interface IToggleScene
    {
        void ToggleScene(in SceneTypes newSceneT);
    }
}