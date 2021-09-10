using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.FillAvailCells
{
    internal sealed class FillCellsForShiftSys : IEcsRunSystem
    {
        private EcsFilter<AvailCellsForShiftComp> _availCellsForShiftFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;

        public void Run()
        {
            ref var availCellsForShiftComp = ref _availCellsForShiftFilter.Get1(0);


            foreach (byte curIdxCell in _xyCellFilter)
            {
                availCellsForShiftComp.Clear(true, curIdxCell);
                availCellsForShiftComp.Clear(false, curIdxCell);

                ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOwnerCellUnitCom = ref _cellUnitFilter.Get2(curIdxCell);


                if (curCellUnitDataCom.HaveUnit)
                {
                    if (curOwnerCellUnitCom.HaveOwner)
                    {
                        var xyCellsAround = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell));

                        foreach (var xy1 in xyCellsAround)
                        {
                            var idxCellAround = _xyCellFilter.GetIdxCell(xy1);

                            if (!_cellEnvDataFilter.Get1(idxCellAround).HaveEnvironment(EnvironmentTypes.Mountain)
                                && !_cellUnitFilter.Get1(idxCellAround).HaveUnit)
                            {
                                if (curCellUnitDataCom.AmountSteps >= _cellEnvDataFilter.Get1(idxCellAround).NeedAmountSteps || _cellUnitFilter.Get1(curIdxCell).HaveMaxAmountSteps)
                                {
                                    availCellsForShiftComp.AddIdxCell(curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
