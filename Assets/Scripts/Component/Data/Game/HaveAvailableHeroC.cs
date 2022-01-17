namespace Game.Game
{
    public struct HaveAvailableHeroC : IAvailableHeroE
    {
        public bool Have;

        public HaveAvailableHeroC(in bool have) => Have = have;
    }
}