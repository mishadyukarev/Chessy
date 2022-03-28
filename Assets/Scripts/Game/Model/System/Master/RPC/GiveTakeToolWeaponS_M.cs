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
        readonly CellEs _cellEs;
        readonly CellSs _unitSMGame;

        internal GiveTakeToolWeaponS_M(in CellEs cellEs, in CellSs unitSMGame, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _cellEs = cellEs;
            _unitSMGame = unitSMGame;
        }

        public void GiveTake(in ToolWeaponTypes twT, in LevelTypes levTW, in Player sender)
        {
            var whoseMove = eMGame.WhoseMove.Player;

            if (_cellEs.UnitTC.Is(UnitTypes.Pawn))
            {
                if (_cellEs.UnitStatsE.StepC.Steps >= StepValues.FOR_GIVE_TAKE_TOOLWEAPON)
                {
                    if (twT == ToolWeaponTypes.BowCrossbow || twT == ToolWeaponTypes.Staff)
                    {
                        if (_cellEs.UnitExtraTWE.ToolWeaponTC.HaveToolWeapon)
                        {
                            eMGame.ToolWeaponsC(_cellEs.UnitMainE.PlayerTC.Player, _cellEs.UnitExtraTWE.LevelTC.Level, _cellEs.UnitExtraTWE.ToolWeaponTC.ToolWeapon)++;
                            _cellEs.UnitExtraTWE.ToolWeaponTC.ToolWeapon = ToolWeaponTypes.None;

                            _cellEs.UnitStatsE.StepC.Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                        }
                        else
                        {
                            if (_cellEs.UnitEs.MainToolWeaponE.ToolWeaponTC.Is(ToolWeaponTypes.Axe))
                            {
                                if (_cellEs.UnitMainE.LevelTC.Is(LevelTypes.First))
                                {
                                    if (eMGame.PlayerInfoE(whoseMove).LevelE(levTW).ToolWeapons(twT) > 0)
                                    {
                                        eMGame.ToolWeaponsC(whoseMove, levTW, twT)--;
                                        _unitSMGame.SetMainTWS.Set(twT, levTW);

                                        _cellEs.UnitStatsE.StepC.Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

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

                                            _cellEs.UnitStatsE.StepC.Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                            _unitSMGame.SetMainTWS.Set(twT, levTW);

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
                                    eMGame.ToolWeaponsC(whoseMove, _cellEs.UnitMainE.LevelTC.Level, _cellEs.UnitEs.MainToolWeaponE.ToolWeaponTC.ToolWeapon)++;
                                    _unitSMGame.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First);

                                    _cellEs.UnitStatsE.StepC.Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                                }
                            }

                            else
                            {
                                eMGame.ToolWeaponsC(whoseMove, _cellEs.UnitMainE.LevelTC.Level, _cellEs.UnitEs.MainToolWeaponE.ToolWeaponTC.ToolWeapon)++;
                                _unitSMGame.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First);

                                _cellEs.UnitStatsE.StepC.Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                            }
                        }
                    }

                    else if (twT == ToolWeaponTypes.Axe)
                    {
                        if (_cellEs.UnitEs.MainToolWeaponE.ToolWeaponTC.Is(ToolWeaponTypes.Axe))
                        {
                            if (_cellEs.UnitMainE.LevelTC.Is(LevelTypes.First))
                            {
                                if (eMGame.PlayerInfoE(whoseMove).LevelE(levTW).ToolWeapons(twT) > 0)
                                {
                                    eMGame.ToolWeaponsC(whoseMove, levTW, twT)--;
                                    _unitSMGame.SetMainTWS.Set(twT, levTW);

                                    _cellEs.UnitStatsE.StepC.Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

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

                                        _cellEs.UnitStatsE.StepC.Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                        _unitSMGame.SetMainTWS.Set(twT, levTW);

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
                                eMGame.ToolWeaponsC(whoseMove, _cellEs.UnitMainE.LevelTC.Level, _cellEs.UnitEs.MainToolWeaponE.ToolWeaponTC.ToolWeapon)++;
                                _unitSMGame.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First);

                                _cellEs.UnitStatsE.StepC.Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                            }
                        }

                        else
                        {
                            eMGame.ToolWeaponsC(whoseMove, _cellEs.UnitMainE.LevelTC.Level, _cellEs.UnitEs.MainToolWeaponE.ToolWeaponTC.ToolWeapon)++;
                            _unitSMGame.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First);

                            _cellEs.UnitStatsE.StepC.Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                            eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                        }
                    }

                    else
                    {
                        var ownUnit_0 = _cellEs.UnitMainE.PlayerTC.Player;

                        if (_cellEs.UnitEs.MainToolWeaponE.ToolWeaponTC.Is(ToolWeaponTypes.BowCrossbow, ToolWeaponTypes.Staff))
                        {
                            eMGame.ToolWeaponsC(_cellEs.UnitMainE.PlayerTC.Player, _cellEs.UnitMainE.LevelTC.Level, _cellEs.UnitEs.MainToolWeaponE.ToolWeaponTC.ToolWeapon)++;
                            _unitSMGame.SetMainTWS.Set(ToolWeaponTypes.Axe, LevelTypes.First);

                            _cellEs.UnitStatsE.StepC.Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;
                        }

                        else
                        {
                            if (_cellEs.UnitEs.MainToolWeaponE.ToolWeaponTC.Is(ToolWeaponTypes.Axe))
                            {
                                if (_cellEs.UnitExtraTWE.ToolWeaponTC.HaveToolWeapon)
                                {
                                    eMGame.PlayerInfoE(ownUnit_0).LevelE(_cellEs.UnitExtraTWE.LevelTC.Level).ToolWeapons(_cellEs.UnitExtraTWE.ToolWeaponTC.ToolWeapon)++;
                                    _cellEs.UnitExtraTWE.ToolWeaponTC.ToolWeapon = ToolWeaponTypes.None;

                                    _cellEs.UnitStatsE.StepC.Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

                                    eMGame.RpcPoolEs.SoundToGeneral(sender, ClipTypes.PickMelee);
                                }

                                else if (eMGame.ToolWeaponsC(ownUnit_0, levTW, twT) > 0)
                                {
                                    eMGame.PlayerInfoE(ownUnit_0).LevelE(levTW).ToolWeapons(twT)--;



                                    _unitSMGame.SetExtraTWS.Set(twT, levTW, _cellEs.UnitExtraTWE.ProtectionC.Protection);

                                    _cellEs.UnitStatsE.StepC.Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

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

                                        _unitSMGame.SetExtraTWS.Set(twT, levTW, protection);

                                        _cellEs.UnitStatsE.StepC.Steps -= StepValues.FOR_GIVE_TAKE_TOOLWEAPON;

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