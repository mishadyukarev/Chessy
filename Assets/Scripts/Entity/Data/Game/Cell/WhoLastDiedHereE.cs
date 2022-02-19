namespace Game.Game
{
    public struct WhoLastDiedHereE
    {
        public UnitTC UnitTC;
        public LevelTC LevelTC;
        public PlayerTC PlayerTC;

        public void Set(in UnitTypes unitT, in LevelTypes levT, in PlayerTypes playerT)
        {
            UnitTC.Unit = unitT;
            LevelTC.Level = levT;
            PlayerTC.Player = playerT;
        }
        public void Set(in UnitTC unitTC, in LevelTC levTC, in PlayerTC playerTC)
        {
            UnitTC = unitTC;
            LevelTC = levTC;
            PlayerTC = playerTC;
        }
    }
}