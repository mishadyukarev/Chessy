using Leopotam.Ecs;
using System;

namespace Chessy.Game
{
    public sealed class FillCellsForAttackRookSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellDataC> _cellDataFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellUnitDataC, StepComponent, OwnerCom, VisibleC> _cellUnitFilter = default;

        public void Run()
        {
            foreach (byte idxCell_0 in _xyCellFilter)
            {
                var xy_0 = _xyCellFilter.Get1(idxCell_0).XyCell;

                ref var unitDataCom_0 = ref _cellUnitFilter.Get1(idxCell_0);
                ref var stepUnitC_0 = ref _cellUnitFilter.Get2(idxCell_0);
                ref var ownUnitCom_0 = ref _cellUnitFilter.Get3(idxCell_0);


                if (unitDataCom_0.HaveUnit && unitDataCom_0.Is(UnitTypes.Rook))
                {
                    if (stepUnitC_0.HaveMinSteps)

                        for (DirectTypes dirType_1 = (DirectTypes)1; dirType_1 < (DirectTypes)Enum.GetNames(typeof(DirectTypes)).Length; dirType_1++)
                        {
                            var xy_1 = CellSpaceSupport.GetXyCellByDirect(xy_0, dirType_1);
                            var idxCell_1 = _xyCellFilter.GetIdxCell(xy_1);


                            ref var envrDatCom_1 = ref _cellEnvDataFilter.Get1(idxCell_1);
                            ref var unitDatCom_1 = ref _cellUnitFilter.Get1(idxCell_1);
                            ref var ownUnitCom_1 = ref _cellUnitFilter.Get3(idxCell_1);


                            if (_cellDataFilter.Get1(idxCell_1).IsActiveCell)
                            {
                                if (!envrDatCom_1.Have(EnvTypes.Mountain))
                                {
                                    if (unitDatCom_1.HaveUnit)
                                    {
                                        if (!ownUnitCom_1.Is(ownUnitCom_0.Owner))
                                        {
                                            if (dirType_1 == DirectTypes.Left || dirType_1 == DirectTypes.Right || dirType_1 == DirectTypes.Up || dirType_1 == DirectTypes.Down)
                                            {
                                                CellsAttackC.Add(ownUnitCom_0.Owner, AttackTypes.Unique, idxCell_0, idxCell_1);
                                            }
                                            else CellsAttackC.Add(ownUnitCom_0.Owner, AttackTypes.Simple, idxCell_0, idxCell_1);
                                        }
                                    }


                                    var xy_2 = CellSpaceSupport.GetXyCellByDirect(xy_1, dirType_1);
                                    var idxCell_2 = _xyCellFilter.GetIdxCell(xy_2);


                                    ref var envrDataCom_2 = ref _cellEnvDataFilter.Get1(idxCell_2);
                                    ref var unitDataCom_2 = ref _cellUnitFilter.Get1(idxCell_2);
                                    ref var ownUnitCom_2 = ref _cellUnitFilter.Get3(idxCell_2);
                                    ref var visUnitCom_2 = ref _cellUnitFilter.Get4(idxCell_2);



                                    if (unitDataCom_2.HaveUnit)
                                    {
                                        if (visUnitCom_2.IsVisibled(ownUnitCom_0.Owner))
                                        {
                                            if (!ownUnitCom_2.Is(ownUnitCom_0.Owner))
                                            {
                                                if (dirType_1 == DirectTypes.DownLeft || dirType_1 == DirectTypes.UpLeft || dirType_1 == DirectTypes.UpRight || dirType_1 == DirectTypes.DownRight)
                                                {
                                                    CellsAttackC.Add(ownUnitCom_0.Owner, AttackTypes.Simple, idxCell_0, idxCell_2);
                                                }

                                                else
                                                {
                                                    CellsAttackC.Add(ownUnitCom_0.Owner, AttackTypes.Unique, idxCell_0, idxCell_2);
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
