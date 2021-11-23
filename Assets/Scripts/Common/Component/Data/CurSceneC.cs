namespace Game.Common
{
    public struct CurSceneC
    {
        private static SceneTypes _scene;

        public static SceneTypes Scene => _scene;
        public static bool Is(params SceneTypes[] scenes)
        {
            foreach (var scene in scenes) if (scene == Scene) return true;
            return false;
        }


        public static void Set(SceneTypes scene) => _scene = scene;
    }
}