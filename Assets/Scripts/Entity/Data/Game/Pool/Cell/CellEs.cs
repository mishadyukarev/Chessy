using ECS;

namespace Game.Game
{
    public struct CellEs
    {
        public readonly byte Idx;

        public bool IsActiveParentSelf;

        public readonly CellE CellE;

        public CellWhoLastDiedHereE WhoLastDiedHereE;

        public CellUnitEs UnitEs;
        public CellBuildingE BuildE;
        public CellEnvironmentEs EnvironmentEs;
        public CellEffectE EffectEs;
        public CellRiverEs RiverEs;
        public CellTrailEs TrailEs;

        internal CellEs(in bool isActiveParentCell, in int idCell, byte[] xy, in byte idx, in EcsWorld gameW) : this()
        {
            Idx = idx;

            IsActiveParentSelf = isActiveParentCell;
            CellE = new CellE(xy, idCell);

            BuildE = new CellBuildingE((byte)PlayerTypes.End);
            TrailEs = new CellTrailEs(gameW);
            UnitEs = new CellUnitEs(this, gameW);
            RiverEs = new CellRiverEs(gameW);
        }
    }
}