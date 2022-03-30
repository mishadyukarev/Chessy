using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.System.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game
{
    sealed class GiveTakeToolWeaponS_M : SystemModelGameAbs
    {
        internal GiveTakeToolWeaponS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void GiveTake(in ToolWeaponTypes twT, in LevelTypes levTW, in byte cell_0, in Player sender)
        {
            var whoseMove = e.WhoseMove.Player;

            if (e.UnitTC(cell_0).Is(UnitTypes.Pawn))
            {
                if (e.UnitStepC(cell_0).Steps >= StepValues.FOR_GIVE_TAKE_TOOLWEAPON)
                {
                    if (twT == ToolWeaponTypes.BowCrossbow || twT == ToolWeaponTypes.Staff)
                    {
                        if (e.UnitExtraTWTC(cell_0).HaveToolWeapon)
                        {
                            e.ToolWeaponsC(e.UnitPlayerTC(cell_0).Player, e.UnitExtraLevelTC(cell_0).Level, e.UnitExtraTWTC(cell_0).ToolWeapon)++;
                            e.UnitExtraTWTC(cell_0).ToolWeapon = ToolWeaponTypes.None;

                            e.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                        }
                        else
                        {


                            if (e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Axe))
                            {


                                if (e.UnitMainTWLevelTC(cell_0).Is(LevelTypes.First))
                                {
                                    if (e.PlayerInfoE(whoseMove).LevelE(levTW).ToolWeapons(twT) > 0)
                                    {
                                        e.ToolWeaponsC(whoseMove, levTW, twT)--;

                                        s.SetMainTWS.Set(twT, levTW, cell_0);

                                        e.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                    }
                                    else
                                    {
                                        var needRes = new Dictionary<ResourceTypes, float>();
                                        var canBuy = true;

                                        for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                        {
                                            var difAmountRes = e.PlayerInfoE(whoseMove).ResourcesC(res).Resources - EconomyValues.ForBuyToolWeapon(twT, levTW, res);
                                            needRes.Add(res, EconomyValues.ForBuyToolWeapon(twT, levTW, res));

                                            if (canBuy) canBuy = difAmountRes >= 0;
                                        }

                                        if (canBuy)
                                        {
                                            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                                e.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= EconomyValues.ForBuyToolWeapon(twT, levTW, resT);

                                            e.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                            s.SetMainTWS.Set(twT, levTW, cell_0);

                                            e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                        }
                                        else
                                        {
                                            e.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                                        }

                                    }
                                }
                                else
                                {
                                    e.ToolWeaponsC(whoseMove, e.UnitMainTWLevelTC(cell_0).Level, e.UnitMainTWTC(cell_0).ToolWeapon)++;
                                    s.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First, cell_0);

                                    e.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                                }
                            }

                            else
                            {
                                e.ToolWeaponsC(whoseMove, e.UnitMainTWLevelTC(cell_0).Level, e.UnitMainTWTC(cell_0).ToolWeapon)++;
                                s.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First, cell_0);

                                e.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                            }
                        }
                    }

                    else if (twT == ToolWeaponTypes.Axe)
                    {
                        if (e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Axe))
                        {
                            if (e.UnitMainTWLevelTC(cell_0).Is(LevelTypes.First))
                            {
                                if (e.PlayerInfoE(whoseMove).LevelE(levTW).ToolWeapons(twT) > 0)
                                {
                                    e.ToolWeaponsC(whoseMove, levTW, twT)--;
                                    s.SetMainTWS.Set(twT, levTW, cell_0);

                                    e.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                }
                                else
                                {
                                    var needRes = new Dictionary<ResourceTypes, float>();
                                    var canBuy = true;

                                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                    {
                                        var difAmountRes = e.PlayerInfoE(whoseMove).ResourcesC(res).Resources - EconomyValues.ForBuyToolWeapon(twT, levTW, res);
                                        needRes.Add(res, EconomyValues.ForBuyToolWeapon(twT, levTW, res));

                                        if (canBuy) canBuy = difAmountRes >= 0;
                                    }

                                    if (canBuy)
                                    {
                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                            e.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= EconomyValues.ForBuyToolWeapon(twT, levTW, resT);

                                        e.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        s.SetMainTWS.Set(twT, levTW, cell_0);

                                        e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                    }
                                    else
                                    {
                                        e.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                                    }

                                }
                            }
                            else
                            {
                                e.ToolWeaponsC(whoseMove, e.UnitMainTWLevelTC(cell_0).Level, e.UnitMainTWTC(cell_0).ToolWeapon)++;
                                s.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First, cell_0);

                                e.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                            }
                        }

                        else
                        {
                            e.ToolWeaponsC(whoseMove, e.UnitMainTWLevelTC(cell_0).Level, e.UnitMainTWTC(cell_0).ToolWeapon)++;
                            s.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First, cell_0);

                            e.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                            e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }
                    }

                    else
                    {
                        var ownUnit_0 = e.UnitPlayerTC(cell_0).Player;

                        if (e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                        {
                            e.ToolWeaponsC(e.UnitPlayerTC(cell_0).Player, e.UnitMainTWLevelTC(cell_0).Level, e.UnitMainTWTC(cell_0).ToolWeapon)++;
                            s.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First, cell_0);

                            e.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                        }

                        else
                        {
                            if (e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Axe))
                            {
                                if (e.UnitExtraTWTC(cell_0).HaveToolWeapon)
                                {
                                    e.PlayerInfoE(ownUnit_0).LevelE(e.UnitExtraLevelTC(cell_0).Level).ToolWeapons(e.UnitExtraTWTC(cell_0).ToolWeapon)++;
                                    e.UnitExtraTWTC(cell_0).ToolWeapon = ToolWeaponTypes.None;

                                    e.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                }

                                else if (e.ToolWeaponsC(ownUnit_0, levTW, twT) > 0)
                                {
                                    e.PlayerInfoE(ownUnit_0).LevelE(levTW).ToolWeapons(twT)--;



                                    s.SetExtraTWS.Set(twT, levTW, e.UnitExtraProtectionC(cell_0).Protection, cell_0);

                                    e.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                }

                                else
                                {
                                    var needRes = new Dictionary<ResourceTypes, float>();
                                    var canCreatBuild = true;

                                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                    {
                                        var difAmountRes = e.PlayerInfoE(whoseMove).ResourcesC(res).Resources - EconomyValues.ForBuyToolWeapon(twT, levTW, res);
                                        needRes.Add(res, EconomyValues.ForBuyToolWeapon(twT, levTW, res));

                                        if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
                                    }

                                    if (canCreatBuild)
                                    {
                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                            e.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= EconomyValues.ForBuyToolWeapon(twT, levTW, resT);

                                        var protection = 0f;

                                        if (twT == ToolWeaponTypes.Shield)
                                        {
                                            protection = levTW == LevelTypes.First ? ToolWeaponValues.SHIELD_PROTECTION_LEVEL_FIRST
                                                : ToolWeaponValues.SHIELD_PROTECTION_LEVEL_SECOND;
                                        }

                                        s.SetExtraTWS.Set(twT, levTW, protection, cell_0);

                                        e.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);


                                        if (e.LessonTC.Is(LessonTypes.GiveTakePickPawn))
                                        {
                                            e.LessonTC.SetNextLesson();
                                        }

                                    }
                                    else
                                    {
                                        e.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                                    }
                                }
                            }
                        }
                    }
                }

                else
                {
                    e.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

        }
    }
}