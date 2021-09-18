using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.FillAvailCells
{
    internal sealed class FillCellsForShiftSys : IEcsRunSystem
    {
        private EcsFilter<AvailCellsForShiftComp> _cellsForShiftFilter = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter = default;

        public void Run()
        {
            ref var cellsForShiftCom = ref _cellsForShiftFilter.Get1(0);


            foreach (byte curIdxCell in _xyCellFilter)
            {
                cellsForShiftCom.Clear(true, curIdxCell);
                cellsForShiftCom.Clear(false, curIdxCell);

                ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);
                ref var curBotUnitCom = ref _cellUnitFilter.Get4(curIdxCell);
                ref var curOwnLocalCom = ref _cellUnitFilter.Get3(curIdxCell);


                if (curUnitDatCom.HaveUnit)
                {
                    if (!curBotUnitCom.IsBot)
                    {
                        var isMaster = false;

                        if (curOwnUnitCom.HaveOwner)
                        {
                            isMaster = curOwnUnitCom.IsMasterClient;
                        }
                        else
                        {
                            if (curOwnLocalCom.Is(PlayerTypes.First))
                            {
                                isMaster = true;
                            }
                            else isMaster = false;
                        }

                        var xyCellsAround = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell));

                        foreach (var xy1 in xyCellsAround)
                        {
                            var idxCellAround = _xyCellFilter.GetIdxCell(xy1);

                            if (!_cellEnvDataFilter.Get1(idxCellAround).HaveEnvironment(EnvironmentTypes.Mountain))
                            {
                                if (!_cellUnitFilter.Get1(idxCellAround).HaveUnit)

                                    if (curUnitDatCom.AmountSteps >= _cellEnvDataFilter.Get1(idxCellAround).NeedAmountSteps
                                        || _cellUnitFilter.Get1(curIdxCell).HaveMaxAmountSteps)
                                    {
                                        cellsForShiftCom.AddIdxCell(isMaster, curIdxCell, idxCellAround);
                                    }
                            }
                        }
                    }
                }
            }
        }
    }
}
