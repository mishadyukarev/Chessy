using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.FillAvailCells
{
    internal sealed class FillCellsForAttackPawnSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;

        private EcsFilter<CellsForAttackCom> _cellsForAttackFilter = default;

        public void Run()
        {
            foreach (byte curIdx in _xyCellFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdx);
                ref var curOnUnitCom = ref _cellUnitFilter.Get2(curIdx);

                ref var cellsAttackCom = ref _cellsForAttackFilter.Get1(0);


                if (curUnitDatCom.HaveUnit && curUnitDatCom.IsUnit(UnitTypes.Pawn))
                {
                    DirectTypes curDurect1 = default;

                    foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdx)))
                    {
                        curDurect1 += 1;
                        var idxAround = _xyCellFilter.GetIdxCell(xy1);

                        ref var aroEnvrDatCom = ref _cellEnvDataFilter.Get1(idxAround);
                        ref var aroUnitDatCom = ref _cellUnitFilter.Get1(idxAround);
                        ref var aroOwnUnitCom = ref _cellUnitFilter.Get2(idxAround);

                        if (!aroEnvrDatCom.HaveEnvir(EnvirTypes.Mountain))
                        {
                            if (aroEnvrDatCom.NeedAmountSteps <= curUnitDatCom.AmountSteps || curUnitDatCom.HaveMaxAmountSteps)
                            {
                                if (aroUnitDatCom.HaveUnit)
                                {
                                    if (!aroOwnUnitCom.IsPlayerType(curOnUnitCom.PlayerType))
                                    {
                                        if (curDurect1 == DirectTypes.Left || curDurect1 == DirectTypes.Right
                                            || curDurect1 == DirectTypes.Up || curDurect1 == DirectTypes.Down)
                                        {
                                            cellsAttackCom.Add(curOnUnitCom.PlayerType, AttackTypes.Simple, curIdx, idxAround);
                                        }
                                        else cellsAttackCom.Add(curOnUnitCom.PlayerType, AttackTypes.Unique, curIdx, idxAround);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
