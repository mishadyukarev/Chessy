using ECS;

namespace Game.Game
{
    public sealed class CellEs
    {
        public readonly byte Idx;

        public readonly CellParenE ParentE;
        public readonly CellE CellE;

        public readonly CellUnitEs UnitEs;
        public readonly CellBuildingEs BuildEs;
        public readonly CellEnvironmentEs EnvironmentEs;
        public readonly CellTrailEs TrailEs;
        public readonly CellEffectEs EffectEs;
        public readonly CellRiverEs RiverEs;

        public CellUnitE UnitE => UnitEs.UnitE;
        public UnitTC UnitC => UnitE.UnitTC;

        internal CellEs(in bool isActiveParentCell, in int idCell, byte[] xy, in byte idx, in EcsWorld gameW)
        {
            Idx = idx;

            ParentE = new CellParenE(isActiveParentCell, gameW);
            CellE = new CellE(gameW, xy, idCell);

            BuildEs = new CellBuildingEs(this, gameW);
            TrailEs = new CellTrailEs(gameW);
            UnitEs = new CellUnitEs(this, idx, gameW);
            EnvironmentEs = new CellEnvironmentEs(this, gameW);
            EffectEs = new CellEffectEs(this, gameW);
            RiverEs = new CellRiverEs(gameW);
        }
    }
}