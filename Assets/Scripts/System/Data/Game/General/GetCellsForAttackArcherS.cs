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
                var isRight_0 = Es.UnitIsRightArcherC(idx_0).IsRight;

                if (!Es.UnitStunC(idx_0).IsStunned)
                {
                    if (Es.UnitStepC(idx_0).HaveAnySteps)
                    {
                        if (Es.UnitTC(idx_0).Is(UnitTypes.Elfemale, UnitTypes.Snowy) || Es.UnitTC(idx_0).Is(UnitTypes.Pawn) && Es.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            var xy_from = Es.CellEs(idx_0).CellE.XyC.Xy;

                            for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                            {
                                var xy_1 = CellWorker.GetXyCellByDirect(xy_from, dir_1);
                                var idx_1 = CellWorker.GetIdxCell(xy_1);


                                if (Es.CellEs(idx_1).IsActiveParentSelf && !Es.MountainC(idx_1).HaveAny)
                                {
                                    if (Es.UnitTC(idx_0).HaveUnit)
                                    {
                                        if (!Es.UnitPlayerTC(idx_1).Is(Es.UnitPlayerTC(idx_0).Player))
                                        {
                                            if (Es.UnitTC(idx_0).Is(UnitTypes.Pawn) && Es.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                                            {
                                                if (isRight_0)
                                                {
                                                    if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Down)
                                                    {
                                                        Es.UnitEs(idx_0).ForPlayer(Es.UnitPlayerTC(idx_0).Player).ForAttack(AttackTypes.Unique).Add(idx_1);
                                                    }
                                                    else Es.UnitEs(idx_0).ForPlayer(Es.UnitPlayerTC(idx_0).Player).ForAttack(AttackTypes.Simple).Add(idx_1);
                                                }
                                                else
                                                {
                                                    if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                    {
                                                        Es.UnitEs(idx_0).ForPlayer(Es.UnitPlayerTC(idx_0).Player).ForAttack(AttackTypes.Unique).Add(idx_1);
                                                    }
                                                    else Es.UnitEs(idx_0).ForPlayer(Es.UnitPlayerTC(idx_0).Player).ForAttack(AttackTypes.Simple).Add(idx_1);
                                                }
                                            }
                                            else
                                            {
                                                Es.UnitEs(idx_0).ForPlayer(Es.UnitPlayerTC(idx_0).Player).ForAttack(AttackTypes.Simple).Add(idx_1);
                                            }
                                        }
                                    }


                                    var xy_2 = CellWorker.GetXyCellByDirect(xy_1, dir_1);
                                    var idx_2 = CellWorker.GetIdxCell(xy_2);


                                    if (Es.UnitTC(idx_2).HaveUnit && !Es.UnitEs(idx_2).IsAnimal
                                        && Es.UnitEs(idx_2).ForPlayer(Es.UnitPlayerTC(idx_0).Player).IsVisibleC
                                        && !Es.UnitPlayerTC(idx_2).Is(Es.UnitPlayerTC(idx_0).Player))
                                    {
                                        if (Es.UnitTC(idx_0).Is(UnitTypes.Pawn) && Es.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            if (isRight_0)
                                            {
                                                if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                {
                                                    Es.UnitEs(idx_0).ForPlayer(Es.UnitPlayerTC(idx_0).Player).ForAttack(AttackTypes.Unique).Add(idx_2);
                                                }

                                                else Es.UnitEs(idx_0).ForPlayer(Es.UnitPlayerTC(idx_0).Player).ForAttack(AttackTypes.Simple).Add(idx_2);
                                            }
                                            else
                                            {
                                                if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
                                                {
                                                    Es.UnitEs(idx_0).ForPlayer(Es.UnitPlayerTC(idx_0).Player).ForAttack(AttackTypes.Unique).Add(idx_2);
                                                }

                                                else Es.UnitEs(idx_0).ForPlayer(Es.UnitPlayerTC(idx_0).Player).ForAttack(AttackTypes.Simple).Add(idx_2);
                                            }
                                        }
                                        else
                                        {
                                            Es.UnitEs(idx_0).ForPlayer(Es.UnitPlayerTC(idx_0).Player).ForAttack(AttackTypes.Simple).Add(idx_2);
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