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
    internal sealed class FillCellsForAttackKingSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter = default;

        private EcsFilter<CellsForAttackCom> _availCellsForAttackFilter = default;

        public void Run()
        {
            foreach (byte curIdxCell in _xyCellFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);
                ref var curOffUnitCom = ref _cellUnitFilter.Get3(curIdxCell);

                ref var cellsAttackCom = ref _availCellsForAttackFilter.Get1(0);


                if (curUnitDatCom.HaveUnit)
                {
                    if (curOnUnitCom.HaveOwner || curOffUnitCom.HaveLocalPlayer)
                    {
                        if (curUnitDatCom.IsUnit(UnitTypes.King))
                        {
                            DirectTypes curDurect1 = default;

                            foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdxCell)))
                            {
                                curDurect1 += 1;
                                var idxCellAround = _xyCellFilter.GetIdxCell(xy1);

                                ref var arouEnvrDatCom = ref _cellEnvDataFilter.Get1(idxCellAround);
                                ref var arouUnitDatCom = ref _cellUnitFilter.Get1(idxCellAround);
                                ref var arouOnUnitCom = ref _cellUnitFilter.Get2(idxCellAround);
                                ref var aroOffUnitCom = ref _cellUnitFilter.Get3(idxCellAround);

                                if (!arouEnvrDatCom.HaveEnvir(EnvirTypes.Mountain))
                                {
                                    if (arouEnvrDatCom.NeedAmountSteps <= curUnitDatCom.AmountSteps || curUnitDatCom.HaveMaxAmountSteps)
                                    {
                                        if (arouUnitDatCom.HaveUnit)
                                        {
                                            if (arouOnUnitCom.HaveOwner)
                                            {
                                                if (!arouOnUnitCom.IsHim(curOnUnitCom.Owner))
                                                {
                                                    cellsAttackCom.Add(AttackTypes.Simple, curOnUnitCom.IsMasterClient, curIdxCell, idxCellAround);
                                                }
                                            }

                                            else if (aroOffUnitCom.HaveLocalPlayer)
                                            {
                                                if(aroOffUnitCom.IsMainMaster != curOffUnitCom.IsMainMaster)
                                                {
                                                    cellsAttackCom.Add(AttackTypes.Simple, curOffUnitCom.IsMainMaster, curIdxCell, idxCellAround);
                                                }
                                            }

                                            else
                                            {
                                                cellsAttackCom.Add(AttackTypes.Simple, curOffUnitCom.IsMainMaster, curIdxCell, idxCellAround);
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
}
