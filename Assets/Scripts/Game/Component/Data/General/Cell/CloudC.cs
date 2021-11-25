namespace Game.Game
{
    public struct CloudC : IElseCell
    {
        public bool Have { get; set; }

        public void Sync(bool haveCloud)
        {
            Have = haveCloud;
        }
    }
}