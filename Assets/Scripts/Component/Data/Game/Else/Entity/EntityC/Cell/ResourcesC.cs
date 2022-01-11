namespace Game.Game
{
    public struct ResourcesC : IEnvCell
    {
        public int Resources;

        public bool Have => Resources > 0;
    }
}