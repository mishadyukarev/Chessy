﻿using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class FillCellsArsonSys : IEcsRunSystem
    {
        private EcsFilter<CellsArsonArcherComp> _cellsArsonFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;
        private EcsFilter<CellFireDataC> _cellFireFilter = default;

        private EcsFilter<CellUnitDataC, OwnerC> _cellUnitFilter = default;
        private EcsFilter<CellFireDataC, StunC> _unitEffFilt = default;

        public void Run()
        {
            ref var cellsArsonCom = ref _cellsArsonFilter.Get1(0);

            foreach (byte curIdxCell in _cellEnvFilter)
            {
                var curXy = _xyCellFilter.Get1(curIdxCell).XyCell;

                ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);
                ref var stunUnit_0 = ref _unitEffFilt.Get2(curIdxCell);

                if (!stunUnit_0.IsStunned)
                {
                    if (curUnitDatCom.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                    {
                        foreach (var arouXy in CellSpaceSupport.GetXyAround(curXy))
                        {
                            var arouIdx = _xyCellFilter.GetIdxCell(arouXy);

                            ref var arounEnvDatCom = ref _cellEnvFilter.Get1(arouIdx);

                            if (!_cellFireFilter.Get1(arouIdx).HaveFire)
                            {
                                if (arounEnvDatCom.Have(EnvTypes.AdultForest))
                                {
                                    cellsArsonCom.Add(curOwnUnitCom.Owner, curIdxCell, arouIdx);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
