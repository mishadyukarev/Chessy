namespace Game.Game
{
    sealed class GetCellsForAttackArcherS : SystemCellAbstract, IEcsRunSystem
    {
        public GetCellsForAttackArcherS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in Es.CellEs.Idxs)
            {
                ref var unit_0 = ref UnitEs.Main(idx_0).UnitC;
                ref var level_0 = ref UnitEs.Main(idx_0).LevelC;
                ref var ownUnit_0 = ref UnitEs.Main(idx_0).OwnerC;
                ref var stepUnit_0 = ref UnitEs.StatEs.Step(idx_0).Steps;
                ref var stunUnit_0 = ref UnitEs.Stun(idx_0).ForExitStun;
                ref var corner_0 = ref UnitEs.Main(idx_0).IsCorned;

                if (!stunUnit_0.Have)
                {
                    if (UnitEs.StatEs.Step(idx_0).Steps.Have)
                    {
                        if (unit_0.Is(UnitTypes.Archer, UnitTypes.Elfemale, UnitTypes.Snowy))
                        {
                            var xy_from = Es.CellEs.CellE(idx_0).XyC.Xy;

                            for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                            {
                                var xy_1 = Es.CellEs.GetXyCellByDirect(xy_from, dir_1);
                                var idx_1 = Es.CellEs.GetIdxCell(xy_1);


                                ref var unit_1 = ref UnitEs.Main(idx_1).UnitC;
                                ref var ownUnit_1 = ref UnitEs.Main(idx_1).OwnerC;




                                if (Es.CellEs.ParentE(idx_1).IsActiveSelf.IsActive && !Es.CellEs.EnvironmentEs.Mountain(idx_1).HaveEnvironment)
                                {
                                    if (unit_1.Have)
                                    {
                                        if (!ownUnit_1.Is(ownUnit_0.Player))
                                        {
                                            if (unit_0.Is(UnitTypes.Archer))
                                            {
                                                if (corner_0.Is)
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


                                    var xy_2 = Es.CellEs.GetXyCellByDirect(xy_1, dir_1);
                                    var idx_2 = Es.CellEs.GetIdxCell(xy_2);


                                    ref var unit_2 = ref UnitEs.Main(idx_2).UnitC;
                                    ref var ownUnit_2 = ref UnitEs.Main(idx_2).OwnerC;



                                    if (unit_2.Have && !unit_2.IsAnimal
                                        && UnitEs.VisibleE(ownUnit_0.Player, idx_2).VisibleC.IsVisible
                                        && !ownUnit_2.Is(ownUnit_0.Player))
                                    {
                                        if (unit_0.Is(UnitTypes.Archer))
                                        {
                                            if (corner_0.Is)
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