using static Game.Game.CellUnitEs;

namespace Game.Game
{
    public struct GetCellsForAttackArcherS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in CellEs.Idxs)
            {
                ref var unit_0 = ref Unit<UnitTC>(idx_0);
                ref var level_0 = ref Unit<LevelTC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);
                ref var stepUnit_0 = ref CellUnitStepEs.Steps<AmountC>(idx_0);
                ref var stunUnit_0 = ref CellUnitStunEs.StepsForExitStun<AmountC>(idx_0);
                ref var corner_0 = ref Unit<IsCornedArcherC>(idx_0);

                if (!stunUnit_0.Have)
                {
                    if (CellUnitStepEs.HaveMin(idx_0))
                    {
                        if (unit_0.Is(UnitTypes.Archer, UnitTypes.Elfemale))
                        {
                            var xy_from = CellEs.Cell<XyC>(idx_0).Xy;

                            for (var dir_1 = DirectTypes.First; dir_1 < DirectTypes.End; dir_1++)
                            {
                                var xy_1 = CellSpaceC.GetXyCellByDirect(xy_from, dir_1);
                                var idx_1 = CellEs.IdxCell(xy_1);


                                ref var unit_1 = ref Unit<UnitTC>(idx_1);
                                ref var ownUnit_1 = ref Unit<PlayerTC>(idx_1);




                                if (CellEs.Cell<IsActiveC>(idx_1).IsActive && !CellEnvironmentEs.Environment<HaveEnvironmentC>(EnvironmentTypes.Mountain, idx_1).Have)
                                {
                                    if (unit_1.Have)
                                    {
                                        if (!ownUnit_1.Is(ownUnit_0.Player))
                                        {
                                            if (unit_0.Is(UnitTypes.Archer))
                                            {
                                                if (corner_0.IsCornered)
                                                {
                                                    if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Down)
                                                    {
                                                        CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, ownUnit_0.Player).Add(idx_1);
                                                    }
                                                    else CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, ownUnit_0.Player).Add(idx_1);
                                                }
                                                else
                                                {
                                                    if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                    {
                                                        CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, ownUnit_0.Player).Add(idx_1);
                                                    }
                                                    else CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, ownUnit_0.Player).Add(idx_1);
                                                }
                                            }
                                        }
                                    }


                                    var xy_2 = CellSpaceC.GetXyCellByDirect(xy_1, dir_1);
                                    var idx_2 = CellEs.IdxCell(xy_2);


                                    ref var unit_2 = ref Unit<UnitTC>(idx_2);
                                    ref var ownUnit_2 = ref Unit<PlayerTC>(idx_2);



                                    if (CellEs.Cell<IsActiveC>(idx_2).IsActive && unit_2.Have
                                        && Unit<IsVisibleC>(ownUnit_0.Player, idx_2).IsVisible && !ownUnit_2.Is(ownUnit_0.Player))
                                    {
                                        if (unit_0.Is(UnitTypes.Archer))
                                        {
                                            if (corner_0.IsCornered)
                                            {
                                                if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                {
                                                    CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, ownUnit_0.Player).Add(idx_2);
                                                }

                                                else CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, ownUnit_0.Player).Add(idx_2);
                                            }
                                            else
                                            {
                                                if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
                                                {
                                                    CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, ownUnit_0.Player).Add(idx_2);
                                                }

                                                else CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, ownUnit_0.Player).Add(idx_2);
                                            }
                                        }
                                        else
                                        {
                                            CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, ownUnit_0.Player).Add(idx_2);
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