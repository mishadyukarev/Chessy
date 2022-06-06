using Chessy.Game.Model.Component;

namespace Chessy.Game
{
    public sealed class RiverE
    {
        public RiverTC RiverTC;
        public HaveRiverC HaveRiverC;

        internal RiverE(in bool[] haveRive)
        {
            HaveRiverC = new HaveRiverC(haveRive);
        }
    }
}