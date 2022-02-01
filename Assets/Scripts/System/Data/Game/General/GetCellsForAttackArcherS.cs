namespace Game.Game
{
    sealed class GetCellsForAttackArcherS : SystemCellAbstract, IEcsRunSystem
    {
        public GetCellsForAttackArcherS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            foreach (var idx_0 in CellEsWorker.Idxs)
            {
                var unit_0 = UnitEs(idx_0).MainE.UnitTC;
                var ownUnit_0 = UnitEs(idx_0).MainE.OwnerC;
                var corner_0 = UnitEs(idx_0).MainE.IsCorned;

                if (!UnitEffectEs(idx_0).StunE.IsStunned)
                {
                    if (UnitStatEs(idx_0).StepE.HaveSteps)
                    {
                        if (unit_0.Is(UnitTypes.Archer, UnitTypes.Elfemale, UnitTypes.Snowy))
                        {
                            var xy_from = CellEs(idx_0).CellE.XyC.Xy;

                            for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                            {
                                var xy_1 = CellEsWorker.GetXyCellByDirect(xy_from, dir_1);
                                var idx_1 = CellEsWorker.GetIdxCell(xy_1);

                                var ownUnit_1 = UnitEs(idx_1).MainE.OwnerC;


                                if (CellEs(idx_1).ParentE.IsActiveSelf.IsActive && !EnvironmentEs(idx_1).Mountain.HaveEnvironment)
                                {
                                    if (UnitEs(idx_1).MainE.HaveUnit(UnitStatEs(idx_1)))
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


                                    var xy_2 = CellEsWorker.GetXyCellByDirect(xy_1, dir_1);
                                    var idx_2 = CellEsWorker.GetIdxCell(xy_2);


                                    var unit_2 = UnitEs(idx_2).MainE.UnitTC;
                                    var ownUnit_2 = UnitEs(idx_2).MainE.OwnerC;



                                    if (UnitEs(idx_2).MainE.HaveUnit(UnitStatEs(idx_2)) && !unit_2.IsAnimal
                                        && UnitEs(idx_2).VisibleE(ownUnit_0.Player).IsVisibleC.IsVisible
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