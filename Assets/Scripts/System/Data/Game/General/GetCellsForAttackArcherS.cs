namespace Game.Game
{
    sealed class GetCellsForAttackArcherS : SystemCellAbstract, IEcsRunSystem
    {
        public GetCellsForAttackArcherS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                var ownUnit_0 = UnitEs(idx_0).OwnerE.OwnerC;
                var isCornered_0 = UnitEs(idx_0).CornedE.IsRight;

                if (!UnitEffectEs(idx_0).StunE.IsStunned)
                {
                    if (UnitStatEs(idx_0).StepE.HaveSteps)
                    {
                        if (Es.UnitTypeE(idx_0).Is(UnitTypes.Elfemale, UnitTypes.Snowy) || Es.UnitTypeE(idx_0).Is(UnitTypes.Pawn) && Es.UnitTWE(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            var xy_from = CellEs(idx_0).CellE.XyC.Xy;

                            for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                            {
                                var xy_1 = CellWorker.GetXyCellByDirect(xy_from, dir_1);
                                var idx_1 = CellWorker.GetIdxCell(xy_1);

                                var ownUnit_1 = UnitEs(idx_1).OwnerE.OwnerC;


                                if (CellEs(idx_1).ParentE.IsActiveSelf.IsActive && !EnvironmentEs(idx_1).Mountain.HaveEnvironment)
                                {
                                    if (UnitEs(idx_1).TypeE.HaveUnit)
                                    {
                                        if (!ownUnit_1.Is(ownUnit_0.Player))
                                        {
                                            if (Es.UnitTypeE(idx_0).Is(UnitTypes.Pawn) && Es.UnitTWE(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                                            {
                                                if (isCornered_0)
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


                                    var xy_2 = CellWorker.GetXyCellByDirect(xy_1, dir_1);
                                    var idx_2 = CellWorker.GetIdxCell(xy_2);


                                    var unit_2 = UnitEs(idx_2).TypeE.UnitTC;
                                    var ownUnit_2 = UnitEs(idx_2).OwnerE.OwnerC;



                                    if (Es.UnitEs(idx_2).TypeE.HaveUnit && !unit_2.IsAnimal
                                        && Es.UnitEs(idx_2).VisibleE(ownUnit_0.Player).IsVisibleC.IsVisible
                                        && !ownUnit_2.Is(ownUnit_0.Player))
                                    {
                                        if (Es.UnitTypeE(idx_0).Is(UnitTypes.Pawn) && Es.UnitTWE(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            if (isCornered_0)
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