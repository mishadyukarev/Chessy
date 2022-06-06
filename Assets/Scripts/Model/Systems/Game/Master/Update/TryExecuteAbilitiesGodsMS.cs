using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Effect;
using Photon.Pun;

namespace Chessy.Game
{
    sealed class TryExecuteAbilitiesGodsMS : SystemModel
    {
        internal TryExecuteAbilitiesGodsMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TryExecute()
        {
            if (!eMG.LessonTC.HaveLesson)
            {
                if (eMG.MotionsC.Motions % UpdateValues.EVERY_MOTION_FOR_ACTIVE_GOD_ABILITY == 0)
                {
                    for (byte cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
                    {
                        //e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AfterBuildTown);

                        eMG.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);


                        if (!eMG.IsBorder(cellIdx0))
                        {
                            if (eMG.UnitTC(cellIdx0).HaveUnit)
                            {
                                if (eMG.PlayerInfoE(eMG.UnitPlayerTC(cellIdx0).PlayerT).GodInfoE.UnitTC.Is(UnitTypes.Snowy))
                                {
                                    if (eMG.UnitTC(cellIdx0).Is(UnitTypes.Pawn))
                                    {
                                        if (eMG.MainToolWeaponTC(cellIdx0).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            eMG.FrozenArrawEffectC(cellIdx0).Shoots++;
                                        }
                                        else
                                        {
                                            eMG.ShieldUnitEffectC(cellIdx0).Protection = ShieldValues.AFTER_5_MOTIONS_RAINY;
                                        }
                                    }
                                    else
                                    {
                                        eMG.ShieldUnitEffectC(cellIdx0).Protection = ShieldValues.AFTER_5_MOTIONS_RAINY;
                                    }
                                }
                            }
                            else
                            {
                                if (eMG.AdultForestC(cellIdx0).HaveAnyResources)
                                {
                                    if (!eMG.HaveTreeUnit)
                                    {
                                        for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                                        {
                                            if (eMG.PlayerInfoE(playerT).GodInfoE.UnitTC.Is(UnitTypes.Elfemale))
                                            {
                                                sMG.UnitSs.SetNewOnCellS.Set(UnitTypes.Tree, playerT, cellIdx0);

                                                break;
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