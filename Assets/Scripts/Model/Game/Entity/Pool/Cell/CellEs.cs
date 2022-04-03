using Chessy.Game.Entity;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game
{
    public struct CellEs
    {
        public CellE CellE;
        public AroundCellsEs AroundCellsEs;
        public UnitEs UnitEs;
        public BuildingE BuildEs;
        public EnvironmentE EnvironmentEs;
        public EffectE EffectEs;
        public RiverE RiverEs;
        public TrailE TrailE;

        internal CellEs(in bool[] isActiveParents, in int idCell, byte[] xy, in byte idx, in EntitiesModelGame eMGame) : this()
        {
            CellE = new CellE(isActiveParents[idx], idx, xy, idCell);
            AroundCellsEs = new AroundCellsEs(idx, isActiveParents, xy, eMGame);
            UnitEs = new UnitEs(default);
            BuildEs = new BuildingE(default);
            RiverEs = new RiverE(new bool[(byte)DirectTypes.End - 1]);
            TrailE = new TrailE(default);
        }
    }
}