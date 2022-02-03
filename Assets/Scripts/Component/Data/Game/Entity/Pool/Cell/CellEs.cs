using ECS;

namespace Game.Game
{
    public readonly struct CellEs
    {
        public readonly CellParenE ParentE;
        public readonly CellE CellE;


        public readonly CellUnitEs UnitEs;
        public readonly CellBuildEs BuildEs;
        public readonly CellEnvironmentEs EnvironmentEs;
        public readonly CellTrailEs TrailEs;
        public readonly CellEffectEs EffectEs;
        public readonly CellRiverEs RiverEs;

        public CellEs(in bool isActiveParentCell, in int idCell, byte[] xy, in byte idx, in EcsWorld gameW)
        {
            ParentE = new CellParenE(isActiveParentCell, idx, gameW);
            CellE = new CellE(gameW, xy, idCell);

            BuildEs = new CellBuildEs(idx, gameW);
            TrailEs = new CellTrailEs(gameW);
            UnitEs = new CellUnitEs(idx, gameW);
            EnvironmentEs = new CellEnvironmentEs(idx, gameW);
            EffectEs = new CellEffectEs(idx, gameW);
            RiverEs = new CellRiverEs(gameW);
        }
    }
}