namespace Chessy.Common
{
    public struct SceneC
    {
        public SceneTypes Scene;
        public bool Is(params SceneTypes[] scenes)
        {
            foreach (var scene in scenes) if (scene == Scene) return true;
            return false;
        }
    }
}