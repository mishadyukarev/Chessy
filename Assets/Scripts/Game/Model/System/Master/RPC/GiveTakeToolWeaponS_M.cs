using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Realtime;
using System.Collections.Generic;

namespace Chessy.Game
{
    public sealed class GiveTakeToolWeaponS_M : SystemModelGameAbs
    {
        readonly SetExtraToolWeaponS _setExtraTWS;

        public GiveTakeToolWeaponS_M(in SetExtraToolWeaponS setExtraTWS, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _setExtraTWS = setExtraTWS;
        }

        public void GiveTake(in ToolWeaponTypes twT, in LevelTypes levTW, in byte cell_0, in Player sender)
        {
            var whoseMove = eMGame.WhoseMove.Player;

            if (eMGame.UnitTC(cell_0).Is(UnitTypes.Pawn))
            {
                if (eMGame.UnitStepC(cell_0).Steps >= StepValues.FOR_GIVE_TAKE_TOOLWEAPON)
                {
                    if (twT == ToolWeaponTypes.BowCrossbow || twT == ToolWeaponTypes.Staff)
                    {
                        if (eMGame.UnitExtraTWTC(cell_0).HaveToolWeapon)
                        {
                            eMGame.ToolWeaponsC(eMGame.UnitPlayerTC(cell_0).Player, eMGame.UnitExtraLevelTC(cell_0).Level, eMGame.UnitExtraTWTC(cell_0).ToolWeapon)++;
                            eMGame.UnitExtraTWTC(cell_0).ToolWeapon = ToolWeaponTypes.None;

                            eMGame.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                        }
                        else
                        {
                            if (eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Axe))
                            {
                                if (eMGame.UnitMainTWLevelTC(cell_0).Is(LevelTypes.First))
                                {
                                    if (eMGame.PlayerInfoE(whoseMove).LevelE(levTW).ToolWeapons(twT) > 0)
                                    {
                                        eMGame.ToolWeaponsC(whoseMove, levTW, twT)--;
                                        eMGame.UnitMainTWTC(cell_0).ToolWeapon = twT;
                                        eMGame.UnitMainTWLevelTC(cell_0).Level = levTW;

                                        eMGame.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                    }
                                    else
                                    {
                                        var needRes = new Dictionary<ResourceTypes, float>();
                                        var canBuy = true;

                                        for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                        {
                                            var difAmountRes = eMGame.PlayerInfoE(whoseMove).ResourcesC(res).Resources - EconomyValues.ForBuyToolWeapon(twT, levTW, res);
                                            needRes.Add(res, EconomyValues.ForBuyToolWeapon(twT, levTW, res));

                                            if (canBuy) canBuy = difAmountRes >= 0;
                                        }

                                        if (canBuy)
                                        {
                                            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                                eMGame.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= EconomyValues.ForBuyToolWeapon(twT, levTW, resT);

                                            eMGame.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                            eMGame.UnitMainTWTC(cell_0).ToolWeapon = twT;
                                            eMGame.UnitMainTWLevelTC(cell_0).Level = levTW;

                                            eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                        }
                                        else
                                        {
                                            eMGame.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                                        }

                                    }
                                }
                                else
                                {
                                    eMGame.ToolWeaponsC(whoseMove, eMGame.UnitMainTWLevelTC(cell_0).Level, eMGame.UnitMainTWTC(cell_0).ToolWeapon)++;
                                    eMGame.UnitMainTWTC(cell_0).ToolWeapon = ToolWeaponTypes.Axe;
                                    eMGame.UnitMainTWLevelTC(cell_0).Level = LevelTypes.First;

                                    eMGame.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                                }
                            }

                            else
                            {
                                eMGame.ToolWeaponsC(whoseMove, eMGame.UnitMainTWLevelTC(cell_0).Level, eMGame.UnitMainTWTC(cell_0).ToolWeapon)++;
                                eMGame.UnitMainTWTC(cell_0).ToolWeapon = ToolWeaponTypes.Axe;
                                eMGame.UnitMainTWLevelTC(cell_0).Level = LevelTypes.First;

                                eMGame.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                            }
                        }
                    }

                    else if (twT == ToolWeaponTypes.Axe)
                    {
                        if (eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Axe))
                        {
                            if (eMGame.UnitMainTWLevelTC(cell_0).Is(LevelTypes.First))
                            {
                                if (eMGame.PlayerInfoE(whoseMove).LevelE(levTW).ToolWeapons(twT) > 0)
                                {
                                    eMGame.ToolWeaponsC(whoseMove, levTW, twT)--;
                                    eMGame.UnitMainTWTC(cell_0).ToolWeapon = twT;
                                    eMGame.UnitMainTWLevelTC(cell_0).Level = levTW;

                                    eMGame.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                }
                                else
                                {
                                    var needRes = new Dictionary<ResourceTypes, float>();
                                    var canBuy = true;

                                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                    {
                                        var difAmountRes = eMGame.PlayerInfoE(whoseMove).ResourcesC(res).Resources - EconomyValues.ForBuyToolWeapon(twT, levTW, res);
                                        needRes.Add(res, EconomyValues.ForBuyToolWeapon(twT, levTW, res));

                                        if (canBuy) canBuy = difAmountRes >= 0;
                                    }

                                    if (canBuy)
                                    {
                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                            eMGame.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= EconomyValues.ForBuyToolWeapon(twT, levTW, resT);

                                        eMGame.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        eMGame.UnitMainTWTC(cell_0).ToolWeapon = twT;
                                        eMGame.UnitMainTWLevelTC(cell_0).Level = levTW;

                                        eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                    }
                                    else
                                    {
                                        eMGame.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                                    }

                                }
                            }
                            else
                            {
                                eMGame.ToolWeaponsC(whoseMove, eMGame.UnitMainTWLevelTC(cell_0).Level, eMGame.UnitMainTWTC(cell_0).ToolWeapon)++;
                                eMGame.UnitMainTWTC(cell_0).ToolWeapon = ToolWeaponTypes.Axe;
                                eMGame.UnitMainTWLevelTC(cell_0).Level = LevelTypes.First;

                                eMGame.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                            }
                        }

                        else
                        {
                            eMGame.ToolWeaponsC(whoseMove, eMGame.UnitMainTWLevelTC(cell_0).Level, eMGame.UnitMainTWTC(cell_0).ToolWeapon)++;
                            eMGame.UnitMainTWTC(cell_0).ToolWeapon = ToolWeaponTypes.Axe;
                            eMGame.UnitMainTWLevelTC(cell_0).Level = LevelTypes.First;

                            eMGame.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                            eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }
                    }

                    else
                    {
                        var ownUnit_0 = eMGame.UnitPlayerTC(cell_0).Player;

                        if (eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                        {
                            eMGame.ToolWeaponsC(eMGame.UnitPlayerTC(cell_0).Player, eMGame.UnitMainTWLevelTC(cell_0).Level, eMGame.UnitMainTWTC(cell_0).ToolWeapon)++;
                            eMGame.UnitMainTWTC(cell_0).ToolWeapon = ToolWeaponTypes.Axe;
                            eMGame.UnitMainTWLevelTC(cell_0).Level = LevelTypes.First;

                            eMGame.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                        }

                        else
                        {
                            if (eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Axe))
                            {
                                if (eMGame.UnitExtraTWTC(cell_0).HaveToolWeapon)
                                {
                                    eMGame.PlayerInfoE(ownUnit_0).LevelE(eMGame.UnitExtraLevelTC(cell_0).Level).ToolWeapons(eMGame.UnitExtraTWTC(cell_0).ToolWeapon)++;
                                    eMGame.UnitExtraTWTC(cell_0).ToolWeapon = ToolWeaponTypes.None;

                                    eMGame.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                }

                                else if (eMGame.ToolWeaponsC(ownUnit_0, levTW, twT) > 0)
                                {
                                    eMGame.PlayerInfoE(ownUnit_0).LevelE(levTW).ToolWeapons(twT)--;



                                    _setExtraTWS.Set(twT, levTW, eMGame.UnitExtraProtectionTC(cell_0).Protection, cell_0);

                                    eMGame.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                }

                                else
                                {
                                    var needRes = new Dictionary<ResourceTypes, float>();
                                    var canCreatBuild = true;

                                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                                    {
                                        var difAmountRes = eMGame.PlayerInfoE(whoseMove).ResourcesC(res).Resources - EconomyValues.ForBuyToolWeapon(twT, levTW, res);
                                        needRes.Add(res, EconomyValues.ForBuyToolWeapon(twT, levTW, res));

                                        if (canCreatBuild) canCreatBuild = difAmountRes >= 0;
                                    }

                                    if (canCreatBuild)
                                    {
                                        for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                                            eMGame.PlayerInfoE(whoseMove).ResourcesC(resT).Resources -= EconomyValues.ForBuyToolWeapon(twT, levTW, resT);

                                        var protection = 0f;

                                        if (twT == ToolWeaponTypes.Shield)
                                        {
                                            protection = levTW == LevelTypes.First ? ToolWeaponValues.SHIELD_PROTECTION_LEVEL_FIRST
                                                : ToolWeaponValues.SHIELD_PROTECTION_LEVEL_SECOND;
                                        }

                                        _setExtraTWS.Set(twT, levTW, protection, cell_0);

                                        eMGame.UnitStepC(cell_0).Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                    }
                                    else
                                    {
                                        eMGame.RpcPoolEs.MistakeEconomyToGeneral(sender, needRes);
                                    }
                                }
                            }
                        }
                    }
                }

                else
                {
                    eMGame.RpcPoolEs.SimpleMistake_ToGeneral(MistakeTypes.NeedMoreSteps, sender);
                }
            }

        }
    }
}