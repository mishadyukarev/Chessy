namespace Chessy.Game.System.Model
{
    sealed class GetCellsForAttackArcherS : CellSystem, IEcsRunSystem
    {
        internal GetCellsForAttackArcherS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            if (E.UnitTC(Idx).HaveUnit)
            {
                if (!E.UnitEffectStunC(Idx).IsStunned)
                {
                    if (E.UnitStepC(Idx).HaveAnySteps)
                    {
                        if (!E.UnitMainE(Idx).IsMelee)
                        {
                            for (var dir_1 = DirectTypes.None + 1; dir_1 < DirectTypes.End; dir_1++)
                            {
                                var idx_1 = E.CellEs(Idx).AroundCellE(dir_1).IdxC.Idx;

                                var isRight_0 = E.UnitIsRightArcherC(Idx).IsRight;

                                if (E.CellEs(idx_1).IsActiveParentSelf && !E.MountainC(idx_1).HaveAnyResources)
                                {
                                    if (E.UnitTC(idx_1).HaveUnit)
                                    {
                                        if (!E.UnitPlayerTC(idx_1).Is(E.UnitPlayerTC(Idx).Player))
                                        {
                                            if (E.UnitTC(Idx).Is(UnitTypes.Pawn) && E.UnitMainTWTC(Idx).Is(ToolWeaponTypes.BowCrossbow))
                                            {
                                                if (isRight_0)
                                                {
                                                    if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Up || dir_1 == DirectTypes.Down)
                                                    {
                                                        E.UnitEs(Idx).ForAttack(AttackTypes.Unique).Add(idx_1);
                                                    }
                                                    else E.UnitEs(Idx).ForAttack(AttackTypes.Simple).Add(idx_1);
                                                }
                                                else
                                                {
                                                    if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                    {
                                                        E.UnitEs(Idx).ForAttack(AttackTypes.Unique).Add(idx_1);
                                                    }
                                                    else E.UnitEs(Idx).ForAttack(AttackTypes.Simple).Add(idx_1);
                                                }
                                            }
                                            else
                                            {
                                                E.UnitEs(Idx).ForAttack(AttackTypes.Simple).Add(idx_1);
                                            }
                                        }
                                    }

                                    var idx_2 = E.CellEs(idx_1).AroundCellE(dir_1).IdxC.Idx;


                                    if (E.UnitTC(idx_2).HaveUnit && !E.IsAnimal(E.UnitTC(idx_2).Unit)
                                        && E.UnitEs(idx_2).ForPlayer(E.UnitPlayerTC(Idx).Player).IsVisible
                                        && !E.UnitPlayerTC(idx_2).Is(E.UnitPlayerTC(Idx).Player))
                                    {
                                        if (E.UnitTC(Idx).Is(UnitTypes.Pawn) && E.UnitMainTWTC(Idx).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            if (!isRight_0)
                                            {
                                                if (dir_1 == DirectTypes.DownLeft || dir_1 == DirectTypes.UpLeft || dir_1 == DirectTypes.UpRight || dir_1 == DirectTypes.DownRight)
                                                {
                                                    E.UnitEs(Idx).ForAttack(AttackTypes.Unique).Add(idx_2);
                                                }

                                                else E.UnitEs(Idx).ForAttack(AttackTypes.Simple).Add(idx_2);
                                            }
                                            else
                                            {
                                                if (dir_1 == DirectTypes.Left || dir_1 == DirectTypes.Right || dir_1 == DirectTypes.Down || dir_1 == DirectTypes.Up)
                                                {
                                                    E.UnitEs(Idx).ForAttack(AttackTypes.Unique).Add(idx_2);
                                                }

                                                else E.UnitEs(Idx).ForAttack(AttackTypes.Simple).Add(idx_2);
                                            }
                                        }
                                        else
                                        {
                                            E.UnitEs(Idx).ForAttack(AttackTypes.Simple).Add(idx_2);
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