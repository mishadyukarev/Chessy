using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.System.Model
{
    sealed class GetAttackMeleeCellsS : SystemAbstract, IEcsRunSystem
    {
        internal GetAttackMeleeCellsS(in EntitiesModel eM) : base(eM) { }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                if (!E.UnitEffectStunC(idx_0).IsStunned)
                {
                    if (E.UnitTC(idx_0).HaveUnit && E.IsMelee(idx_0) && !E.IsAnimal(E.UnitTC(idx_0).Unit)
                        && !E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                    {
                        DirectTypes dir_cur = default;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                            dir_cur += 1;

                            if (!E.EnvironmentEs(idx_1).MountainC.HaveAnyResources)
                            {
                                var haveMaxSteps = E.UnitStepC(idx_0).Steps >= StepValues.MAX;

                                if (E.UnitStepC(idx_0).Steps >= E.UnitEs(idx_0).NeedSteps(idx_1).Steps || haveMaxSteps)
                                {
                                    if (E.UnitTC(idx_1).HaveUnit)
                                    {
                                        if (!E.UnitPlayerTC(idx_1).Is(E.UnitPlayerTC(idx_0).Player))
                                        {
                                            if (E.UnitTC(idx_0).Is(UnitTypes.Pawn))
                                            {
                                                if (dir_cur == DirectTypes.Left || dir_cur == DirectTypes.Right
                                               || dir_cur == DirectTypes.Up || dir_cur == DirectTypes.Down)
                                                {
                                                    E.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_1);
                                                }
                                                else E.UnitEs(idx_0).ForAttack(AttackTypes.Unique).Add(idx_1);
                                            }
                                            else
                                            {
                                                E.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Add(idx_1);
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
}
