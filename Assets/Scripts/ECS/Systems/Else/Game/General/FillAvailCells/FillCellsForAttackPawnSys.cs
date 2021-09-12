using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.FillAvailCells
{
    internal sealed class FillCellsForAttackPawnSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;

        private EcsFilter<AvailCellsForAttackComp> _availCellsForAttackFilter = default;

        public void Run()
        {
            foreach (byte curIdxCell in _xyCellFilter)
            {
                ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOwnerCellUnitCom = ref _cellUnitFilter.Get2(curIdxCell);

                ref var availCellsForAttackComp = ref _availCellsForAttackFilter.Get1(0);


                if (curCellUnitDataCom.HaveUnit && curCellUnitDataCom.IsUnitType(UnitTypes.Pawn))
                {
                    if (curOwnerCellUnitCom.HaveOwner)
                    {
                        DirectTypes curDurect1 = default;

                        foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell)))
                        {
                            curDurect1 += 1;
                            var idxCellAround = _xyCellFilter.GetIdxCell(xy1);

                            ref var arouCellEnvrDataCom = ref _cellEnvDataFilter.Get1(idxCellAround);
                            ref var arouCellUnitDataCom = ref _cellUnitFilter.Get1(idxCellAround);
                            ref var arouOwnerCellUnitCom = ref _cellUnitFilter.Get2(idxCellAround);

                            if (!arouCellEnvrDataCom.HaveEnvironment(EnvironmentTypes.Mountain))
                            {
                                if (arouCellEnvrDataCom.NeedAmountSteps <= curCellUnitDataCom.AmountSteps || curCellUnitDataCom.HaveMaxAmountSteps)
                                {
                                    if (arouCellUnitDataCom.HaveUnit)
                                    {
                                        if (arouOwnerCellUnitCom.HaveOwner)
                                        {
                                            if (!arouOwnerCellUnitCom.IsHim(curOwnerCellUnitCom.Owner))
                                            {
                                                if (curDurect1 == DirectTypes.Left || curDurect1 == DirectTypes.Right
                                                    || curDurect1 == DirectTypes.Up || curDurect1 == DirectTypes.Down)
                                                {
                                                    availCellsForAttackComp.Add(AttackTypes.Simple, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround);
                                                }
                                                else availCellsForAttackComp.Add(AttackTypes.Unique, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround);
                                            }
                                        }

                                        else
                                        {
                                            if (curDurect1 == DirectTypes.Left || curDurect1 == DirectTypes.Right
                                                || curDurect1 == DirectTypes.Up || curDurect1 == DirectTypes.Down)
                                            {
                                                availCellsForAttackComp.Add(AttackTypes.Simple, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround);
                                            }
                                            else availCellsForAttackComp.Add(AttackTypes.Unique, curOwnerCellUnitCom.IsMasterClient, curIdxCell, idxCellAround);
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
}
