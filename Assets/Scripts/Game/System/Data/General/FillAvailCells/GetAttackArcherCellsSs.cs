using Leopotam.Ecs;
using System;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class GetAttackArcherCellsSs : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                var xy_0 = Cell<XyC>(idx_0).Xy;

                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);

                ref var stepUnit_0 = ref Unit<StepUnitWC>(idx_0);     
                ref var stun_0 = ref Unit<StunC>(idx_0);
                ref var corner_0 = ref Unit<CornerArcherC>(idx_0);


                if (stun_0.IsStunned || !unit_0.Is(UnitTypes.Archer, UnitTypes.Elfemale) || !stepUnit_0.HaveMin) continue;


                for (var dir_1 = DirectTypes.First; dir_1 < DirectTypes.End; dir_1++)
                {
                    var xy_1 = CellSpaceC.GetXyCellByDirect(xy_0, dir_1);
                    var idx_1 = IdxCell(xy_1);


                    ref var env_1 = ref Environment<EnvC>(idx_1);
                    ref var unit_1 = ref Unit<UnitC>(idx_1);
                    ref var ownUnit_1 = ref Unit<OwnerC>(idx_1);




                    if (Cell<CellC>(idx_1).IsActiveCell && !env_1.Have(EnvTypes.Mountain))
                    {
                        if (unit_1.Have)
                        {
                            if (!ownUnit_1.Is(ownUnit_0.Owner))
                            {
                                if (unit_0.Is(UnitTypes.Archer))
                                {
                                    if (corner_0.IsCornered)
                                    {
                                        if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Down)
                                        {
                                            AttackCellsC.Add(AttackTypes.Unique, ownUnit_0.Owner,  idx_0, idx_1);
                                        }
                                        else AttackCellsC.Add(AttackTypes.Simple, ownUnit_0.Owner,  idx_0, idx_1);
                                    }
                                    else
                                    {
                                        if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                        {
                                            AttackCellsC.Add(AttackTypes.Unique, ownUnit_0.Owner,  idx_0, idx_1);
                                        }
                                        else AttackCellsC.Add(AttackTypes.Simple, ownUnit_0.Owner,  idx_0, idx_1);
                                    }
                                }
                                else
                                {
                                    AttackCellsC.Add(AttackTypes.Simple, ownUnit_0.Owner, idx_0, idx_1);
                                }
                            }
                        }


                        var xy_2 = CellSpaceC.GetXyCellByDirect(xy_1, dir_1);
                        var idx_2 = IdxCell(xy_2);


                        ref var env_2 = ref Environment<EnvC>(idx_2);
                        ref var unit_2 = ref Unit<UnitC>(idx_2);
                        ref var ownUnit_2 = ref Unit<OwnerC>(idx_2);
                        ref var visUnit_2 = ref Unit<VisibleC>(idx_2);



                        if (Cell<CellC>(idx_2).IsActiveCell && unit_2.Have 
                            && visUnit_2.IsVisibled(ownUnit_0.Owner) && !ownUnit_2.Is(ownUnit_0.Owner))
                        {
                            if (unit_0.Is(UnitTypes.Archer))
                            {
                                if (corner_0.IsCornered)
                                {
                                    if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                    {
                                        AttackCellsC.Add(AttackTypes.Simple, ownUnit_0.Owner,  idx_0, idx_2);
                                    }

                                    else
                                    {
                                        AttackCellsC.Add(AttackTypes.Unique, ownUnit_0.Owner,  idx_0, idx_2);
                                    }
                                }
                                else
                                {
                                    if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
                                    {
                                        AttackCellsC.Add(AttackTypes.Simple, ownUnit_0.Owner,  idx_0, idx_2);
                                    }

                                    else
                                    {
                                        AttackCellsC.Add(AttackTypes.Unique, ownUnit_0.Owner,  idx_0, idx_2);
                                    }
                                }
                            }
                            else
                            {
                                AttackCellsC.Add(AttackTypes.Simple, ownUnit_0.Owner,  idx_0, idx_2);
                            }
                        }
                    }
                }
            }
        }
    }
}
