using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class FillCellsArsonSys : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyCellFilter = default;
        private EcsFilter<EnvC> _cellEnvFilter = default;
        private EcsFilter<FireC> _cellFireFilter = default;

        private EcsFilter<UnitC, OwnerC> _unitF = default;
        private EcsFilter<StunC> _effUnitF = default;
        private EcsFilter<FireC> _fireF = default;

        public void Run()
        {
            foreach (byte curIdxCell in _cellEnvFilter)
            {
                var curXy = _xyCellFilter.Get1(curIdxCell).Xy;

                ref var curUnitDatCom = ref _unitF.Get1(curIdxCell);
                ref var curOwnUnitCom = ref _unitF.Get2(curIdxCell);
                ref var stunUnit_0 = ref _effUnitF.Get1(curIdxCell);

                if (!stunUnit_0.IsStunned)
                {
                    if (curUnitDatCom.Is(UnitTypes.Archer))
                    {
                        foreach (var arouXy in CellSpace.GetXyAround(curXy))
                        {
                            var arouIdx = _xyCellFilter.GetIdxCell(arouXy);

                            ref var arounEnvDatCom = ref _cellEnvFilter.Get1(arouIdx);

                            if (!_cellFireFilter.Get1(arouIdx).Have)
                            {
                                if (arounEnvDatCom.Have(EnvTypes.AdultForest))
                                {
                                    CellsArsonArcherComp.Add(curOwnUnitCom.Owner, curIdxCell, arouIdx);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
