namespace Game.Game
{
    public struct HaveUpgradeC : IUpgradeE
    {
        public bool Have;

        public HaveUpgradeC(in bool haveUpgrade) => Have = haveUpgrade;
    }
}