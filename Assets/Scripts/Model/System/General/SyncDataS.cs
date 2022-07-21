using Chessy.Model.Entity;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Chessy.Model.System.OnPhotonSerializeViewS;

namespace Chessy.Model.System
{
    sealed class SyncDataS : SystemModelAbstract
    {
        //readonly EntitiesModel _eCopy;
        //readonly bool[] _needSync = new bool[(byte)SyncTypes.End];

        internal SyncDataS(in SystemsModel sM, in EntitiesModel eM) : base(sM, eM)
        {
            //_eCopy = new EntitiesModel(eM.DataFromViewC, eM.RpcC.PunRPCName, new List<object>() { eM.RpcC.Action0, eM.RpcC.Action1 }, eM.TestModeT);
        }

        internal void TrySyncDataM()
        {
            //for (byte curCellIdx_0 = 0; curCellIdx_0 < IndexCellsValues.CELLS; curCellIdx_0++)
            //{
            //    //if (_e.IsBorder(curCellIdx_0)) continue;

            //    var curUnitT = _e.UnitT(curCellIdx_0);
            //    if (_eCopy.UnitT(curCellIdx_0) != curUnitT)
            //    {
            //        _eCopy.UnitMainC(curCellIdx_0).UnitType = curUnitT;

            //        _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.Others, new object[] { nameof(SyncData), SyncTypes.Cell, curCellIdx_0, CellSyncTypes.UnitType,  curUnitT });
            //    }


            //    var whereViewDataUnit = _unitWhereViewDataCs[curCellIdx_0);


            //    var curIdxViewUnit = whereViewDataUnit.ViewIdxCell;
            //    if (_eCopy.WhereViewDataUnitC(curCellIdx_0).ViewIdxCell != curIdxViewUnit)
            //    {
            //        _eCopy.WhereViewDataUnitC(curCellIdx_0).ViewIdxCell = curIdxViewUnit;

            //        _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.Others, new object[] { nameof(SyncData), SyncTypes.Cell, curCellIdx_0, CellSyncTypes.UnitIdxView, curIdxViewUnit });
            //    }

            //    var curIdxDataUnit = whereViewDataUnit.DataIdxCell;
            //    if (_eCopy.WhereViewDataUnitC(curCellIdx_0).DataIdxCell != curIdxDataUnit)
            //    {
            //        _eCopy.WhereViewDataUnitC(curCellIdx_0).DataIdxCell = curIdxDataUnit;

            //        _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.Others, new object[] { nameof(SyncData), SyncTypes.Cell, curCellIdx_0, CellSyncTypes.UnitIdxData, curIdxDataUnit });
            //    }

            //    var curPosUnit = _e.UnitPossitionOnCell(curCellIdx_0);
            //    if (_e.UnitPossitionOnCell(curCellIdx_0) != curPosUnit)
            //    {
            //        _e.UnitPossitionOnCellC(curCellIdx_0).Position = curPosUnit;

            //        _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.Others, new object[] { nameof(SyncData), SyncTypes.Cell, curCellIdx_0, CellSyncTypes.UnitPossition, curPosUnit });
            //    }
            //}


            //var objects = new object[objs.Count];
            //for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];

            //_rpcC.Action0(_rpcC.PunRPCName, RpcTarget.Others, objects);




            //var needUpdateElse = false;

            //if (_eCopy.IsStartedGame != _e.IsStartedGame)
            //{
            //    _eCopy.IsStartedGame = _e.IsStartedGame;
            //    needUpdateElse = true;
            //}
            //if (_eCopy.WinnerPlayerT != _e.WinnerPlayerT)
            //{
            //    _eCopy.WinnerPlayerT = _e.WinnerPlayerT;
            //    needUpdateElse = true;
            //}
            //if (_eCopy.DirectWindT != _e.DirectWindT)
            //{
            //    _eCopy.DirectWindT = _e.DirectWindT;
            //    needUpdateElse = true;
            //}
            //if (_eCopy.SpeedWind != _e.SpeedWind)
            //{
            //    _eCopy.SpeedWind = _e.SpeedWind;
            //    needUpdateElse = true;
            //}
            //if (_eCopy.SunSideT != _e.SunSideT)
            //{
            //    _eCopy.SunSideT = _e.SunSideT;
            //    needUpdateElse = true;
            //}
            //for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            //{
            //    if (_eCopy.PlayerInfoC(playerT).IsReadyForStartOnlineGame != _e.PlayerInfoC(playerT).IsReadyForStartOnlineGame)
            //    {
            //        _eCopy.PlayerInfoC(playerT).IsReadyForStartOnlineGame = _e.PlayerInfoC(playerT).IsReadyForStartOnlineGame;
            //        needUpdateElse = true;
            //    }


            //    //objs.Add(_e.PlayerInfoC(playerT).WoodForBuyHouse);
            //    //objs.Add(_e.BuildingsInTownInfoC(playerT).HaveBuildingsClone);

            //    //objs.Add(_e.PlayerInfoC(playerT).HaveKingInInventor);

            //    //objs.Add(_e.PawnPeopleInfoC(playerT).PeopleInCity);
            //    //objs.Add(_e.PawnPeopleInfoC(playerT).AmountInGame);

            //    //objs.Add(_e.GodInfoC(playerT).HaveGodInInventor);
            //    //objs.Add(_e.GodInfoC(playerT).UnitT);
            //    //objs.Add(_e.GodInfoC(playerT).Cooldown);

            //    //for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            //    //{
            //    //    for (var twT = (ToolsWeaponsWarriorTypes)1; twT < ToolsWeaponsWarriorTypes.End; twT++)
            //    //    {
            //    //        objs.Add(_e.ToolWeaponsInInventor(playerT, levelT, twT));
            //    //    }
            //    //}

            //    //for (var resT = (ResourceTypes)1; resT < ResourceTypes.End; resT++)
            //    //{
            //    //    objs.Add(_e.ResourcesInInventory(playerT, resT));
            //    //}
            //}


            ////if (needUpdateElse) SyncM(SyncTypes.Else);




            //for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
            //{
            //    //if (_eCopy.WaterUnit(cell_0) != _e.WaterUnit(cell_0))
            //    //{
            //    //    _eCopy.WaterUnitC(cell_0).Water = _e.WaterUnit(cell_0);

            //    //    _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.Others, new object[] { nameof(SyncData), SyncTypes.WaterUnit, cell_0, _e.WaterUnit(cell_0), });
            //    //}

            //    //if (_eCopy.WaterOnCellC(cell_0).Resources != _e.WaterOnCellC(cell_0).Resources)
            //    //{
            //    //    _eCopy.WaterOnCellC(cell_0).Resources = _e.WaterOnCellC(cell_0).Resources;

            //    //    SyncM(SyncTypes.WaterOnCell, cell_0);
            //    //}


            //    #region Environment

            //    //var needUpdateEnv = false;

            //    //if (_eCopy.YoungForestC(cell_0).Resources != _e.YoungForestC(cell_0).Resources)
            //    //{
            //    //    _eCopy.YoungForestC(cell_0).Resources = _e.YoungForestC(cell_0).Resources;
            //    //    needUpdateEnv = true;
            //    //}
            //    //if (_eCopy.AdultForestC(cell_0).Resources != _e.AdultForestC(cell_0).Resources)
            //    //{
            //    //    _eCopy.AdultForestC(cell_0).Resources = _e.AdultForestC(cell_0).Resources;
            //    //    needUpdateEnv = true;
            //    //}
            //    //if (_eCopy.HillC(cell_0).Resources != _e.HillC(cell_0).Resources)
            //    //{
            //    //    _eCopy.HillC(cell_0).Resources = _e.HillC(cell_0).Resources;
            //    //    needUpdateEnv = true;
            //    //}
            //    //if (_eCopy.MountainC(cell_0).Resources != _e.MountainC(cell_0).Resources)
            //    //{
            //    //    _eCopy.MountainC(cell_0).Resources = _e.MountainC(cell_0).Resources;
            //    //    needUpdateEnv = true;
            //    //}
            //    //if(needUpdateEnv) SyncM(SyncTypes.Environment, cell_0);

            //    #endregion


            //    #region MainToolWeapon

            //    var needUpdateMainToolWeapon = false;

            //    if (_eCopy.MainToolWeaponT(cell_0) != _mainTWC[cell_0))
            //    {
            //        _eCopy.MainToolWeaponC(cell_0).ToolWeaponT = _mainTWC[cell_0);
            //        needUpdateMainToolWeapon = true;
            //    }
            //    if (_eCopy.MainTWLevelT(cell_0) != _mainTWC[cell_0))
            //    {
            //        _eCopy.MainToolWeaponC(cell_0).LevelT = _mainTWC[cell_0);
            //        needUpdateMainToolWeapon = true;
            //    }

            //    if (needUpdateMainToolWeapon)
            //    {
            //        var objs = new List<object>
            //        {                        
            //            nameof(SyncData),
            //            SyncTypes.MainToolWeapon,
            //            cell_0,
            //            _mainTWC[cell_0),
            //            _mainTWC[cell_0),
            //        };

            //        var objects = new object[objs.Count];
            //        for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];

            //        _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.Others, objects);
            //    }


            //    #endregion






            //    var needUpdateCellElse = false;

            //    if (_eCopy.UnitMainC(cell_0).UnitT != _unitCs[cell_0).UnitT)
            //    {
            //        _eCopy.UnitMainC(cell_0).UnitT = _unitCs[cell_0).UnitT;
            //        needUpdateCellElse = true;
            //    }
            //    if (_eCopy.UnitMainC(cell_0).LevelT != _unitCs[cell_0).LevelT)
            //    {
            //        _eCopy.UnitMainC(cell_0).LevelT = _unitCs[cell_0).LevelT;
            //        needUpdateCellElse = true;
            //    }
            //    if (_eCopy.UnitMainC(cell_0).PlayerT != _unitCs[cell_0).PlayerT)
            //    {
            //        _eCopy.UnitMainC(cell_0).PlayerT = _unitCs[cell_0).PlayerT;
            //        needUpdateCellElse = true;
            //    }
            //    if (_eCopy.UnitMainC(cell_0).ConditionT != _unitCs[cell_0).ConditionT)
            //    {
            //        _eCopy.UnitMainC(cell_0).ConditionT = _unitCs[cell_0).ConditionT;
            //        needUpdateCellElse = true;
            //    }
            //    if (_eCopy.UnitMainC(cell_0).IsArcherDirectedToRight != _unitCs[cell_0).IsArcherDirectedToRight)
            //    {
            //        _eCopy.UnitMainC(cell_0).IsArcherDirectedToRight = _unitCs[cell_0).IsArcherDirectedToRight;
            //        needUpdateCellElse = true;
            //    }
            //    if (_eCopy.ShiftingInfoForUnitC(cell_0).WhereIdxCell != _e.ShiftingInfoForUnitC(cell_0).WhereIdxCell)
            //    {
            //        _eCopy.ShiftingInfoForUnitC(cell_0).WhereIdxCell = _e.ShiftingInfoForUnitC(cell_0).WhereIdxCell;
            //        needUpdateCellElse = true;
            //    }
            //    if (_eCopy.HpUnit(cell_0) != _e.HpUnit(cell_0))
            //    {
            //        _eCopy.HpUnitC(cell_0).Health = _e.HpUnit(cell_0);
            //        needUpdateCellElse = true;
            //    }
            //    if (_eCopy.DamageSimpleAttack(cell_0) != _e.DamageSimpleAttack(cell_0))
            //    {
            //        _eCopy.UnitMainC(cell_0).DamageSimpleAttack = _e.DamageSimpleAttack(cell_0);
            //        needUpdateCellElse = true;
            //    }
            //    if (_eCopy.DamageOnCell(cell_0) != _e.DamageOnCell(cell_0))
            //    {
            //        _eCopy.UnitMainC(cell_0).DamageOnCell = _e.DamageOnCell(cell_0);
            //        needUpdateCellElse = true;
            //    }

            //    //if (_eCopy.ExtraToolWeaponT(cell_0) != _extraTWC[cell_0))
            //    //{
            //    //    _eCopy.SetExtraToolWeaponT(cell_0, _extraTWC[cell_0));
            //    //    needUpdate = true;
            //    //}
            //    //if (_eCopy.ExtraTWLevelT(cell_0) != _extraTWC[cell_0))
            //    //{
            //    //    _eCopy.SetExtraTWLevelT(cell_0, _extraTWC[cell_0));
            //    //    needUpdate = true;
            //    //}
            //    //if (_eCopy.ExtraTWProtection(cell_0) != _e.ExtraTWProtection(cell_0))
            //    //{
            //    //    _eCopy.UnitExtraTWC(cell_0).ProtectionShield =  _e.ExtraTWProtection(cell_0);
            //    //    needUpdate = true;
            //    //}

            //    //if (_eCopy.HowManyWarriourCanExtractAdultForest(cell_0) != _e.HowManyWarriourCanExtractAdultForest(cell_0))
            //    //{
            //    //    _eCopy.ExtactionResourcesWithWarriorC(cell_0).HowManyWarriourCanExtractAdultForest = _e.HowManyWarriourCanExtractAdultForest(cell_0);
            //    //    needUpdate = true;
            //    //}
            //    //if (_eCopy.PawnExtractHill(cell_0) != _e.PawnExtractHill(cell_0))
            //    //{
            //    //    _eCopy.ExtactionResourcesWithWarriorC(cell_0).HowManyWarriourCanExtractHill = _e.PawnExtractHill(cell_0);
            //    //    needUpdate = true;
            //    //}

            //    //if (_eCopy.StunUnit(cell_0) != _e.StunUnit(cell_0))
            //    //{
            //    //    _eCopy.UnitEffectsC(cell_0).StunHowManyUpdatesNeedStay = _e.StunUnit(cell_0);
            //    //    needUpdate = true;
            //    //}
            //    //if (_eCopy.ProtectionRainyMagicShield(cell_0) != _e.ProtectionRainyMagicShield(cell_0))
            //    //{
            //    //    _eCopy.UnitEffectsC(cell_0).ProtectionRainyMagicShield = _e.ProtectionRainyMagicShield(cell_0);
            //    //    needUpdate = true;
            //    //}
            //    //if (_eCopy.HaveFrozenArrawArcher(cell_0) != _e.HaveFrozenArrawArcher(cell_0))
            //    //{
            //    //    _eCopy.UnitEffectsC(cell_0).HaveFrozenArrawArcher = _e.HaveFrozenArrawArcher(cell_0);
            //    //    needUpdate = true;
            //    //}


            //    //#region Building

            //    //if (_eCopy.BuildingOnCellT(cell_0) != _buildingCs[cell_0))
            //    //{
            //    //    _eCopy.BuildingC(cell_0).BuildingT = _buildingCs[cell_0);
            //    //    needUpdate = true;
            //    //}
            //    //if (_eCopy.BuildingLevelT(cell_0) != _e.BuildingLevelT(cell_0))
            //    //{
            //    //    _eCopy.BuildingC(cell_0).LevelT = _e.BuildingLevelT(cell_0);
            //    //    needUpdate = true;
            //    //}
            //    //if (_eCopy.BuildingPlayerT(cell_0) != _e.BuildingPlayerT(cell_0))
            //    //{
            //    //    _eCopy.BuildingC(cell_0).PlayerT = _e.BuildingPlayerT(cell_0);
            //    //    needUpdate = true;
            //    //}
            //    //if (_eCopy.BuildingPlayerT(cell_0) != _e.BuildingPlayerT(cell_0))
            //    //{
            //    //    _eCopy.BuildingC(cell_0).PlayerT = _e.BuildingPlayerT(cell_0);
            //    //    needUpdate = true;
            //    //}

            //    //#endregion



            //    //if (_eCopy.HaveFire(cell_0) != _e.EffectE(cell_0).HaveFire)
            //    //{
            //    //    _eCopy.EffectE(cell_0).HaveFire = _fireCs[cell_0);
            //    //    needUpdate = true;
            //    //}


            //    for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
            //    {
            //        if (_eCopy.UnitVisibleC(cell_0).IsVisible(playerT) != _unitVisibleCs[cell_0).IsVisible(playerT))
            //        {
            //            _eCopy.UnitVisibleC(cell_0).Set(playerT, _unitVisibleCs[cell_0).IsVisible(playerT));
            //            needUpdateCellElse = true;
            //        }
            //    }

            //    //objs.Add(_e.UnitButtonAbilitiesC(cell_0).AbilityTypesClone);
            //    //objs.Add(_cooldownAbilityCs[cell_0).CooldonwsFloat);


            //    //#region Building

            //    //objs.Add(_visibleBuildingCs[cell_0).IsVisibleClone);

            //    //#endregion

            //    //objs.Add(_riverCs[cell_0));
            //    //objs.Add(_haveRiverAroundCellCs[cell_0).HaveRives);

            //    //objs.Add(_e.TrailVisibleC(cell_0).IsVisibleClone);
            //    //objs.Add(_hpTrailCs[cell_0).Healths);


            //    if (needUpdateCellElse)
            //    {
            //        var objs = new List<object>
            //        {
            //            nameof(SyncData),
            //            SyncTypes.Cell,
            //            cell_0,
            //            _e.UnitT(cell_0),
            //            _e.UnitLevelT(cell_0),
            //            _unitCs[cell_0),
            //            _unitCs[cell_0),
            //            _e.IsRightArcherUnit(cell_0),
            //            _e.ShiftingInfoForUnitC(cell_0).WhereIdxCell,
            //            _e.HpUnit(cell_0),
            //            _e.WaterUnit(cell_0),
            //            _e.DamageSimpleAttack(cell_0),
            //            _e.DamageOnCell(cell_0),

            //            //_extraTWC[cell_0),
            //            //_extraTWC[cell_0),
            //            //_e.ExtraTWProtection(cell_0),

            //            //_e.HowManyWarriourCanExtractAdultForest(cell_0),
            //            //_e.PawnExtractHill(cell_0),

            //            //_e.StunUnit(cell_0),
            //            //_e.ProtectionRainyMagicShield(cell_0),
            //            //_e.HaveFrozenArrawArcher(cell_0),

            //            //_fireCs[cell_0),
            //        };

            //        for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
            //        {
            //            objs.Add(_unitVisibleCs[cell_0).IsVisible(playerT));
            //        }


            //        var objects = new object[objs.Count];
            //        for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];

            //        _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.Others, objects);
            //    }
            //}
        }

        internal void SyncM(in SyncTypes syncT, in byte cellIdx = 0)
        {
            //switch (syncT)
            //{
            //    case SyncTypes.Cell:
            //        break;

            //    case SyncTypes.Environment:
            //        {
            //            var objs = new List<object>
            //            {
            //                nameof(SyncData),
            //                SyncTypes.Environment,
            //                cellIdx,
            //                _e.YoungForestC(cellIdx).Resources,
            //                _e.AdultForestC(cellIdx).Resources,
            //                _e.HillC(cellIdx).Resources,
            //                _e.MountainC(cellIdx).Resources,
            //            };

            //            var objects = new object[objs.Count];
            //            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];

            //            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.Others, objects);
            //        }
            //        break;

            //    case SyncTypes.WaterOnCell:
            //        {
            //            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.Others, new object[] { nameof(SyncData), SyncTypes.WaterOnCell, cellIdx, _eCopy.WaterOnCellC(cellIdx).Resources, });
            //        }
            //        break;

            //    case SyncTypes.MainToolWeapon:
            //        break;

            //    case SyncTypes.WaterUnit:
            //        break;

            //    case SyncTypes.Else:
            //        {
            //            var objs = new List<object>
            //            {
            //                nameof(SyncData),
            //                SyncTypes.Else,
            //                _e.IsStartedGame,
            //                _eCopy.WinnerPlayerT,
            //                _eCopy.DirectWindT,
            //                _eCopy.SpeedWind,
            //                _eCopy.SunSideT,
            //            };
            //            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            //            {
            //                objs.Add(_e.PlayerInfoC(playerT).IsReadyForStartOnlineGame);
            //            }

            //            var objects = new object[objs.Count];
            //            for (int i = 0; i < objects.Length; i++) objects[i] = objs[i];

            //            _rpcC.Action0(_rpcC.PunRPCName, RpcTarget.Others, objects);
            //        }
            //        break;

            //    default: throw new Exception();
            //}
        }


    //    internal void SyncData(in object[] objects)
    //    {
    //        if (PhotonNetwork.IsMasterClient) return;


    //        var idxCurrent = 1;

    //        var syncT = (SyncTypes)objects[idxCurrent++];

    //        switch (syncT)
    //        {
    //            case SyncTypes.Cell:
    //                {
    //                    var curCellIdx_0 = (byte)objects[idxCurrent++];
    //                    var cellSyncT = (CellSyncTypes)objects[idxCurrent++];

    //                    switch (cellSyncT)
    //                    {
    //                        case CellSyncTypes.UnitType:
    //                            _unitCs[curCellIdx_0).UnitType = (UnitTypes)objects[idxCurrent++];
    //                            break;

    //                        case CellSyncTypes.UnitIdxView:
    //                            _unitWhereViewDataCs[curCellIdx_0).ViewIdxCell = (byte)objects[idxCurrent++];
    //                            break;

    //                        case CellSyncTypes.UnitIdxData:
    //                            _unitWhereViewDataCs[curCellIdx_0).DataIdxCell = (byte)objects[idxCurrent++];
    //                            break;

    //                        case CellSyncTypes.UnitPossition:
    //                            _e.UnitPossitionOnCellC(curCellIdx_0).Position = (Vector3)objects[idxCurrent++];
    //                            break;

    //                        default: throw new Exception();
    //                    }
    //                }
    //                break;

    //            default: throw new Exception();
    //        }


    //        _updateAllViewC.NeedUpdateView = true;

    //        //switch (syncT)
    //        //{
    //        //    case SyncTypes.Cell:
    //        //        var idxCell = (byte)objects[idxCurrent++];
    //        //        _unitCs[idxCell).UnitT = (UnitTypes)objects[idxCurrent++];
    //        //        _unitCs[idxCell).LevelT = (LevelTypes)objects[idxCurrent++];
    //        //        _unitCs[idxCell).PlayerT = (PlayerTypes)objects[idxCurrent++];
    //        //        _unitCs[idxCell).ConditionT = (ConditionUnitTypes)objects[idxCurrent++];
    //        //        _unitCs[idxCell).IsArcherDirectedToRight = (bool)objects[idxCurrent++];
    //        //        _e.ShiftingInfoForUnitC(idxCell).WhereIdxCell = (byte)objects[idxCurrent++];
    //        //        _hpUnitCs[idxCell).Health = (double)objects[idxCurrent++];
    //        //        _unitWaterCs[idxCell).Water = (double)objects[idxCurrent++];
    //        //        _unitCs[idxCell).DamageSimpleAttack = (double)objects[idxCurrent++];
    //        //        _unitCs[idxCell).DamageOnCell = (double)objects[idxCurrent++];

    //        //        //_e.UnitExtraTWC(idxCell).ToolWeaponT = (ToolsWeaponsWarriorTypes)objects[idxCurrent++];
    //        //        //_e.UnitExtraTWC(idxCell).LevelT = (LevelTypes)objects[idxCurrent++];
    //        //        //_e.UnitExtraTWC(idxCell).ProtectionShield = (float)objects[idxCurrent++];

    //        //        //_e.ExtactionResourcesWithWarriorC(idxCell).HowManyWarriourCanExtractAdultForest = (float)objects[idxCurrent++];
    //        //        //_e.ExtactionResourcesWithWarriorC(idxCell).HowManyWarriourCanExtractHill = (float)objects[idxCurrent++];

    //        //        //_effectsUnitCs[idxCell).StunHowManyUpdatesNeedStay = (float)objects[idxCurrent++];
    //        //        //_effectsUnitCs[idxCell).ProtectionRainyMagicShield = (float)objects[idxCurrent++];
    //        //        //_effectsUnitCs[idxCell).HaveFrozenArrawArcher = (bool)objects[idxCurrent++];



    //        //        //_e.EffectE(idxCell).HaveFire = (bool)objects[idxCurrent++];

    //        //        for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
    //        //        {
    //        //            _unitVisibleCs[idxCell).Set(playerT, (bool)objects[idxCurrent++]);
    //        //        }
    //        //        break;

    //        //    case SyncTypes.Environment:
    //        //        {
    //        //            idxCell = (byte)objects[idxCurrent++];
    //        //            _e.YoungForestC(idxCell).Resources = (float)objects[idxCurrent++];
    //        //            _e.AdultForestC(idxCell).Resources = (float)objects[idxCurrent++];
    //        //            _e.HillC(idxCell).Resources = (float)objects[idxCurrent++];
    //        //            _e.MountainC(idxCell).Resources = (float)objects[idxCurrent++];
    //        //        }
    //        //        break;


    //        //    case SyncTypes.MainToolWeapon:
    //        //        {
    //        //            idxCell = (byte)objects[idxCurrent++];
    //        //            _e.MainToolWeaponC(idxCell).ToolWeaponT = (ToolsWeaponsWarriorTypes)objects[idxCurrent++];
    //        //            _e.MainToolWeaponC(idxCell).LevelT = (LevelTypes)objects[idxCurrent++];
    //        //        }
    //        //        break;


    //        //    case SyncTypes.WaterOnCell:
    //        //        {
    //        //            idxCell = (byte)objects[idxCurrent++];
    //        //            _e.WaterOnCellC(idxCell).Resources = (float)objects[idxCurrent++];
    //        //        }
    //        //        break;

    //        //    case SyncTypes.WaterUnit:
    //        //        {
    //        //            idxCell = (byte)objects[idxCurrent++];
    //        //            _unitWaterCs[idxCell).Water = (double)objects[idxCurrent++];
    //        //        }
    //        //        break;

    //        //    case SyncTypes.Else:
    //        //        {
    //        //            _e.IsStartedGame = (bool)objects[idxCurrent++];
    //        //            _e.WinnerPlayerT = (PlayerTypes)objects[idxCurrent++];
    //        //            _e.DirectWindT = (DirectTypes)objects[idxCurrent++];
    //        //            _e.SpeedWind = (byte)objects[idxCurrent++];
    //        //            _e.SunSideT = (SunSideTypes)objects[idxCurrent++];

    //        //            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
    //        //            {
    //        //                _e.PlayerInfoC(playerT).IsReadyForStartOnlineGame = (bool)objects[idxCurrent++];
    //        //            }

    //        //                //for (byte cell_0 = 0; cell_0 < IndexCellsValues.CELLS; cell_0++)
    //        //                //{
    //        //                //_e.SetUnitOnCellT(cell_0, (UnitTypes)objects[idxCurrent++]);
    //        //                //_e.SetUnitLevelT(cell_0, (LevelTypes)objects[idxCurrent++]);
    //        //                //_e.SetUnitPlayerT(cell_0, (PlayerTypes)objects[idxCurrent++]);
    //        //                //_e.SetUnitConditionT(cell_0, (ConditionUnitTypes)objects[idxCurrent++]);
    //        //                //_unitCs[cell_0).IsArcherDirectedToRight = (bool)objects[idxCurrent++];
    //        //                //for (var playerT = (PlayerTypes)0; playerT < PlayerTypes.End; playerT++)
    //        //                //    _unitVisibleCs[cell_0).Set(playerT, (bool)objects[idxCurrent++]);


    //        //                //_hpUnitCs[cell_0).Health = (double)objects[idxCurrent++];
    //        //                //_e.EnergyUnitC(cell_0).Energy = (double)objects[idxCurrent++];
    //        //                //_unitWaterCs[cell_0).Water = (double)objects[idxCurrent++];

    //        //                //_unitCs[cell_0).DamageSimpleAttack = (double)objects[idxCurrent++];
    //        //                //_unitCs[cell_0).DamageOnCell = (double)objects[idxCurrent++];

    //        //                //_e.SetMainToolWeaponT(cell_0, (ToolsWeaponsWarriorTypes)objects[idxCurrent++]);
    //        //                //_e.SetMainTWLevelT(cell_0, (LevelTypes)objects[idxCurrent++]);

    //        //                //_e.SetExtraToolWeaponT(cell_0, (ToolsWeaponsWarriorTypes)objects[idxCurrent++]);
    //        //                //_e.SetExtraTWLevelT(cell_0, (LevelTypes)objects[idxCurrent++]);
    //        //                //_e.UnitExtraTWE(cell_0).ProtectionShield = (float)objects[idxCurrent++];

    //        //                //_e.ExtactionResourcesWithWarriorC(cell_0).HowManyWarriourCanExtractAdultForest = (float)objects[idxCurrent++];
    //        //                //_e.ExtactionResourcesWithWarriorC(cell_0).HowManyWarriourCanExtractHill = (float)objects[idxCurrent++];

    //        //                //_e.SetLastDiedUnitT(cell_0, (UnitTypes)objects[idxCurrent++]);
    //        //                //_e.SetLastDiedLevelT(cell_0, (LevelTypes)objects[idxCurrent++]);
    //        //                //_e.SetLastDiedPlayerT(cell_0, (PlayerTypes)objects[idxCurrent++]);

    //        //                //_e.UnitButtonAbilitiesC(cell_0).Sync((byte[])objects[idxCurrent++]);
    //        //                //_cooldownAbilityCs[cell_0).Sync((float[])objects[idxCurrent++]);

    //        //                //_effectsUnitCs[cell_0).StunHowManyUpdatesNeedStay = (float)objects[idxCurrent++];
    //        //                //_effectsUnitCs[cell_0).ProtectionRainyMagicShield = (float)objects[idxCurrent++];
    //        //                //_effectsUnitCs[cell_0).HaveFrozenArrawArcher = (bool)objects[idxCurrent++];


    //        //                //#region Building

    //        //                //_e.SetBuildingOnCellT(cell_0, (BuildingTypes)objects[idxCurrent++]);
    //        //                //_e.SetBuildingLevelT(cell_0, (LevelTypes)objects[idxCurrent++]);
    //        //                //_e.SetBuildingPlayerT(cell_0, (PlayerTypes)objects[idxCurrent++]);
    //        //                //_e.BuildingHpC(cell_0).Health = (double)objects[idxCurrent++];
    //        //                //_visibleBuildingCs[cell_0).Sync((bool[])objects[idxCurrent++]);

    //        //                //#endregion


    //        //                //_e.YoungForestC(cell_0).Resources = (float)objects[idxCurrent++];
    //        //                //_e.AdultForestC(cell_0).Resources = (float)objects[idxCurrent++];
    //        //                //_e.MountainC(cell_0).Resources = (float)objects[idxCurrent++];
    //        //                //_e.HillC(cell_0).Resources = (float)objects[idxCurrent++];
    //        //                //_e.WaterOnCellC(cell_0).Resources = (float)objects[idxCurrent++];

    //        //                //_e.SetRiverT(cell_0, (RiverTypes)objects[idxCurrent++]);
    //        //                //_haveRiverAroundCellCs[cell_0).Sync((bool[])objects[idxCurrent++]);

    //        //                //_fireCs[cell_0) = (bool)objects[idxCurrent++];

    //        //                //_e.TrailVisibleC(cell_0).Sync((bool[])objects[idxCurrent++]);
    //        //                //_hpTrailCs[cell_0).Sync((float[])objects[idxCurrent++]);
    //        //                //}

    //        //                //_e.IsStartedGame = (bool)objects[idxCurrent++];
    //        //                //_e.Motions = (int)objects[idxCurrent++];
    //        //                //_e.WhereTeleportC.StartIdxCell = (byte)objects[idxCurrent++];
    //        //                //_e.WhereTeleportC.EndIdxCell = (byte)objects[idxCurrent++];
    //        //                //_e.WinnerPlayerT = (PlayerTypes)objects[idxCurrent++];

    //        //                //_e.DirectWindT = (DirectTypes)objects[idxCurrent++];
    //        //                //_e.SpeedWind = (byte)objects[idxCurrent++];
    //        //                //_e.CenterCloudCellIdx = (byte)objects[idxCurrent++];
    //        //                //_e.SunSideT = (SunSideTypes)objects[idxCurrent++];

    //        //                //for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
    //        //                //{
    //        //                //    _e.PlayerInfoE(playerT).PlayerInfoC.IsReadyForStartOnlineGame = (bool)objects[idxCurrent++];
    //        //                //_e.PlayerInfoE(playerT).PlayerInfoC.WoodForBuyHouse = (float)objects[idxCurrent++];
    //        //                //_e.PlayerInfoE(playerT).BuildingsInTownInfoC.Sync((bool[])objects[idxCurrent++]);

    //        //                //_e.PlayerInfoC(playerT).HaveKingInInventor = (bool)objects[idxCurrent++];

    //        //                //_e.PlayerInfoE(playerT).PawnInfoC.PeopleInCity = (int)objects[idxCurrent++];
    //        //                //_e.PlayerInfoE(playerT).PawnInfoC.AmountInGame = (int)objects[idxCurrent++];

    //        //                //_e.PlayerInfoE(playerT).GodInfoC.HaveGodInInventor = (bool)objects[idxCurrent++];
    //        //                //_e.PlayerInfoE(playerT).GodInfoC.UnitT = (UnitTypes)objects[idxCurrent++];
    //        //                //_e.PlayerInfoE(playerT).GodInfoC.Cooldown = (float)objects[idxCurrent++];

    //        //                //for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
    //        //                //{
    //        //                //    for (var twT = (ToolsWeaponsWarriorTypes)1; twT < ToolsWeaponsWarriorTypes.End; twT++)
    //        //                //    {
    //        //                //        _e.SetToolWeaponsInInventor(playerT, levelT, twT, (int)objects[idxCurrent++]);
    //        //                //    }
    //        //                //}

    //        //                //for (var resT = (ResourceTypes)1; resT < ResourceTypes.End; resT++)
    //        //                //{
    //        //                //    _e.SetResourcesInInventory(playerT, resT, (float)objects[idxCurrent++]);
    //        //                //}
    //        //                //}
    //        //            }
    //        //        break;

    //        //    default: throw new Exception();
    //        //}

    //        ////_updateAllViewC.NeedUpdateView = true;
    //    }
    }
}