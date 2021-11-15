﻿using Leopotam.Ecs;
using System;

namespace Chessy.Game
{
    public sealed class GetAttackArcherCellsSs : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyF = default;
        private EcsFilter<CellC> _cellF = default;
        private EcsFilter<EnvC> _envF = default;
  
        private EcsFilter<UnitC, OwnerC, VisibleC> _unitF = default;
        private EcsFilter<StepC> _statUnitF = default;
        private EcsFilter<StunC> _effUnitF = default;
        private EcsFilter<CornerArcherC> _archerFilt = default;

        public void Run()
        {
            foreach (byte idx_0 in _xyF)
            {
                var xy_0 = _xyF.Get1(idx_0).Xy;

                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var ownUnit_0 = ref _unitF.Get2(idx_0);

                ref var step_0 = ref _statUnitF.Get1(idx_0);     
                ref var stun_0 = ref _effUnitF.Get1(idx_0);
                ref var corner_0 = ref _archerFilt.Get1(idx_0);


                if (stun_0.IsStunned || !unit_0.Is(new[] { UnitTypes.Archer, UnitTypes.Elfemale }) || !step_0.HaveMinSteps) continue;


                for (var dir_1 = (DirectTypes)1; dir_1 < (DirectTypes)Enum.GetNames(typeof(DirectTypes)).Length; dir_1++)
                {
                    var xy_1 = CellSpace.GetXyCellByDirect(xy_0, dir_1);
                    var idx_1 = _xyF.GetIdxCell(xy_1);


                    ref var env_1 = ref _envF.Get1(idx_1);
                    ref var unit_1 = ref _unitF.Get1(idx_1);
                    ref var ownUnit_1 = ref _unitF.Get2(idx_1);




                    if (_cellF.Get1(idx_1).IsActiveCell && !env_1.Have(EnvTypes.Mountain))
                    {
                        if (unit_1.HaveUnit)
                        {
                            if (!ownUnit_1.Is(ownUnit_0.Owner))
                            {
                                if (unit_0.Is(UnitTypes.Archer))
                                {
                                    if (corner_0.IsCornered)
                                    {
                                        if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Down)
                                        {
                                            CellsAttackC.Add(ownUnit_0.Owner, AttackTypes.Unique, idx_0, idx_1);
                                        }
                                        else CellsAttackC.Add(ownUnit_0.Owner, AttackTypes.Simple, idx_0, idx_1);
                                    }
                                    else
                                    {
                                        if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                        {
                                            CellsAttackC.Add(ownUnit_0.Owner, AttackTypes.Unique, idx_0, idx_1);
                                        }
                                        else CellsAttackC.Add(ownUnit_0.Owner, AttackTypes.Simple, idx_0, idx_1);
                                    }
                                }
                                else
                                {
                                    CellsAttackC.Add(ownUnit_0.Owner, AttackTypes.Simple, idx_0, idx_1);
                                }
                            }
                        }


                        var xy_2 = CellSpace.GetXyCellByDirect(xy_1, dir_1);
                        var idx_2 = _xyF.GetIdxCell(xy_2);


                        ref var envrDataCom_2 = ref _envF.Get1(idx_2);
                        ref var unitDataCom_2 = ref _unitF.Get1(idx_2);
                        ref var ownUnitCom_2 = ref _unitF.Get2(idx_2);
                        ref var visUnit_2 = ref _unitF.Get3(idx_2);



                        if (_cellF.Get1(idx_2).IsActiveCell && unitDataCom_2.HaveUnit 
                            && visUnit_2.IsVisibled(ownUnit_0.Owner) && !ownUnitCom_2.Is(ownUnit_0.Owner))
                        {
                            if (unit_0.Is(UnitTypes.Archer))
                            {
                                if (corner_0.IsCornered)
                                {
                                    if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                    {
                                        CellsAttackC.Add(ownUnit_0.Owner, AttackTypes.Simple, idx_0, idx_2);
                                    }

                                    else
                                    {
                                        CellsAttackC.Add(ownUnit_0.Owner, AttackTypes.Unique, idx_0, idx_2);
                                    }
                                }
                                else
                                {
                                    if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
                                    {
                                        CellsAttackC.Add(ownUnit_0.Owner, AttackTypes.Simple, idx_0, idx_2);
                                    }

                                    else
                                    {
                                        CellsAttackC.Add(ownUnit_0.Owner, AttackTypes.Unique, idx_0, idx_2);
                                    }
                                }
                            }
                            else
                            {
                                CellsAttackC.Add(ownUnit_0.Owner, AttackTypes.Simple, idx_0, idx_2);
                            }
                        }
                    }
                }
            }
        }
    }
}