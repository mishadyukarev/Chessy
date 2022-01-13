namespace Game.Game
{
    public struct AmountResourcesC : IEnvCell
    {
        public int Resources;

        public bool Have => Resources > 0;
    }
}