using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell.Unit.Stats;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class GetAttackMeleeCellsS : SystemModelGameAbs
    {
        internal GetAttackMeleeCellsS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Get(in byte cell_0)
        {
            eMG.UnitEs(cell_0).SimpleAttack.Clear();
            eMG.UnitEs(cell_0).UniqueAttack.Clear();

            if (!eMG.UnitEs(cell_0).EffectsE.StunC.IsStunned)
            {
                if (eMG.UnitEs(cell_0).MainE.UnitTC.HaveUnit && eMG.UnitEs(cell_0).MainE.UnitTC.IsMelee(eMG.UnitEs(cell_0).MainToolWeaponE.ToolWeaponTC.ToolWeaponT) && !eMG.UnitEs(cell_0).MainE.UnitTC.IsAnimal)
                {
                    DirectTypes dir_cur = default;

                    for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                    {
                        var idx_1 = eMG.CellEs(cell_0).AroundCellsEs.AroundCellE(dirT).IdxC.Idx;

                        dir_cur += 1;

                        if (!eMG.MountainC(idx_1).HaveAnyResources)
                        {
                            var haveMaxSteps = eMG.UnitStepC(cell_0).Steps >= StepValues.MAX;

                            if (eMG.UnitStepC(cell_0).Steps >= eMG.UnitEs(cell_0).NeedSteps(idx_1).Steps || haveMaxSteps)
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
                                                eMG.UnitEs(cell_0).SimpleAttack.Add(idx_1);
                                            }
                                            else eMG.UnitEs(cell_0).UniqueAttack.Add(idx_1);
                                        }
                                        else
                                        {
                                            eMG.UnitEs(cell_0).SimpleAttack.Add(idx_1);
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
