﻿using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class FillCellsArsonSys : IEcsRunSystem
    {
        private EcsFilter<CellsArsonArcherComp> _cellsArsonFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

        public void Run()
        {
            ref var cellsArsonCom = ref _cellsArsonFilter.Get1(0);

            foreach (byte curIdxCell in _cellEnvFilter)
            {
                var curXy = _xyCellFilter.GetXyCell(curIdxCell);

                ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);

                if (curUnitDatCom.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                {
                    foreach (var arouXy in CellSpaceSupport.TryGetXyAround(curXy))
                    {
                        var arouIdx = _xyCellFilter.GetIdxCell(arouXy);

                        ref var arounEnvDatCom = ref _cellEnvFilter.Get1(arouIdx);

                        if (!_cellFireFilter.Get1(arouIdx).HaveFire)
                        {
                            if (arounEnvDatCom.Have(EnvirTypes.AdultForest))
                            {
                                cellsArsonCom.Add(curOwnUnitCom.PlayerType, curIdxCell, arouIdx);
                            }
                        }
                    }
                }
            }
        }
    }
}
