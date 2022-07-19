using Chessy.Model.Entity;
using Chessy.Model.Values;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Model.System
{
    public sealed class OnPhotonSerializeViewS : SystemModelAbstract
    {
        readonly EntitiesModel _eCopy;

        readonly List<bool> _canSync = new List<bool>();
        readonly List<object> _objsForSync = new List<object>();

        internal OnPhotonSerializeViewS(in SystemsModel sM, in EntitiesModel eM) : base(sM, eM)
        {
            _eCopy = new EntitiesModel(eM.DataFromViewC, eM.RpcC.PunRPCName, new List<object>() { eM.RpcC.Action0, eM.RpcC.Action1 }, eM.TestModeT);
        }

        internal void OnPhotonSerializeView0(in SyncTypes syncType, PhotonStream stream, PhotonMessageInfo info)
        {
            switch (syncType)
            {
                case SyncTypes.Main:
                    {
                        if (stream.IsWriting)
                        {
                            stream.SendNext(_e.NeedGetDataCellsForNextClient);

                            TryAddDataForSync(_e.IsStartedGame, ref _eCopy.IsStartedGame, _canSync, _objsForSync);
                            TryAddDataForSync(_e.WinnerPlayerT, ref _eCopy.WinnerPlayerT, _canSync, _objsForSync);
                            ref var windC = ref _eCopy.WeatherE.WindC;
                            TryAddDataForSync(_e.DirectWindT, ref windC.DirectType, _canSync, _objsForSync);
                            TryAddDataForSync(_e.SpeedWind, ref windC.Speed, _canSync, _objsForSync);
                            ref var sunC = ref _eCopy.SunC;
                            TryAddDataForSync(_e.SunSideT, ref sunC.SunSideType, _canSync, _objsForSync);

                            TrySync(stream, _canSync, _objsForSync);


                            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                            {
                                ref var playerInfoE = ref _e.PlayerInfoE(playerT);
                                ref var playerInfoEcopy = ref _eCopy.PlayerInfoE(playerT);

                                ref var playerInfoC = ref playerInfoE.PlayerInfoC;
                                ref var playerInfoCcopy = ref playerInfoEcopy.PlayerInfoC;

                                TryAddDataForSync(playerInfoC.IsReadyForStartOnlineGame, ref playerInfoCcopy.IsReadyForStartOnlineGame, _canSync, _objsForSync);
                                TryAddDataForSync(playerInfoC.WoodForBuyHouse, ref playerInfoCcopy.WoodForBuyHouse, _canSync, _objsForSync);
                                TryAddDataForSync(playerInfoC.HaveKingInInventor, ref playerInfoCcopy.HaveKingInInventor, _canSync, _objsForSync);
                                TryAddDataForSync(playerInfoC.AmountBuiltHouses, ref playerInfoCcopy.AmountBuiltHouses, _canSync, _objsForSync);
                                ref var pawnPeopleInfoC = ref playerInfoE.PawnInfoC;
                                ref var pawnPeopleInfoCcopy = ref playerInfoEcopy.PawnInfoC;
                                TryAddDataForSync(pawnPeopleInfoC.PeopleInCity, ref pawnPeopleInfoCcopy.PeopleInCity, _canSync, _objsForSync);
                                TryAddDataForSync(pawnPeopleInfoC.AmountInGame, ref pawnPeopleInfoCcopy.AmountInGame, _canSync, _objsForSync);
                                ref var godInfoC = ref playerInfoE.GodInfoC;
                                ref var godInfoCcopy = ref playerInfoEcopy.GodInfoC;
                                TryAddDataForSync(godInfoC.HaveGodInInventor, ref godInfoCcopy.HaveGodInInventor, _canSync, _objsForSync);
                                TryAddDataForSync(godInfoC.UnitType, ref godInfoCcopy.UnitType, _canSync, _objsForSync);
                                TryAddDataForSync(godInfoC.CooldownInSecondsForNextAppearance, ref godInfoCcopy.CooldownInSecondsForNextAppearance, _canSync, _objsForSync);

                                TrySync(stream, _canSync, _objsForSync);


                                for (var buildingT = (BuildingTypes)0; buildingT < BuildingTypes.End; buildingT++)
                                {
                                    var buildingInfoC = playerInfoE.BuildingsInTownInfoC;
                                    var buildingInfoCcopy = playerInfoEcopy.BuildingsInTownInfoC;

                                    TryAddDataForSync(buildingInfoC.HaveBuilding(buildingT), ref buildingInfoCcopy.HaveBuilding(buildingT), _canSync, _objsForSync);
                                }

                                TrySync(stream, _canSync, _objsForSync);



                                for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                                {
                                    for (var twT = (ToolsWeaponsWarriorTypes)1; twT < ToolsWeaponsWarriorTypes.End; twT++)
                                    {
                                        ref var toolsWeapons = ref playerInfoEcopy.HowManyToolWeaponsInInventoryC.ToolWeapons(twT, levelT);

                                        TryAddDataForSync(_e.ToolWeaponsInInventor(playerT, levelT, twT), ref toolsWeapons, _canSync, _objsForSync);
                                    }
                                }

                                TrySync(stream, _canSync, _objsForSync);


                                for (var resT = (ResourceTypes)1; resT < ResourceTypes.End; resT++)
                                {
                                    var resInInventorC = playerInfoE.ResourcesInInventoryC;
                                    var resInInventorCcopy = playerInfoEcopy.ResourcesInInventoryC;

                                    TryAddDataForSync(resInInventorC.Resources(resT), ref resInInventorCcopy.Resources(resT), _canSync, _objsForSync);
                                }

                                TrySync(stream, _canSync, _objsForSync);
                            }


                            for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                            {
                                if (_e.IsBorder(curCellIdx_0)) continue;

                                ref var cellEs = ref _e.CellEs(curCellIdx_0);
                                ref var cellEsCopy = ref _eCopy.CellEs(curCellIdx_0);

                                ref var unitE = ref cellEs.UnitE;
                                ref var unitECopy = ref cellEsCopy.UnitE;

                                ref var unitMainC = ref unitE.MainC;
                                ref var unitMainCCopy = ref unitECopy.MainC;

                                TryAddDataForSync(unitMainC.UnitType, ref unitMainCCopy.UnitType, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainC.LevelType, ref unitMainCCopy.LevelType, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainC.PlayerType, ref unitMainCCopy.PlayerType, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainC.ConditionType, ref unitMainCCopy.ConditionType, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainC.IsArcherDirectedToRight, ref unitMainCCopy.IsArcherDirectedToRight, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainC.DamageSimpleAttack, ref unitMainCCopy.DamageSimpleAttack, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainC.DamageOnCell, ref unitMainCCopy.DamageOnCell, _canSync, _objsForSync);

                                ref var unitHpC = ref unitE.HealthC;
                                ref var unitHpCCopy = ref unitECopy.HealthC;
                                TryAddDataForSync(unitHpC.Health, ref unitHpCCopy.Health, _canSync, _objsForSync);

                                ref var unitMainTwC = ref unitE.MainToolWeaponC;
                                ref var unitMainTwCCopy = ref unitECopy.MainToolWeaponC;
                                TryAddDataForSync(unitMainTwC.ToolWeaponT, ref unitMainTwCCopy.ToolWeaponT, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainTwC.LevelT, ref unitMainTwCCopy.LevelT, _canSync, _objsForSync);

                                ref var unitExtraTwC = ref unitE.ExtraToolWeaponC;
                                ref var unitExtraTwCCopy = ref unitECopy.ExtraToolWeaponC;
                                TryAddDataForSync(unitExtraTwC.ToolWeaponT, ref unitExtraTwCCopy.ToolWeaponT, _canSync, _objsForSync);
                                TryAddDataForSync(unitExtraTwC.LevelT, ref unitExtraTwCCopy.LevelT, _canSync, _objsForSync);
                                TryAddDataForSync(unitExtraTwC.ProtectionShield, ref unitExtraTwCCopy.ProtectionShield, _canSync, _objsForSync);

                                ref var unitDataViewC = ref unitE.WhereViewDataUnitC;
                                ref var unitDataViewCCopy = ref unitECopy.WhereViewDataUnitC;
                                TryAddDataForSync(unitDataViewC.DataIdxCell, ref unitDataViewCCopy.DataIdxCell, _canSync, _objsForSync);
                                TryAddDataForSync(unitDataViewC.ViewIdxCell, ref unitDataViewCCopy.ViewIdxCell, _canSync, _objsForSync);
                                TryAddDataForSync(_e.ShiftingInfoForUnitC(curCellIdx_0).WhereNeedShiftIdxCell, ref _eCopy.ShiftingInfoForUnitC(curCellIdx_0).WhereNeedShiftIdxCell, _canSync, _objsForSync);

                                TrySync(stream, _canSync, _objsForSync);




                                TryAddDataForSync(_e.YoungForestC(curCellIdx_0).Resources, ref _eCopy.YoungForestC(curCellIdx_0).Resources, _canSync, _objsForSync);
                                TryAddDataForSync(_e.AdultForestC(curCellIdx_0).Resources, ref _eCopy.AdultForestC(curCellIdx_0).Resources, _canSync, _objsForSync);
                                TryAddDataForSync(_e.HillC(curCellIdx_0).Resources, ref _eCopy.HillC(curCellIdx_0).Resources, _canSync, _objsForSync);
                                TryAddDataForSync(_e.MountainC(curCellIdx_0).Resources, ref _eCopy.MountainC(curCellIdx_0).Resources, _canSync, _objsForSync);

                                TryAddDataForSync(_e.HaveFire(curCellIdx_0), ref _eCopy.HaveFire(curCellIdx_0), _canSync, _objsForSync);

                                TryAddDataForSync(_e.ProtectionRainyMagicShield(curCellIdx_0), ref _eCopy.UnitEffectsC(curCellIdx_0).ProtectionRainyMagicShield, _canSync, _objsForSync);
                                TryAddDataForSync(_e.HaveFrozenArrawArcher(curCellIdx_0), ref _eCopy.UnitEffectsC(curCellIdx_0).HaveFrozenArrawArcher, _canSync, _objsForSync);

                                TryAddDataForSync(_e.BuildingOnCellT(curCellIdx_0), ref _eCopy.BuildingC(curCellIdx_0).BuildingT, _canSync, _objsForSync);
                                TryAddDataForSync(_e.BuildingLevelT(curCellIdx_0), ref _eCopy.BuildingC(curCellIdx_0).LevelT, _canSync, _objsForSync);
                                TryAddDataForSync(_e.BuildingPlayerT(curCellIdx_0), ref _eCopy.BuildingC(curCellIdx_0).PlayerT, _canSync, _objsForSync);

                                TrySync(stream, _canSync, _objsForSync);




                                for (var abilityT = (AbilityTypes)1; abilityT < AbilityTypes.End; abilityT++)
                                {
                                    TryAddDataForSync(_e.UnitCooldownAbilitiesC(curCellIdx_0).Cooldown(abilityT), ref _eCopy.UnitCooldownAbilitiesC(curCellIdx_0).Cooldown(abilityT), _canSync, _objsForSync);
                                }

                                TrySync(stream, _canSync, _objsForSync);


                                
                                //for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                                //{
                                //    TryAddDataForSync(_e.UnitButtonAbilitiesC(curCellIdx_0).Ability(buttonT), ref _eCopy.UnitButtonAbilitiesC(curCellIdx_0).Ability(buttonT), _canSync, _objsForSync);
                                //    TryAddDataForSync(_e.EffectsUnitsRightBarsC(curCellIdx_0).Effect(buttonT), ref _eCopy.EffectsUnitsRightBarsC(curCellIdx_0).Effect(buttonT), _canSync, _objsForSync);
                                //}

                                //TrySync(stream, _canSync, _objsForSync);



                                //for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                                //{
                                //    TryAddDataForSync(_e.HasKingEffectHereC(curCellIdx_0).Has(playerT), ref _eCopy.HasKingEffectHereC(curCellIdx_0).Has(playerT), _canSync, _objsForSync);
                                //    TryAddDataForSync(_e.UnitVisibleC(curCellIdx_0).IsVisible(playerT), ref _eCopy.UnitVisibleC(curCellIdx_0).IsVisible(playerT), _canSync, _objsForSync);
                                //    TryAddDataForSync(_e.TrailVisibleC(curCellIdx_0).IsVisible(playerT), ref _eCopy.TrailVisibleC(curCellIdx_0).IsVisible(playerT), _canSync, _objsForSync);
                                //    TryAddDataForSync(_e.BuildingVisibleC(curCellIdx_0).IsVisible(playerT), ref _eCopy.BuildingVisibleC(curCellIdx_0).IsVisible(playerT), _canSync, _objsForSync);
                                //}

                                //TrySync(stream, _canSync, _objsForSync);


                                for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                                {
                                    TryAddDataForSync(_e.HealthTrail(curCellIdx_0).Health(directT), ref _eCopy.HealthTrail(curCellIdx_0).Health(directT), _canSync, _objsForSync);
                                }
                                TrySync(stream, _canSync, _objsForSync);



                                TryAddDataForSync(_e.WaterOnCellC(curCellIdx_0).Resources, ref _eCopy.WaterOnCellC(curCellIdx_0).Resources, _canSync, _objsForSync);
                                TryAddDataForSync(_e.WaterUnitC(curCellIdx_0).Water, ref _eCopy.WaterUnitC(curCellIdx_0).Water, _canSync, _objsForSync);
                                TryAddDataForSync(_e.UnitMainC(curCellIdx_0).CooldownForAttackAnyUnitInSeconds, ref _eCopy.UnitMainC(curCellIdx_0).CooldownForAttackAnyUnitInSeconds, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainC.HowManySecondUnitWasHereInThisCondition, ref unitMainCCopy.HowManySecondUnitWasHereInThisCondition, _canSync, _objsForSync);

                                TrySync(stream, _canSync, _objsForSync);


                                TryAddDataForSync(_e.CloudC(curCellIdx_0).IsCenter, ref _eCopy.CloudC(curCellIdx_0).IsCenter, _canSync, _objsForSync);
                                TryAddDataForSync(_e.CloudWhereViewDataOnCellC(curCellIdx_0).DataIdxCell, ref _eCopy.CloudWhereViewDataOnCellC(curCellIdx_0).DataIdxCell, _canSync, _objsForSync);
                                TryAddDataForSync(_e.CloudWhereViewDataOnCellC(curCellIdx_0).ViewIdxCell, ref _eCopy.CloudWhereViewDataOnCellC(curCellIdx_0).ViewIdxCell, _canSync, _objsForSync);

                                TrySync(stream, _canSync, _objsForSync);
                            }
                        }

                        else
                        {
                            _e.NeedGetDataCellsForNextClient = (bool)stream.ReceiveNext();

                            if ((bool)stream.ReceiveNext())
                            {
                                if ((bool)stream.ReceiveNext()) _e.IsStartedGame = (bool)stream.ReceiveNext();
                                if ((bool)stream.ReceiveNext()) _e.WinnerPlayerT = (PlayerTypes)stream.ReceiveNext();
                                if ((bool)stream.ReceiveNext()) _e.DirectWindT = (DirectTypes)stream.ReceiveNext();
                                if ((bool)stream.ReceiveNext()) _e.SpeedWind = (byte)stream.ReceiveNext();
                                if ((bool)stream.ReceiveNext()) _e.SunSideT = (SunSideTypes)stream.ReceiveNext();
                            }

                            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                            {
                                if ((bool)stream.ReceiveNext())
                                {
                                    if ((bool)stream.ReceiveNext()) _e.PlayerInfoC(playerT).IsReadyForStartOnlineGame = (bool)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.PlayerInfoC(playerT).WoodForBuyHouse = (float)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.PlayerInfoC(playerT).HaveKingInInventor = (bool)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.PlayerInfoC(playerT).AmountBuiltHouses = (int)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _e.PawnPeopleInfoC(playerT).PeopleInCity = (int)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.PawnPeopleInfoC(playerT).AmountInGame = (int)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _e.GodInfoC(playerT).HaveGodInInventor = (bool)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.GodInfoC(playerT).UnitType = (UnitTypes)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.GodInfoC(playerT).CooldownInSecondsForNextAppearance = (int)stream.ReceiveNext();
                                }


                                if ((bool)stream.ReceiveNext())
                                {
                                    for (var buildingT = (BuildingTypes)0; buildingT < BuildingTypes.End; buildingT++)
                                    {
                                        if ((bool)stream.ReceiveNext()) _e.BuildingsInTownInfoC(playerT).HaveBuilding(buildingT) = (bool)stream.ReceiveNext();
                                    }
                                }

                                if ((bool)stream.ReceiveNext())
                                {
                                    for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                                    {
                                        for (var twT = (ToolsWeaponsWarriorTypes)1; twT < ToolsWeaponsWarriorTypes.End; twT++)
                                        {
                                            if ((bool)stream.ReceiveNext()) _e.SetToolWeaponsInInventor(playerT, levelT, twT, (int)stream.ReceiveNext());
                                        }
                                    }
                                }

                                if ((bool)stream.ReceiveNext())
                                {
                                    for (var resT = (ResourceTypes)1; resT < ResourceTypes.End; resT++)
                                    {
                                        if ((bool)stream.ReceiveNext()) _e.SetResourcesInInventory(playerT, resT, (float)stream.ReceiveNext());
                                    }
                                }
                            }


                            for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                            {
                                if (_e.IsBorder(curCellIdx_0)) continue;

                                ref var cellEs = ref _e.CellEs(curCellIdx_0);

                                ref var unitE = ref cellEs.UnitE;
                                ref var unitMainC = ref unitE.MainC;


                                if ((bool)stream.ReceiveNext())
                                {
                                    if ((bool)stream.ReceiveNext()) _e.SetUnitOnCellT(curCellIdx_0, (UnitTypes)stream.ReceiveNext());
                                    if ((bool)stream.ReceiveNext()) _e.SetUnitLevelT(curCellIdx_0, (LevelTypes)stream.ReceiveNext());
                                    if ((bool)stream.ReceiveNext()) _e.SetUnitPlayerT(curCellIdx_0, (PlayerTypes)stream.ReceiveNext());
                                    if ((bool)stream.ReceiveNext()) _e.SetUnitConditionT(curCellIdx_0, (ConditionUnitTypes)stream.ReceiveNext());
                                    if ((bool)stream.ReceiveNext()) unitMainC.IsArcherDirectedToRight = (bool)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) unitMainC.DamageSimpleAttack = (double)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) unitMainC.DamageOnCell = (double)stream.ReceiveNext();


                                    if ((bool)stream.ReceiveNext()) _e.HpUnitC(curCellIdx_0).Health = (double)stream.ReceiveNext();


                                    ref var unitMainTWC = ref unitE.MainToolWeaponC;
                                    if ((bool)stream.ReceiveNext()) unitMainTWC.ToolWeaponT = (ToolsWeaponsWarriorTypes)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) unitMainTWC.LevelT = (LevelTypes)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _e.SetExtraToolWeaponT(curCellIdx_0, (ToolsWeaponsWarriorTypes)stream.ReceiveNext());
                                    if ((bool)stream.ReceiveNext()) _e.SetExtraTWLevelT(curCellIdx_0, (LevelTypes)stream.ReceiveNext());
                                    if ((bool)stream.ReceiveNext()) _e.SetExtraTWProtection(curCellIdx_0, (float)stream.ReceiveNext());

                                    if ((bool)stream.ReceiveNext()) _e.WhereViewDataUnitC(curCellIdx_0).DataIdxCell = (byte)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.WhereViewDataUnitC(curCellIdx_0).ViewIdxCell = (byte)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _e.ShiftingInfoForUnitC(curCellIdx_0).WhereNeedShiftIdxCell = (byte)stream.ReceiveNext();
                                }

                                if ((bool)stream.ReceiveNext())
                                {
                                    if ((bool)stream.ReceiveNext()) _e.YoungForestC(curCellIdx_0).Resources = (float)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.AdultForestC(curCellIdx_0).Resources = (float)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.HillC(curCellIdx_0).Resources = (float)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.MountainC(curCellIdx_0).Resources = (float)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _e.EffectE(curCellIdx_0).HaveFire = (bool)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _e.UnitEffectsC(curCellIdx_0).ProtectionRainyMagicShield = (float)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.UnitEffectsC(curCellIdx_0).HaveFrozenArrawArcher = (bool)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _e.BuildingC(curCellIdx_0).BuildingT = (BuildingTypes)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.BuildingC(curCellIdx_0).LevelT = (LevelTypes)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.BuildingC(curCellIdx_0).PlayerT = (PlayerTypes)stream.ReceiveNext();
                                }


                                if ((bool)stream.ReceiveNext())
                                {
                                    for (var abilityT = (AbilityTypes)1; abilityT < AbilityTypes.End; abilityT++)
                                    {
                                        if ((bool)stream.ReceiveNext()) _e.UnitCooldownAbilitiesC(curCellIdx_0).Cooldown(abilityT) = (int)stream.ReceiveNext();
                                    }
                                }


                                

                                //if ((bool)stream.ReceiveNext())
                                //{
                                //    for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                                //    {
                                //        if ((bool)stream.ReceiveNext()) _e.UnitButtonAbilitiesC(curCellIdx_0).SetAbility(buttonT, (AbilityTypes)stream.ReceiveNext());
                                //        if ((bool)stream.ReceiveNext()) _e.EffectsUnitsRightBarsC(curCellIdx_0).Set(buttonT, (EffectTypes)stream.ReceiveNext());
                                //    }
                                //}

                                //if ((bool)stream.ReceiveNext())
                                //{
                                //    for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                                //    {
                                //        if ((bool)stream.ReceiveNext()) _e.HasKingEffectHereC(curCellIdx_0).Set(playerT, (bool)stream.ReceiveNext());
                                //        if ((bool)stream.ReceiveNext()) _e.UnitVisibleC(curCellIdx_0).Set(playerT, (bool)stream.ReceiveNext());
                                //        if ((bool)stream.ReceiveNext()) _e.TrailVisibleC(curCellIdx_0).Set(playerT, (bool)stream.ReceiveNext());
                                //        if ((bool)stream.ReceiveNext()) _e.BuildingVisibleC(curCellIdx_0).Set(playerT, (bool)stream.ReceiveNext());
                                //    }
                                //}


                                if ((bool)stream.ReceiveNext())
                                {
                                    for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                                    {
                                        if ((bool)stream.ReceiveNext()) _e.HealthTrail(curCellIdx_0).Set(directT, (float)stream.ReceiveNext());
                                    }
                                }

                                if ((bool)stream.ReceiveNext())
                                {
                                    if ((bool)stream.ReceiveNext()) _e.WaterOnCellC(curCellIdx_0).Resources = (float)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.WaterUnitC(curCellIdx_0).Water = (double)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.UnitMainC(curCellIdx_0).CooldownForAttackAnyUnitInSeconds = (int)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) unitMainC.HowManySecondUnitWasHereInThisCondition = (int)stream.ReceiveNext();
                                }

                                if ((bool)stream.ReceiveNext())
                                {
                                    if ((bool)stream.ReceiveNext()) _e.CloudC(curCellIdx_0).IsCenter = (bool)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _e.CloudWhereViewDataOnCellC(curCellIdx_0).DataIdxCell = (byte)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.CloudWhereViewDataOnCellC(curCellIdx_0).ViewIdxCell = (byte)stream.ReceiveNext();

                                }
                            }

                            _s.GetDataCellsS.GetDataCells();
                            _e.NeedUpdateView = true;


                            Debug.Log("Synchronization");
                        }
                    }
                    break;

                case SyncTypes.UnitShift:
                    {
                        if (stream.IsWriting)
                        {
                            for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                            {
                                if (_e.IsBorder(curCellIdx_0)) continue;

                                var unitPos = _e.UnitPossitionOnCell(curCellIdx_0);
                                TryAddDataForSync(unitPos.x, ref _eCopy.UnitPossitionOnCellC(curCellIdx_0).Position.x, _canSync, _objsForSync);
                                TryAddDataForSync(unitPos.y, ref _eCopy.UnitPossitionOnCellC(curCellIdx_0).Position.y, _canSync, _objsForSync);
                            }

                            TrySync(stream, _canSync, _objsForSync);
                        }
                        else
                        {
                            if ((bool)stream.ReceiveNext())
                            {
                                for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                                {
                                    if (_e.IsBorder(curCellIdx_0)) continue;

                                    if ((bool)stream.ReceiveNext()) _e.UnitPossitionOnCellC(curCellIdx_0).X = (float)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.UnitPossitionOnCellC(curCellIdx_0).Y = (float)stream.ReceiveNext();

                                }
                            }
                        }
                    }
                    break;

                case SyncTypes.CloudShift:
                    {
                        if (stream.IsWriting)
                        {
                            for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                            {
                                if (_e.IsBorder(curCellIdx_0)) continue;

                                var cloudPos = _e.CloudPossitionC(curCellIdx_0).Position;
                                TryAddDataForSync(cloudPos.x, ref _eCopy.CloudPossitionC(curCellIdx_0).Position.x, _canSync, _objsForSync);
                                TryAddDataForSync(cloudPos.y, ref _eCopy.CloudPossitionC(curCellIdx_0).Position.y, _canSync, _objsForSync);
                            }
                            TrySync(stream, _canSync, _objsForSync);
                        }
                        else
                        {
                            if ((bool)stream.ReceiveNext())
                            {
                                for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                                {
                                    if (_e.IsBorder(curCellIdx_0)) continue;

                                    if ((bool)stream.ReceiveNext()) _e.CloudPossitionC(curCellIdx_0).X = (float)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.CloudPossitionC(curCellIdx_0).Y = (float)stream.ReceiveNext();
                                }
                            }
                        }
                    }
                    break;

                case SyncTypes.Else:
                    {
                        //if (stream.IsWriting)
                        //{
                        //    for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                        //    {
                        //        if (_e.IsBorder(curCellIdx_0)) continue;


                        //        for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                        //        {
                        //            TryAddDataForSync(_e.WhereUnitCanAttackUniqueAttackToEnemyC(curCellIdx_0).Can(cellIdxCurrent), ref _eCopy.WhereUnitCanAttackUniqueAttackToEnemyC(curCellIdx_0).Can(cellIdxCurrent), _canSync, _objsForSync);
                        //            TryAddDataForSync(_e.WhereUnitCanShiftC(curCellIdx_0).CanShiftHere(cellIdxCurrent), ref _eCopy.WhereUnitCanShiftC(curCellIdx_0).CanShiftHere(cellIdxCurrent), _canSync, _objsForSync);
                        //            TryAddDataForSync(_e.WhereUnitCanFireAdultForestC(curCellIdx_0).Can(cellIdxCurrent), ref _eCopy.WhereUnitCanFireAdultForestC(curCellIdx_0).Can(cellIdxCurrent), _canSync, _objsForSync);
                        //            TryAddDataForSync(_e.WhereUnitCanAttackSimpleAttackToEnemyC(curCellIdx_0).Can(cellIdxCurrent), ref _eCopy.WhereUnitCanAttackSimpleAttackToEnemyC(curCellIdx_0).Can(cellIdxCurrent), _canSync, _objsForSync);
                        //        }
                        //        TrySync(stream, _canSync, _objsForSync);
                        //    }
                        //}

                        //else
                        //{
                        //    for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                        //    {
                        //        if (_e.IsBorder(curCellIdx_0)) continue;

                        //        if ((bool)stream.ReceiveNext())
                        //        {
                        //            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                        //            {
                        //                if ((bool)stream.ReceiveNext()) _e.WhereUnitCanAttackUniqueAttackToEnemyC(curCellIdx_0).Set(cellIdxCurrent, (bool)stream.ReceiveNext());
                        //                if ((bool)stream.ReceiveNext()) _e.WhereUnitCanShiftC(curCellIdx_0).Set(cellIdxCurrent, (bool)stream.ReceiveNext());
                        //                if ((bool)stream.ReceiveNext()) _e.WhereUnitCanFireAdultForestC(curCellIdx_0).Can(cellIdxCurrent) = (bool)stream.ReceiveNext();
                        //                if ((bool)stream.ReceiveNext()) _e.WhereUnitCanAttackSimpleAttackToEnemyC(curCellIdx_0).Set(cellIdxCurrent, (bool)stream.ReceiveNext());
                        //            }
                        //        }

                        //        //if ((bool)stream.ReceiveNext())
                        //        //{
                        //        //    for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                        //        //    {
                                       
                        //        //    }
                        //        //}

                        //        //if ((bool)stream.ReceiveNext())
                        //        //{
                        //        //    for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                        //        //    {
                                        
                        //        //    }
                        //        //}

                        //        //if ((bool)stream.ReceiveNext())
                        //        //{
                        //        //    for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                        //        //    {
                                        
                        //        //    }
                        //        //}
                        //    }
                        //}
                    }
                    break;

                default: throw new Exception();
            }

            
        }


        void TrySync(in PhotonStream stream, in List<bool> needSyncList, in List<object> objsForSync)
        {
            var needSync = false;

            for (var i = 0; i < _canSync.Count; i++)
            {
                if (needSyncList[i])
                {
                    needSync = true;
                    break;
                }
            }

            if (needSync)
            {
                stream.SendNext(needSync);

                for (var i = 0; i < needSyncList.Count; i++)
                {
                    if (needSyncList[i])
                    {
                        stream.SendNext(true);
                        stream.SendNext(objsForSync[i]);
                    }
                    else
                    {
                        stream.SendNext(false);
                    }
                }
            }

            needSyncList.Clear();
            objsForSync.Clear();
        }

        bool TryAddDataForSync<T>(T one, ref T twoCopy, in List<bool> canSyncList, in List<object> objs) where T : struct
        {
            var canSync = (object)one != (object)twoCopy;
            canSyncList.Add(canSync);
            if (canSync)
            {
                twoCopy = one;
                objs.Add(one);
            }
            return canSync;
        }
    }
}