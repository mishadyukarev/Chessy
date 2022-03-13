namespace Chessy.Game.Entity.Model.Cell.Unit
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
        public void Set(in CellUnitMainE unitMainE)
        {
            UnitTC = unitMainE.UnitTC;
            LevelTC = unitMainE.LevelTC;
            PlayerTC = unitMainE.PlayerTC;
        }
    }
}