using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.Model.System
{
    sealed class GetAttackMeleeCellsS : SystemModelGameAbs
    {
        internal GetAttackMeleeCellsS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            eMG.UnitAttackE(cell_0).Simple.Clear();
            eMG.UnitAttackE(cell_0).Unique.Clear();

            if (!eMG.UnitEs(cell_0).EffectsE.StunC.IsStunned)
            {
                if (eMG.UnitTC(cell_0).HaveUnit && eMG.UnitTC(cell_0).IsMelee(eMG.UnitMainTWTC(cell_0).ToolWeaponT) && !eMG.UnitTC(cell_0).IsAnimal)
                {
                    DirectTypes dir_cur = default;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = eMG.AroundCellsE(cell_0).AroundCellE(dirT).IdxC.Idx;

                        dir_cur += 1;

                        if (!eMG.MountainC(idx_1).HaveAnyResources)
                        {
                            var haveMaxSteps = eMG.UnitStepC(cell_0).Steps >= StepValues.MAX;

                            if (eMG.UnitStepC(cell_0).Steps >= eMG.UnitShiftE(cell_0).NeedSteps(idx_1) || haveMaxSteps)
                            {
                                if (eMG.UnitTC(idx_1).HaveUnit)
                                {
                                    if (!eMG.UnitPlayerTC(idx_1).Is(eMG.UnitPlayerTC(cell_0).PlayerT))
                                    {
                                        if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
                                        {
                                            if (dir_cur == DirectTypes.Left || dir_cur == DirectTypes.Right
                                           || dir_cur == DirectTypes.Up || dir_cur == DirectTypes.Down)
                                            {
                                                eMG.UnitAttackE(cell_0).Simple.Add(idx_1);
                                            }
                                            else eMG.UnitAttackE(cell_0).Unique.Add(idx_1);
                                        }
                                        else
                                        {
                                            eMG.UnitAttackE(cell_0).Simple.Add(idx_1);
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
