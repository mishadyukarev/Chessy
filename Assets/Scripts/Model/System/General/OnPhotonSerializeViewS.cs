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
            _eCopy = new EntitiesModel(_dataFromViewC, eM.CommonGameE.RpcC.PunRPCName, new List<object>() { eM.CommonGameE.RpcC.Action0, eM.CommonGameE.RpcC.Action1 }, _aboutGameC.TestModeT);
        }

        internal void OnPhotonSerializeView0(in SyncTypes syncType, PhotonStream stream, PhotonMessageInfo info)
        {
            switch (syncType)
            {
                case SyncTypes.Main:
                    {
                        if (stream.IsWriting)
                        {
                            TryAddDataForSync(_aboutGameC.IsStartedGame, ref _eCopy.CommonGameE.CommonInfoAboutGameC.IsStartedGame, _canSync, _objsForSync);
                            TryAddDataForSync(_aboutGameC.WinnerPlayerT, ref _eCopy.CommonGameE.CommonInfoAboutGameC.WinnerPlayerT, _canSync, _objsForSync);
                            var weatherE = _eCopy.WeatherE;
                            var windC = weatherE.WindC;
                            TryAddDataForSync(_windC.DirectT, ref windC.DirectT, _canSync, _objsForSync);
                            TryAddDataForSync(_windC.Speed, ref windC.Speed, _canSync, _objsForSync);
                            var sunC = weatherE.SunC;
                            TryAddDataForSync(_sunC.SunSideT, ref sunC.SunSideT, _canSync, _objsForSync);

                            TrySync(stream, _canSync, _objsForSync);


                            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                            {
                                ref var playerInfoE = ref _e.PlayerInfoE(playerT);
                                ref var playerInfoEcopy = ref _eCopy.PlayerInfoE(playerT);

                                var playerInfoC = playerInfoE.PlayerInfoC;
                                var playerInfoCcopy = playerInfoEcopy.PlayerInfoC;

                                TryAddDataForSync(playerInfoC.IsReadyForStartOnlineGame, ref playerInfoCcopy.IsReadyForStartOnlineGame, _canSync, _objsForSync);
                                TryAddDataForSync(playerInfoC.WoodForBuyHouse, ref playerInfoCcopy.WoodForBuyHouse, _canSync, _objsForSync);
                                TryAddDataForSync(playerInfoC.HaveKingInInventor, ref playerInfoCcopy.HaveKingInInventor, _canSync, _objsForSync);
                                TryAddDataForSync(playerInfoC.AmountBuiltHouses, ref playerInfoCcopy.AmountBuiltHouses, _canSync, _objsForSync);
                                var pawnPeopleInfoC = playerInfoE.PawnInfoC;
                                var pawnPeopleInfoCcopy = playerInfoEcopy.PawnInfoC;
                                TryAddDataForSync(pawnPeopleInfoC.PeopleInCity, ref pawnPeopleInfoCcopy.PeopleInCity, _canSync, _objsForSync);
                                TryAddDataForSync(pawnPeopleInfoC.AmountInGame, ref pawnPeopleInfoCcopy.AmountInGame, _canSync, _objsForSync);
                                var godInfoC = playerInfoE.GodInfoC;
                                var godInfoCcopy = playerInfoEcopy.GodInfoC;
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
                                if (_cellCs[curCellIdx_0].IsBorder) continue;

                                var cellEs = _e.CellEs(curCellIdx_0);
                                var cellEsCopy = _eCopy.CellEs(curCellIdx_0);

                                var unitE = cellEs.UnitE;
                                var unitECopy = cellEsCopy.UnitE;

                                var unitMainC = unitE.MainC;
                                var unitMainCCopy = unitECopy.MainC;

                                var buildingC = _buildingCs[curCellIdx_0];
                                var buildingCCopy = cellEsCopy.BuildingE.BuildingMainC;

                                var unitHpC = unitE.HealthC;
                                var unitHpCCopy = unitECopy.HealthC;

                                var unitMainTwC = unitE.MainToolWeaponC;
                                var unitMainTwCCopy = unitECopy.MainToolWeaponC;

                                var unitExtraTwC = unitE.ExtraToolWeaponC;
                                var unitExtraTwCCopy = unitECopy.ExtraToolWeaponC;

                                var unitDataViewC = unitE.WhereViewDataUnitC;
                                var unitDataViewCCopy = unitECopy.WhereViewDataUnitC;

                                var trailHealthCCopy = cellEsCopy.TrailE.HealthC;
                                var cloudECopy = cellEsCopy.CloudE;


                                TryAddDataForSync(unitMainC.UnitT, ref unitMainCCopy.UnitT, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainC.LevelT, ref unitMainCCopy.LevelT, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainC.PlayerType, ref unitMainCCopy.PlayerT, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainC.ConditionType, ref unitMainCCopy.ConditionT, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainC.IsArcherDirectedToRight, ref unitMainCCopy.IsArcherDirectedToRight, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainC.DamageSimpleAttack, ref unitMainCCopy.DamageSimpleAttack, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainC.DamageOnCell, ref unitMainCCopy.DamageOnCell, _canSync, _objsForSync);

                                TryAddDataForSync(unitHpC.Health, ref unitHpCCopy.Health, _canSync, _objsForSync);

                                TryAddDataForSync(unitMainTwC.ToolWeaponT, ref unitMainTwCCopy.ToolWeaponT, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainTwC.LevelT, ref unitMainTwCCopy.LevelT, _canSync, _objsForSync);

                                TryAddDataForSync(unitExtraTwC.ToolWeaponT, ref unitExtraTwCCopy.ToolWeaponT, _canSync, _objsForSync);
                                TryAddDataForSync(unitExtraTwC.LevelT, ref unitExtraTwCCopy.LevelT, _canSync, _objsForSync);
                                TryAddDataForSync(unitExtraTwC.ProtectionShield, ref unitExtraTwCCopy.ProtectionShield, _canSync, _objsForSync);

                                TryAddDataForSync(unitDataViewC.DataIdxCell, ref unitDataViewCCopy.DataIdxCell, _canSync, _objsForSync);
                                TryAddDataForSync(unitDataViewC.ViewIdxCell, ref unitDataViewCCopy.ViewIdxCell, _canSync, _objsForSync);
                                TryAddDataForSync(_shiftingUnitCs[curCellIdx_0].WhereNeedShiftIdxCell, ref unitECopy.ShiftingInfoForUnitC.WhereNeedShiftIdxCell, _canSync, _objsForSync);
                                TryAddDataForSync(_effectsUnitCs[curCellIdx_0].StunHowManyUpdatesNeedStay, ref unitECopy.EffectsC.StunHowManyUpdatesNeedStay, _canSync, _objsForSync);

                                TrySync(stream, _canSync, _objsForSync);


                                TryAddDataForSync(_e.YoungForestC(curCellIdx_0).Resources, ref _eCopy.YoungForestC(curCellIdx_0).Resources, _canSync, _objsForSync);
                                TryAddDataForSync(_e.AdultForestC(curCellIdx_0).Resources, ref _eCopy.AdultForestC(curCellIdx_0).Resources, _canSync, _objsForSync);
                                TryAddDataForSync(_e.HillC(curCellIdx_0).Resources, ref _eCopy.HillC(curCellIdx_0).Resources, _canSync, _objsForSync);
                                TryAddDataForSync(_e.MountainC(curCellIdx_0).Resources, ref _eCopy.MountainC(curCellIdx_0).Resources, _canSync, _objsForSync);

                                TryAddDataForSync(_fireCs[curCellIdx_0].HaveFire, ref cellEsCopy.EffectE.FireC.HaveFire, _canSync, _objsForSync);

                                TryAddDataForSync(_effectsUnitCs[curCellIdx_0].ProtectionRainyMagicShield, ref _eCopy.CellEs(curCellIdx_0).UnitE.EffectsC.ProtectionRainyMagicShield, _canSync, _objsForSync);
                                TryAddDataForSync(_effectsUnitCs[curCellIdx_0].HaveFrozenArrawArcher, ref _eCopy.CellEs(curCellIdx_0).UnitE.EffectsC.HaveFrozenArrawArcher, _canSync, _objsForSync);


                                TryAddDataForSync(buildingC.BuildingT, ref buildingCCopy.BuildingT, _canSync, _objsForSync);
                                TryAddDataForSync(buildingC.LevelT, ref buildingCCopy.LevelT, _canSync, _objsForSync);
                                TryAddDataForSync(buildingC.PlayerT, ref buildingCCopy.PlayerT, _canSync, _objsForSync);

                                TrySync(stream, _canSync, _objsForSync);




                                for (var abilityT = (AbilityTypes)1; abilityT < AbilityTypes.End; abilityT++)
                                {
                                    TryAddDataForSync(_cooldownAbilityCs[curCellIdx_0].Cooldown(abilityT), ref unitECopy.CooldownsC.Cooldown(abilityT), _canSync, _objsForSync);
                                }

                                TrySync(stream, _canSync, _objsForSync);


                                
                                //for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                                //{
                                //    TryAddDataForSync(_e.UnitButtonAbilitiesC(curCellIdx_0).Ability(buttonT), ref _eCopy.UnitButtonAbilitiesC(curCellIdx_0).Ability(buttonT), _canSync, _objsForSync);
                                //    TryAddDataForSync(_effectsUnitsRightBarsCs[curCellIdx_0).Effect(buttonT), ref _eCopy.EffectsUnitsRightBarsC(curCellIdx_0).Effect(buttonT), _canSync, _objsForSync);
                                //}

                                //TrySync(stream, _canSync, _objsForSync);



                                //for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                                //{
                                //    TryAddDataForSync(_e.HasKingEffectHereC(curCellIdx_0).Has(playerT), ref _eCopy.HasKingEffectHereC(curCellIdx_0).Has(playerT), _canSync, _objsForSync);
                                //    TryAddDataForSync(_unitVisibleCs[curCellIdx_0).IsVisible(playerT), ref _eCopy.UnitVisibleC(curCellIdx_0).IsVisible(playerT), _canSync, _objsForSync);
                                //    TryAddDataForSync(_e.TrailVisibleC(curCellIdx_0).IsVisible(playerT), ref _eCopy.TrailVisibleC(curCellIdx_0).IsVisible(playerT), _canSync, _objsForSync);
                                //    TryAddDataForSync(_visibleBuildingCs[curCellIdx_0).IsVisible(playerT), ref _eCopy.BuildingVisibleC(curCellIdx_0).IsVisible(playerT), _canSync, _objsForSync);
                                //}

                                //TrySync(stream, _canSync, _objsForSync);


                                for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                                {
                                    TryAddDataForSync(_hpTrailCs[curCellIdx_0].Health(directT), ref trailHealthCCopy.Health(directT), _canSync, _objsForSync);
                                }
                                TrySync(stream, _canSync, _objsForSync);



                                TryAddDataForSync(_e.WaterOnCellC(curCellIdx_0).Resources, ref _eCopy.WaterOnCellC(curCellIdx_0).Resources, _canSync, _objsForSync);
                                TryAddDataForSync(_unitWaterCs[curCellIdx_0].Water, ref unitECopy.WaterC.Water, _canSync, _objsForSync);
                                TryAddDataForSync(_unitCs[curCellIdx_0].CooldownForAttackAnyUnitInSeconds, ref unitMainCCopy.CooldownForAttackAnyUnitInSeconds, _canSync, _objsForSync);
                                TryAddDataForSync(unitMainC.HowManySecondUnitWasHereInThisCondition, ref unitMainCCopy.HowManySecondUnitWasHereInThisCondition, _canSync, _objsForSync);

                                TrySync(stream, _canSync, _objsForSync);


                                TryAddDataForSync(_cloudCs[curCellIdx_0].IsCenter, ref cloudECopy.CloudC.IsCenter, _canSync, _objsForSync);
                                TryAddDataForSync(_e.CloudWhereViewDataOnCellC(curCellIdx_0).DataIdxCell, ref _eCopy.CloudWhereViewDataOnCellC(curCellIdx_0).DataIdxCell, _canSync, _objsForSync);
                                TryAddDataForSync(_e.CloudWhereViewDataOnCellC(curCellIdx_0).ViewIdxCell, ref _eCopy.CloudWhereViewDataOnCellC(curCellIdx_0).ViewIdxCell, _canSync, _objsForSync);

                                TrySync(stream, _canSync, _objsForSync);
                            }
                        }

                        else
                        {
                            if ((bool)stream.ReceiveNext())
                            {
                                if ((bool)stream.ReceiveNext()) _aboutGameC.IsStartedGame = (bool)stream.ReceiveNext();
                                if ((bool)stream.ReceiveNext()) _aboutGameC.WinnerPlayerT = (PlayerTypes)stream.ReceiveNext();
                                if ((bool)stream.ReceiveNext()) _windC.DirectT = (DirectTypes)stream.ReceiveNext();
                                if ((bool)stream.ReceiveNext()) _windC.Speed = (byte)stream.ReceiveNext();
                                if ((bool)stream.ReceiveNext()) _sunC.SunSideT = (SunSideTypes)stream.ReceiveNext();
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
                                if (_cellCs[curCellIdx_0].IsBorder) continue;

                                var cellEs = _e.CellEs(curCellIdx_0);

                                var unitE = cellEs.UnitE;
                                var unitMainC = unitE.MainC;


                                if ((bool)stream.ReceiveNext())
                                {
                                    if ((bool)stream.ReceiveNext()) _unitCs[curCellIdx_0].UnitT = (UnitTypes)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _unitCs[curCellIdx_0].LevelT = (LevelTypes)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _unitCs[curCellIdx_0].PlayerT = (PlayerTypes)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _unitCs[curCellIdx_0].ConditionT = (ConditionUnitTypes)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) unitMainC.IsArcherDirectedToRight = (bool)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) unitMainC.DamageSimpleAttack = (double)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) unitMainC.DamageOnCell = (double)stream.ReceiveNext();


                                    if ((bool)stream.ReceiveNext()) _hpUnitCs[curCellIdx_0].Health = (double)stream.ReceiveNext();


                                    var unitMainTWC = unitE.MainToolWeaponC;
                                    if ((bool)stream.ReceiveNext()) unitMainTWC.ToolWeaponT = (ToolsWeaponsWarriorTypes)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) unitMainTWC.LevelT = (LevelTypes)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _extraTWC[curCellIdx_0].ToolWeaponT = (ToolsWeaponsWarriorTypes)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _extraTWC[curCellIdx_0].LevelT = (LevelTypes)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _extraTWC[curCellIdx_0].ProtectionShield = (float)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _unitWhereViewDataCs[curCellIdx_0].DataIdxCell = (byte)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _unitWhereViewDataCs[curCellIdx_0].ViewIdxCell = (byte)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _shiftingUnitCs[curCellIdx_0].WhereNeedShiftIdxCell = (byte)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _effectsUnitCs[curCellIdx_0].StunHowManyUpdatesNeedStay = (float)stream.ReceiveNext();
                                }

                                if ((bool)stream.ReceiveNext())
                                {
                                    if ((bool)stream.ReceiveNext()) _e.YoungForestC(curCellIdx_0).Resources = (float)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.AdultForestC(curCellIdx_0).Resources = (float)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.HillC(curCellIdx_0).Resources = (float)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.MountainC(curCellIdx_0).Resources = (float)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _fireCs[curCellIdx_0].HaveFire = (bool)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _effectsUnitCs[curCellIdx_0].ProtectionRainyMagicShield = (float)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _effectsUnitCs[curCellIdx_0].HaveFrozenArrawArcher = (bool)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _buildingCs[curCellIdx_0].BuildingT = (BuildingTypes)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _buildingCs[curCellIdx_0].LevelT = (LevelTypes)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _buildingCs[curCellIdx_0].PlayerT = (PlayerTypes)stream.ReceiveNext();
                                }


                                if ((bool)stream.ReceiveNext())
                                {
                                    for (var abilityT = (AbilityTypes)1; abilityT < AbilityTypes.End; abilityT++)
                                    {
                                        if ((bool)stream.ReceiveNext()) _cooldownAbilityCs[curCellIdx_0].Cooldown(abilityT) = (int)stream.ReceiveNext();
                                    }
                                }


                                

                                //if ((bool)stream.ReceiveNext())
                                //{
                                //    for (var buttonT = (ButtonTypes)1; buttonT < ButtonTypes.End; buttonT++)
                                //    {
                                //        if ((bool)stream.ReceiveNext()) _e.UnitButtonAbilitiesC(curCellIdx_0).SetAbility(buttonT, (AbilityTypes)stream.ReceiveNext());
                                //        if ((bool)stream.ReceiveNext()) _effectsUnitsRightBarsCs[curCellIdx_0).Set(buttonT, (EffectTypes)stream.ReceiveNext());
                                //    }
                                //}

                                //if ((bool)stream.ReceiveNext())
                                //{
                                //    for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
                                //    {
                                //        if ((bool)stream.ReceiveNext()) _e.HasKingEffectHereC(curCellIdx_0).Set(playerT, (bool)stream.ReceiveNext());
                                //        if ((bool)stream.ReceiveNext()) _unitVisibleCs[curCellIdx_0).Set(playerT, (bool)stream.ReceiveNext());
                                //        if ((bool)stream.ReceiveNext()) _e.TrailVisibleC(curCellIdx_0).Set(playerT, (bool)stream.ReceiveNext());
                                //        if ((bool)stream.ReceiveNext()) _visibleBuildingCs[curCellIdx_0).Set(playerT, (bool)stream.ReceiveNext());
                                //    }
                                //}


                                if ((bool)stream.ReceiveNext())
                                {
                                    for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                                    {
                                        if ((bool)stream.ReceiveNext()) _hpTrailCs[curCellIdx_0].Set(directT, (float)stream.ReceiveNext());
                                    }
                                }

                                if ((bool)stream.ReceiveNext())
                                {
                                    if ((bool)stream.ReceiveNext()) _e.WaterOnCellC(curCellIdx_0).Resources = (float)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _unitWaterCs[curCellIdx_0].Water = (double)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _unitCs[curCellIdx_0].CooldownForAttackAnyUnitInSeconds = (int)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) unitMainC.HowManySecondUnitWasHereInThisCondition = (int)stream.ReceiveNext();
                                }

                                if ((bool)stream.ReceiveNext())
                                {
                                    if ((bool)stream.ReceiveNext()) _cloudCs[curCellIdx_0].IsCenter = (bool)stream.ReceiveNext();

                                    if ((bool)stream.ReceiveNext()) _e.CloudWhereViewDataOnCellC(curCellIdx_0).DataIdxCell = (byte)stream.ReceiveNext();
                                    if ((bool)stream.ReceiveNext()) _e.CloudWhereViewDataOnCellC(curCellIdx_0).ViewIdxCell = (byte)stream.ReceiveNext();

                                }
                            }

                            _s.GetDataCellsS.GetDataCells();
                            _updateAllViewC.NeedUpdateView = true;


                            Debug.Log("Synchronization");
                        }
                    }
                    break;

                case SyncTypes.UnitShift:
                    {
                        if (stream.IsWriting)
                        {
                            //for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                            //{
                            //    if (_e.IsBorder(curCellIdx_0)) continue;

                            //    var unitPos = _e.UnitPossitionOnCell(curCellIdx_0);
                            //    TryAddDataForSync(unitPos.x, ref _eCopy.UnitPossitionOnCellC(curCellIdx_0).Position.x, _canSync, _objsForSync);
                            //    TryAddDataForSync(unitPos.y, ref _eCopy.UnitPossitionOnCellC(curCellIdx_0).Position.y, _canSync, _objsForSync);
                            //}

                            //TrySync(stream, _canSync, _objsForSync);
                        }
                        else
                        {
                            //if ((bool)stream.ReceiveNext())
                            //{
                            //    for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                            //    {
                            //        if (_e.IsBorder(curCellIdx_0)) continue;

                            //        if ((bool)stream.ReceiveNext()) _e.UnitPossitionOnCellC(curCellIdx_0).X = (float)stream.ReceiveNext();
                            //        if ((bool)stream.ReceiveNext()) _e.UnitPossitionOnCellC(curCellIdx_0).Y = (float)stream.ReceiveNext();

                            //    }
                            //}
                        }
                    }
                    break;

                case SyncTypes.CloudShift:
                    {
                        if (stream.IsWriting)
                        {
                            for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
                            {
                                if (_cellCs[curCellIdx_0].IsBorder) continue;

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
                                    if (_cellCs[curCellIdx_0].IsBorder) continue;

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