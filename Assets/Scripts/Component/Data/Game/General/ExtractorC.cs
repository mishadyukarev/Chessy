namespace Game.Game
{
    public struct ExtractorC
    {
        public static int GetExtractOneBuild(float percUpg)
        {
            var extaction = 10;
            extaction += (int)(extaction * percUpg);
            return extaction;
        }
    }
}