namespace Chessy.Common
{
    public struct SceneTC
    {
        public SceneTypes SceneT { get; internal set; }

        public bool Is(params SceneTypes[] scenes)
        {
            foreach (var scene in scenes) if (scene == SceneT) return true;
            return false;
        }
    }
}