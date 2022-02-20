namespace Game.Game
{
    sealed class GetCellsForAttackArcherS : SystemAbstract, IEcsRunSystem
    {
        internal GetCellsForAttackArcherS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                var isRight_0 = E.UnitIsRightArcherC(idx_0).IsRight;

                if (!E.UnitEffectStunC(idx_0).IsStunned)
                {
                    if (E.UnitStepC(idx_0).HaveAnySteps)
                    {
                        if (!E.UnitMainE(idx_0).IsMelee)
                        {
                            for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                            {
                                var idx_1 = E.CellEs(idx_0).AroundCellE(dir_1).IdxC.Idx;


                                if (E.CellEs(idx_1).IsActiveParentSelf && !E.MountainC(idx_1).HaveAny)
                                {
                                    if (E.UnitTC(idx_1).HaveUnit)
                                    {
                                        if (!E.UnitPlayerTC(idx_1).Is(E.UnitPlayerTC(idx_0).Player))
                                        {
                                            if (E.UnitTC(idx_0).Is(UnitTypes.Pawn) && E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                                            {
                                                if (isRight_0)
                                                {
                                                    if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Down)
                                                    {
                                                        E.UnitEs(idx_0).ForAttack(AttackTypes.Unique).Add(idx_1);
                                                    }
                                                    else E.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_1);
                                                }
                                                else
                                                {
                                                    if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                    {
                                                        E.UnitEs(idx_0).ForAttack(AttackTypes.Unique).Add(idx_1);
                                                    }
                                                    else E.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_1);
                                                }
                                            }
                                            else
                                            {
                                                E.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_1);
                                            }
                                        }
                                    }

                                    var idx_2 = E.CellEs(idx_1).AroundCellE(dir_1).IdxC.Idx;


                                    if (E.UnitTC(idx_2).HaveUnit && !E.UnitMainE(idx_2).IsAnimal
                                        && E.UnitEs(idx_2).ForPlayer(E.UnitPlayerTC(idx_0).Player).IsVisible
                                        && !E.UnitPlayerTC(idx_2).Is(E.UnitPlayerTC(idx_0).Player))
                                    {
                                        if (E.UnitTC(idx_0).Is(UnitTypes.Pawn) && E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            if (!isRight_0)
                                            {
                                                if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                {
                                                    E.UnitEs(idx_0).ForAttack(AttackTypes.Unique).Add(idx_2);
                                                }

                                                else E.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_2);
                                            }
                                            else
                                            {
                                                if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
                                                {
                                                    E.UnitEs(idx_0).ForAttack(AttackTypes.Unique).Add(idx_2);
                                                }

                                                else E.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_2);
                                            }
                                        }
                                        else
                                        {
                                            E.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_2);
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