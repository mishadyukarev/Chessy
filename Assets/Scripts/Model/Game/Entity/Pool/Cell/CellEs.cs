using Chessy.Game.Entity;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game
{
    public struct CellEs
    {
        public CellE CellE;
        public AroundCellsE AroundCellsEs;
        public UnitEs UnitEs;
        public BuildingE BuildEs;
        public EnvironmentE EnvironmentEs;
        public EffectE EffectEs;
        public RiverE RiverEs;
        public TrailE TrailE;

        internal CellEs(in bool[] isActiveParents, in int idCell, in byte idx, in EntitiesModelGame eMGame, params byte[] xy) : this()
        {
            CellE = new CellE(isActiveParents[idx], idx, idCell, xy);
            AroundCellsEs = new AroundCellsE(idx, isActiveParents,  eMGame, xy);
            UnitEs = new UnitEs(default);
            BuildEs = new BuildingE(default);
            RiverEs = new RiverE(new bool[(byte)DirectTypes.End]);
            TrailE = new TrailE(default);
        }
    }
}