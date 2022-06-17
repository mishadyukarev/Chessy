namespace Chessy.Game
{
    readonly struct DamageUIE
    {
        internal readonly ImageUIC ImageC;
        internal readonly TextUIC TextC;
        internal readonly ButtonUIC ButtonC;

        internal DamageUIE(in ImageUIC imageC, in TextUIC textC, in ButtonUIC buttonC)
        {
            ImageC = imageC;
            TextC = textC;
            ButtonC = buttonC;
        }
    }
}