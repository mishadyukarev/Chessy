namespace Game.Common
{
    public struct CurSceneC
    {
        public static SceneTypes Scene { get; set; }
        public static bool Is(params SceneTypes[] scenes)
        {
            foreach (var scene in scenes) if (scene == Scene) return true;
            return false;
        }
    }
}