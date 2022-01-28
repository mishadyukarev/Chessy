using static Game.Game.CellUnitEs;

namespace Game.Game
{
    public struct GetCellsForAttackArcherS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in Entities.CellEs.Idxs)
            {
                ref var unit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).UnitC;
                ref var level_0 = ref Entities.CellEs.UnitEs.Else(idx_0).LevelC;
                ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;
                ref var stepUnit_0 = ref Entities.CellEs.UnitEs.Step(idx_0).Steps;
                ref var stunUnit_0 = ref Entities.CellEs.UnitEs.Stun(idx_0).ForExitStun;
                ref var corner_0 = ref Entities.CellEs.UnitEs.Else(idx_0).CornedC;

                if (!stunUnit_0.Have)
                {
                    if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Have)
                    {
                        if (unit_0.Is(UnitTypes.Archer, UnitTypes.Elfemale, UnitTypes.Snowy))
                        {
                            var xy_from = Entities.CellEs.CellE(idx_0).XyC.Xy;

                            for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                            {
                                var xy_1 = CellSpaceSupport.GetXyCellByDirect(xy_from, dir_1);
                                var idx_1 = Entities.CellEs.IdxCell(xy_1);


                                ref var unit_1 = ref Entities.CellEs.UnitEs.Else(idx_1).UnitC;
                                ref var ownUnit_1 = ref Entities.CellEs.UnitEs.Else(idx_1).OwnerC;




                                if (Entities.CellEs.ParentE(idx_1).IsActiveSelf.IsActive && !Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Mountain, idx_1).Resources.Have)
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
                                            else
                                            {
                                                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, ownUnit_0.Player).Add(idx_1);
                                            }
                                        }
                                    }


                                    var xy_2 = CellSpaceSupport.GetXyCellByDirect(xy_1, dir_1);
                                    var idx_2 = Entities.CellEs.IdxCell(xy_2);


                                    ref var unit_2 = ref Entities.CellEs.UnitEs.Else(idx_2).UnitC;
                                    ref var ownUnit_2 = ref Entities.CellEs.UnitEs.Else(idx_2).OwnerC;



                                    if (unit_2.Have && !unit_2.IsAnimal
                                        && Entities.CellEs.UnitEs.VisibleE(ownUnit_0.Player, idx_2).VisibleC.IsVisible
                                        && !ownUnit_2.Is(ownUnit_0.Player))
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