namespace Chessy.Game.Entity.Model.Cell.Unit
{
    public struct WhoLastDiedHereE
    {
        public UnitTC UnitTC;
        public LevelTC LevelTC;
        public PlayerTC PlayerTC;

        public void Set(in CellUnitMainE unitMainE)
        {
            UnitTC = unitMainE.UnitTC;
            LevelTC = unitMainE.LevelTC;
            PlayerTC = unitMainE.PlayerTC;
        }
    }
}