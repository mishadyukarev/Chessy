using System;

namespace Game.Game
{
    sealed class GetAttackMeleeCellsS : SystemAbstract, IEcsRunSystem
    {
        internal GetAttackMeleeCellsS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Start_Values.ALL_CELLS_AMOUNT; idx_0++)
            {
                E.UnitEs(idx_0).ForAttack(AttackTypes.Simple).Clear();
                E.UnitEs(idx_0).ForAttack(AttackTypes.Unique).Clear();

                if (!E.UnitEffectStunC(idx_0).IsStunned)
                {
                    if (E.UnitTC(idx_0).HaveUnit && E.UnitMainE(idx_0).IsMelee && !E.UnitMainE(idx_0).IsAnimal
                        && !E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow) && !E.UnitTC(idx_0).Is(UnitTypes.Scout))
                    {
                        DirectTypes dir_cur = default;

                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = E.CellEs(idx_0).AroundCellE(dirT).IdxC.Idx;

                            dir_cur += 1;

                            if (!E.EnvironmentEs(idx_1).MountainC.HaveAnyResources)
                            {
                                var dir = E.CellEs(idx_0).Direct(idx_1);

                                var haveMaxSteps = E.UnitStepC(idx_0).Steps >= E.UnitInfo(E.UnitPlayerTC(idx_0), E.UnitLevelTC(idx_0), E.UnitTC(idx_0)).MaxSteps;

                                if (E.UnitTC(idx_0).Is(UnitTypes.King))
                                {

                                }

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
