namespace Chessy.Model.Component
{
    public sealed class WhoLastDiedOnCellC
    {
        public UnitTypes UnitT { get; internal set; }
        public LevelTypes LevelT { get; internal set; }
        public PlayerTypes PlayerT { get; internal set; }

        internal void Clone(in WhoLastDiedOnCellC whoLastDiedOnCellC)
        {
            UnitT = whoLastDiedOnCellC.UnitT;
            LevelT = whoLastDiedOnCellC.LevelT;
            PlayerT = whoLastDiedOnCellC.PlayerT;
        }
    }
}