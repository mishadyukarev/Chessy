using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.FillAvailCells
{
    internal sealed class FillCellsForShiftSys : IEcsRunSystem
    {
        private EcsFilter<CellsForShiftCom> _cellsForShiftFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            ref var cellsForShiftCom = ref _cellsForShiftFilter.Get1(0);


            foreach (byte curIdxCell in _xyCellFilter)
            {
                cellsForShiftCom.Clear(PlayerTypes.First, curIdxCell);
                cellsForShiftCom.Clear(PlayerTypes.Second, curIdxCell);

                ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);


                if (curUnitDatCom.HaveUnit)
                {
                    var xyCellsAround = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell));

                    foreach (var xy1 in xyCellsAround)
                    {
                        var idxCellAround = _xyCellFilter.GetIdxCell(xy1);

                        if (!_cellEnvDataFilter.Get1(idxCellAround).HaveEnvir(EnvirTypes.Mountain))
                        {
                            if (!_cellUnitFilter.Get1(idxCellAround).HaveUnit)

                                if (curUnitDatCom.AmountSteps >= _cellEnvDataFilter.Get1(idxCellAround).NeedAmountSteps
                                    || _cellUnitFilter.Get1(curIdxCell).HaveMaxAmountSteps)
                                {
                                    cellsForShiftCom.AddIdxCell(curOwnUnitCom.PlayerType, curIdxCell, idxCellAround);
                                }
                        }
                    }
                }
            }
        }
    }
}
