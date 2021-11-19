namespace Game.Game
{
    public struct CloudC
    {
        public bool Have { get; set; }
        //public CloudWidthTypes CloudWidth { get; set; }

        //public bool IsCenter => CloudWidth != default;

        public void Sync(bool haveCloud/*, CloudWidthTypes cloudWidth*/)
        {
            Have = haveCloud;
            //CloudWidth = cloudWidth;
        }
    }
}