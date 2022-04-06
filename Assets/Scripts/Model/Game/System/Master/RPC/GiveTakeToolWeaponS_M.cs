using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Enum;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System.Collections.Generic;
using Photon.Pun;

namespace Chessy.Game
{
    sealed class GiveTakeToolWeaponS_M : SystemModel
    {
        internal GiveTakeToolWeaponS_M(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void GiveTake(in ToolWeaponTypes twT, in LevelTypes levTW, in byte cell_0, in Player sender)
        {
            var whoseMove = PhotonNetwork.OfflineMode ? eMG.WhoseMovePlayerT : sender.GetPlayer();

            if (eMG.UnitTC(cell_0).Is(UnitTypes.Pawn))
            {
                if (eMG.StepUnitC(cell_0).Steps >= StepValues.FOR_GIVE_TAKE_TOOLWEAPON)
                {
                    if (twT == ToolWeaponTypes.BowCrossbow || twT == ToolWeaponTypes.Staff)
                    {
                        if (eMG.ExtraToolWeaponTC(cell_0).HaveToolWeapon)
                        {
                            eMG.ToolWeaponsC(eMG.UnitPlayerTC(cell_0).PlayerT, eMG.ExtraTWLevelTC(cell_0).LevelT, eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT)++;
                            eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT = ToolWeaponTypes.None;

                            eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                        }
                        else
                        {


                            if (eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.Axe))
                            {


                                if (eMG.MainTWLevelTC(cell_0).Is(LevelTypes.First))
                                {
                                    if (eMG.PlayerInfoE(whoseMove).LevelE(levTW).ToolWeapons(twT) > 0)
                                    {
                                        eMG.ToolWeaponsC(whoseMove, levTW, twT)--;

                                        sMG.UnitSs.SetMainTWS.Set(twT, levTW, cell_0);

                                        eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                    }
                                    else
                                    {
                                        var needRes = new Dictionary<ResourceTypes, float>();
                                        var canBuy = true;

                                        for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                        {
                                            var difAmountRes = eMG.PlayerInfoE(whoseMove).ResourcesC(res).Resources - EconomyValues.ForBuyToolWeapon(twT, levTW, res);
                                            needRes.Add(res, EconomyValues.ForBuyToolWeapon(twT, levTW, res));

                                            if (canBuy) canBuy = difAmountRes >= 0;
                                        }

                                        if (canBuy)
                                        {
                                            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                                eMG.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= EconomyValues.ForBuyToolWeapon(twT, levTW, resT);

                                            eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                            sMG.UnitSs.SetMainTWS.Set(twT, levTW, cell_0);

                                            eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                        }
                                        else
                                        {
                                            eMG.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                                        }

                                    }
                                }
                                else
                                {
                                    eMG.ToolWeaponsC(whoseMove, eMG.MainTWLevelTC(cell_0).LevelT, eMG.MainToolWeaponTC(cell_0).ToolWeaponT)++;
                                    sMG.UnitSs.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First, cell_0);

                                    eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                                }
                            }

                            else
                            {
                                eMG.ToolWeaponsC(whoseMove, eMG.MainTWLevelTC(cell_0).LevelT, eMG.MainToolWeaponTC(cell_0).ToolWeaponT)++;
                                sMG.UnitSs.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First, cell_0);

                                eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                            }
                        }
                    }

                    else if (twT == ToolWeaponTypes.Axe)
                    {
                        if (eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.Axe))
                        {
                            if (eMG.MainTWLevelTC(cell_0).Is(LevelTypes.First))
                            {
                                if (eMG.PlayerInfoE(whoseMove).LevelE(levTW).ToolWeapons(twT) > 0)
                                {
                                    eMG.ToolWeaponsC(whoseMove, levTW, twT)--;
                                    sMG.UnitSs.SetMainTWS.Set(twT, levTW, cell_0);

                                    eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                }
                                else
                                {
                                    var needRes = new Dictionary<ResourceTypes, float>();
                                    var canBuy = true;

                                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                    {
                                        var difAmountRes = eMG.PlayerInfoE(whoseMove).ResourcesC(res).Resources - EconomyValues.ForBuyToolWeapon(twT, levTW, res);
                                        needRes.Add(res, EconomyValues.ForBuyToolWeapon(twT, levTW, res));

                                        if (canBuy) canBuy = difAmountRes >= 0;
                                    }

                                    if (canBuy)
                                    {
                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                            eMG.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= EconomyValues.ForBuyToolWeapon(twT, levTW, resT);

                                        eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        sMG.UnitSs.SetMainTWS.Set(twT, levTW, cell_0);

                                        eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                    }
                                    else
                                    {
                                        eMG.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                                    }

                                }
                            }
                            else
                            {
                                eMG.ToolWeaponsC(whoseMove, eMG.MainTWLevelTC(cell_0).LevelT, eMG.MainToolWeaponTC(cell_0).ToolWeaponT)++;
                                sMG.UnitSs.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First, cell_0);

                                eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                            }
                        }

                        else
                        {
                            eMG.ToolWeaponsC(whoseMove, eMG.MainTWLevelTC(cell_0).LevelT, eMG.MainToolWeaponTC(cell_0).ToolWeaponT)++;
                            sMG.UnitSs.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First, cell_0);

                            eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                            eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }
                    }

                    else
                    {
                        var ownUnit_0 = eMG.UnitPlayerTC(cell_0).PlayerT;

                        if (eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                        {
                            eMG.ToolWeaponsC(eMG.UnitPlayerTC(cell_0).PlayerT, eMG.MainTWLevelTC(cell_0).LevelT, eMG.MainToolWeaponTC(cell_0).ToolWeaponT)++;
                            sMG.UnitSs.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First, cell_0);

                            eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                        }

                        else
                        {
                            if (eMG.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.Axe))
                            {
                                if (eMG.ExtraToolWeaponTC(cell_0).HaveToolWeapon)
                                {
                                    eMG.PlayerInfoE(ownUnit_0).LevelE(eMG.ExtraTWLevelTC(cell_0).LevelT).ToolWeapons(eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT)++;
                                    eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT = ToolWeaponTypes.None;

                                    eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                }

                                else if (eMG.ToolWeaponsC(ownUnit_0, levTW, twT) > 0)
                                {
                                    eMG.PlayerInfoE(ownUnit_0).LevelE(levTW).ToolWeapons(twT)--;



                                    sMG.UnitSs.SetExtraTWS.Set(twT, levTW, eMG.ExtraTWProtectionC(cell_0).Protection, cell_0);

                                    eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                }

                                else
                                {
                                    var needRes = new Dictionary<ResourceTypes, float>();
                                    var canCreatBuild = true;

                                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                    {
                                        var difAmountRes = eMG.PlayerInfoE(whoseMove).ResourcesC(res).Resources - EconomyValues.ForBuyToolWeapon(twT, levTW, res);
                                        needRes.Add(res, EconomyValues.ForBuyToolWeapon(twT, levTW, res));

                                        if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
                                    }

                                    if (canCreatBuild)
                                    {
                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                            eMG.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= EconomyValues.ForBuyToolWeapon(twT, levTW, resT);

                                        var protection = 0f;

                                        if (twT == ToolWeaponTypes.Shield)
                                        {
                                            protection = levTW == LevelTypes.First ? ToolWeaponValues.SHIELD_PROTECTION_LEVEL_FIRST
                                                : ToolWeaponValues.SHIELD_PROTECTION_LEVEL_SECOND;
                                        }

                                        sMG.UnitSs.SetExtraTWS.Set(twT, levTW, protection, cell_0);

                                        eMG.StepUnitC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        eMG.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);


                                        if (eMG.LessonTC.Is(LessonTypes.GiveTakePickPawn))
                                        {
                                            eMG.LessonTC.SetNextLesson();
                                        }

                                    }
                                    else
                                    {
                                        eMG.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                                    }
                                }
                            }
                        }
                    }
                }

                else
                {
                    eMG.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

        }
    }
}