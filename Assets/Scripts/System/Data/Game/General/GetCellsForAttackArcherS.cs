namespace Game.Game
{
    sealed class GetCellsForAttackArcherS : SystemAbstract, IEcsRunSystem
    {
        internal GetCellsForAttackArcherS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                var isRight_0 = Es.UnitE(idx_0).IsRightArcher;

                if (!Es.UnitE(idx_0).IsStunned)
                {
                    if (Es.UnitE(idx_0).HaveSteps)
                    {
                        if (Es.UnitE(idx_0).Is(UnitTypes.Elfemale, UnitTypes.Snowy) || Es.UnitE(idx_0).Is(UnitTypes.Pawn) && Es.MainTWE(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            var xy_from = Es.CellEs(idx_0).CellE.XyC.Xy;

                            for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                            {
                                var xy_1 = CellWorker.GetXyCellByDirect(xy_from, dir_1);
                                var idx_1 = CellWorker.GetIdxCell(xy_1);


                                if (Es.CellEs(idx_1).ParentE.IsActiveSelf.IsActive && !Es.MountainE(idx_1).HaveEnvironment)
                                {
                                    if (Es.UnitE(idx_1).HaveUnit)
                                    {
                                        if (!Es.UnitE(idx_1).Is(Es.UnitE(idx_0).Owner))
                                        {
                                            if (Es.UnitE(idx_0).Is(UnitTypes.Pawn) && Es.MainTWE(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                                            {
                                                if (isRight_0)
                                                {
                                                    if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Down)
                                                    {
                                                        CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, Es.UnitE(idx_0).Owner).Add(idx_1);
                                                    }
                                                    else CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, Es.UnitE(idx_0).Owner).Add(idx_1);
                                                }
                                                else
                                                {
                                                    if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                    {
                                                        CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, Es.UnitE(idx_0).Owner).Add(idx_1);
                                                    }
                                                    else CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, Es.UnitE(idx_0).Owner).Add(idx_1);
                                                }
                                            }
                                            else
                                            {
                                                CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, Es.UnitE(idx_0).Owner).Add(idx_1);
                                            }
                                        }
                                    }


                                    var xy_2 = CellWorker.GetXyCellByDirect(xy_1, dir_1);
                                    var idx_2 = CellWorker.GetIdxCell(xy_2);


                                    if (Es.UnitE(idx_2).HaveUnit && !Es.UnitE(idx_2).IsAnimal
                                        && Es.UnitEs(idx_2).VisibleE(Es.UnitE(idx_0).Owner).IsVisibleC.IsVisible
                                        && !Es.UnitE(idx_2).Is(Es.UnitE(idx_0).Owner))
                                    {
                                        if (Es.UnitE(idx_0).Is(UnitTypes.Pawn) && Es.MainTWE(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            if (isRight_0)
                                            {
                                                if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                {
                                                    CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, Es.UnitE(idx_0).Owner).Add(idx_2);
                                                }

                                                else CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, Es.UnitE(idx_0).Owner).Add(idx_2);
                                            }
                                            else
                                            {
                                                if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
                                                {
                                                    CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, Es.UnitE(idx_0).Owner).Add(idx_2);
                                                }

                                                else CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Unique, Es.UnitE(idx_0).Owner).Add(idx_2);
                                            }
                                        }
                                        else
                                        {
                                            CellsForAttackUnitsEs.CanAttack<IdxsC>(idx_0, AttackTypes.Simple, Es.UnitE(idx_0).Owner).Add(idx_2);
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