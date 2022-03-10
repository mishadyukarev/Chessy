using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.System.Model
{
    sealed class GetAttackMeleeCellsS : CellSystem, IEcsRunSystem
    {
        internal GetAttackMeleeCellsS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            if (!E.UnitEffectStunC(Idx).IsStunned)
            {
                if (E.UnitTC(Idx).HaveUnit && E.UnitMainE(Idx).IsMelee && !E.IsAnimal(E.UnitTC(Idx).Unit)
                    && !E.UnitMainTWTC(Idx).Is(ToolWeaponTypes.BowCrossbow))
                {
                    DirectTypes dir_cur = default;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = E.CellEs(Idx).AroundCellE(dirT).IdxC.Idx;

                        dir_cur += 1;

                        if (!E.EnvironmentEs(idx_1).MountainC.HaveAnyResources)
                        {
                            var haveMaxSteps = E.UnitStepC(Idx).Steps >= StepValues.MAX;

                            if (E.UnitStepC(Idx).Steps >= E.UnitEs(Idx).NeedSteps(idx_1).Steps || haveMaxSteps)
                            {
                                if (E.UnitTC(idx_1).HaveUnit)
                                {
                                    if (!E.UnitPlayerTC(idx_1).Is(E.UnitPlayerTC(Idx).Player))
                                    {
                                        if (E.UnitTC(Idx).Is(UnitTypes.Pawn))
                                        {
                                            if (dir_cur == DirectTypes.Left || dir_cur == DirectTypes.Right
                                           || dir_cur == DirectTypes.Up || dir_cur == DirectTypes.Down)
                                            {
                                                E.UnitEs(Idx).ForAttack(AttackTypes.Simple).Add(idx_1);
                                            }
                                            else E.UnitEs(Idx).ForAttack(AttackTypes.Unique).Add(idx_1);
                                        }
                                        else
                                        {
                                            E.UnitEs(Idx).ForAttack(AttackTypes.Simple).Add(idx_1);
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
