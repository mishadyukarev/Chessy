namespace Game.Common
{
    public struct CurSceneC
    {
        public static SceneTypes Scene;
        public static bool Is(params SceneTypes[] scenes)
        {
            foreach (var scene in scenes) if (scene == Scene) return true;
            return false;
        }
    }
}