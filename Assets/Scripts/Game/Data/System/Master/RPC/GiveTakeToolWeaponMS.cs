using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game
{
    public struct GiveTakeToolWeaponMS
    {
        public GiveTakeToolWeaponMS(in ToolWeaponTypes twT, in LevelTypes levTW, in byte idx_0, in Player sender, in EntitiesModel e)
        {
            var whoseMove = e.WhoseMove.Player;

            if (e.UnitTC(idx_0).Is(UnitTypes.Pawn))
            {
                if (e.UnitStepC(idx_0).Steps >= StepValues.FOR_GIVE_TAKE_TOOLWEAPON)
                {
                    if (twT == ToolWeaponTypes.BowCrossbow || twT == ToolWeaponTypes.Staff)
                    {
                        if (e.UnitExtraTWTC(idx_0).HaveToolWeapon)
                        {
                            e.ToolWeaponsC(e.UnitPlayerTC(idx_0).Player, e.UnitExtraLevelTC(idx_0).Level, e.UnitExtraTWTC(idx_0).ToolWeapon).Amount++;
                            e.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;

                            e.UnitStepC(idx_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                        }
                        else
                        {
                            if (e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                            {
                                if (e.UnitMainTWLevelTC(idx_0).Is(LevelTypes.First))
                                {
                                    if (e.PlayerInfoE(whoseMove).LevelE(levTW).ToolWeapons(twT).HaveAny)
                                    {
                                        e.ToolWeaponsC(whoseMove, levTW, twT).Amount--;
                                        e.UnitMainTWTC(idx_0).ToolWeapon = twT;
                                        e.UnitMainTWLevelTC(idx_0).Level = levTW;

                                        e.UnitStepC(idx_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

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

                                            e.UnitStepC(idx_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                            e.UnitMainTWTC(idx_0).ToolWeapon = twT;
                                            e.UnitMainTWLevelTC(idx_0).Level = levTW;

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
                                    e.ToolWeaponsC(whoseMove, e.UnitMainTWLevelTC(idx_0).Level, e.UnitMainTWTC(idx_0).ToolWeapon).Amount++;
                                    e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                                    e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;

                                    e.UnitStepC(idx_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                                }
                            }

                            else
                            {
                                e.ToolWeaponsC(whoseMove, e.UnitMainTWLevelTC(idx_0).Level, e.UnitMainTWTC(idx_0).ToolWeapon).Amount++;
                                e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                                e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;

                                e.UnitStepC(idx_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                            }
                        }
                    }

                    else if (twT == ToolWeaponTypes.Axe)
                    {
                        if (e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                        {
                            if (e.UnitMainTWLevelTC(idx_0).Is(LevelTypes.First))
                            {
                                if (e.PlayerInfoE(whoseMove).LevelE(levTW).ToolWeapons(twT).HaveAny)
                                {
                                    e.ToolWeaponsC(whoseMove, levTW, twT).Amount--;
                                    e.UnitMainTWTC(idx_0).ToolWeapon = twT;
                                    e.UnitMainTWLevelTC(idx_0).Level = levTW;

                                    e.UnitStepC(idx_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

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

                                        e.UnitStepC(idx_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        e.UnitMainTWTC(idx_0).ToolWeapon = twT;
                                        e.UnitMainTWLevelTC(idx_0).Level = levTW;

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
                                e.ToolWeaponsC(whoseMove, e.UnitMainTWLevelTC(idx_0).Level, e.UnitMainTWTC(idx_0).ToolWeapon).Amount++;
                                e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                                e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;

                                e.UnitStepC(idx_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                            }
                        }

                        else
                        {
                            e.ToolWeaponsC(whoseMove, e.UnitMainTWLevelTC(idx_0).Level, e.UnitMainTWTC(idx_0).ToolWeapon).Amount++;
                            e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                            e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;

                            e.UnitStepC(idx_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                            e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }
                    }

                    else
                    {
                        var ownUnit_0 = e.UnitPlayerTC(idx_0).Player;

                        if (e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                        {
                            e.ToolWeaponsC(e.UnitPlayerTC(idx_0).Player, e.UnitMainTWLevelTC(idx_0).Level, e.UnitMainTWTC(idx_0).ToolWeapon).Amount++;
                            e.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                            e.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;

                            e.UnitStepC(idx_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                        }

                        else
                        {
                            if (e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Axe))
                            {
                                if (e.UnitExtraTWTC(idx_0).HaveToolWeapon)
                                {
                                    e.PlayerInfoE(ownUnit_0).LevelE(e.UnitExtraLevelTC(idx_0).Level).ToolWeapons(e.UnitExtraTWTC(idx_0).ToolWeapon).Amount++;
                                    e.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;

                                    e.UnitStepC(idx_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                }

                                else if (e.ToolWeaponsC(ownUnit_0, levTW, twT).HaveAny)
                                {
                                    e.PlayerInfoE(ownUnit_0).LevelE(levTW).ToolWeapons(twT).Amount--;

                                    e.UnitExtraTWE(idx_0).Set(twT, levTW, e.UnitExtraProtectionTC(idx_0).Protection);

                                    e.UnitStepC(idx_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

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

                                        e.UnitExtraTWE(idx_0).Set(twT, levTW, protection);

                                        e.UnitStepC(idx_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        e.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
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
                    e.RpcPoolEs.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

        }
    }
}