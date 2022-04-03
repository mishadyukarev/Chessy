using Chessy.Game.Model.Component;

namespace Chessy.Game
{
    public struct RiverE
    {
        public RiverTC RiverTC;
        public HaveRiverC HaveRiverC;

        internal RiverE(in bool[] haveRive) : this()
        {
            HaveRiverC = new HaveRiverC(haveRive);
        }
    }
}