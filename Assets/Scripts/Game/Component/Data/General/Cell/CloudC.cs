namespace Game.Game
{
    public struct CloudC : ICloudCell
    {
        public bool Have { get; set; }

        public void Sync(bool haveCloud)
        {
            Have = haveCloud;
        }
    }
}