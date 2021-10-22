using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class EventsGameSys : IEcsInitSystem
    {
        private EcsFilter<CellUnitDataCom> _cellUnitFilter = default;

        private EcsFilter<ReadyDataUICom, ReadyViewUICom> _readyFilter = default;
        private EcsFilter<SelectorCom> _selectorFilter = default;
        private EcsFilter<LeaveViewUIComponent> _leaveUIFilter = default;
        private EcsFilter<GetterUnitsDataUICom, GetterUnitsViewUICom> _takerUIFilter = default;
        private EcsFilter<DonerUICom> _donerUIFilter = default;
        private EcsFilter<EnvirZoneDataUICom, EnvirZoneViewUICom> _envirZoneUIFilter = default;
        private EcsFilter<CondUnitUICom> _condUnitZoneUIFilt = default;
        private EcsFilter<BuildLeftZoneViewUICom> _buildLeftZoneViewUICom = default;
        private EcsFilter<KingZoneViewUIComp> _kingZoneUIFilter = default;
        private EcsFilter<GiveTakeViewUICom> _giveTakeZoneUIFilter = default;
        private EcsFilter<SoundEffectsComp> _soundEffFilt = default;
        private EcsFilter<FriendZoneDataUICom, FriendZoneViewUICom> _friendZoneFilt = default;
        private EcsFilter<HintDataUICom, HintViewUICom> _hintUIFilt = default;

        private EcsFilter<InventorUnitsComponent> _invUnitsFilt = default;

        private byte IdxSelectedCell => _selectorFilter.Get1(0).IdxSelCell;

        public void Init()
        {
            _readyFilter.Get2(0).AddListenerToReadyButton(Ready);

            _kingZoneUIFilter.Get1(0).AddListenerToSetKing_Button(delegate { GetUnit(UnitTypes.King); });
            _takerUIFilter.Get2(0).AddListener(UnitTypes.Pawn, delegate { GetUnit(UnitTypes.Pawn); });
            _takerUIFilter.Get2(0).AddListener(UnitTypes.Rook, delegate { GetUnit(UnitTypes.Rook); });
            _takerUIFilter.Get2(0).AddListener(UnitTypes.Bishop, delegate { GetUnit(UnitTypes.Bishop); });

            _takerUIFilter.Get2(0).AddListenerToCreateUnit(UnitTypes.Pawn, delegate { CreateUnit(UnitTypes.Pawn); });
            _takerUIFilter.Get2(0).AddListenerToCreateUnit(UnitTypes.Rook, delegate { CreateUnit(UnitTypes.Rook); });
            _takerUIFilter.Get2(0).AddListenerToCreateUnit(UnitTypes.Bishop, delegate { CreateUnit(UnitTypes.Bishop); });

            _donerUIFilter.Get1(0).AddListener(Done);

            _envirZoneUIFilter.Get2(0).AddListenerToEnvInfo(EnvironmentInfo);

            _leaveUIFilter.Get1(0).AddListener(delegate { PhotonNetwork.LeaveRoom(); });

            _condUnitZoneUIFilt.Get1(0).AddListener(CondUnitTypes.Protected, delegate { ConditionAbilityButton(CondUnitTypes.Protected); });
            _condUnitZoneUIFilt.Get1(0).AddListener(CondUnitTypes.Relaxed, delegate { ConditionAbilityButton(CondUnitTypes.Relaxed); });


            _giveTakeZoneUIFilter.Get1(0).AddListUpgradeButton(ToggleUpgradeUnit);
            _giveTakeZoneUIFilter.Get1(0).AddList_Button(ToolWeaponTypes.Pick, delegate { ToggleToolWeapon(ToolWeaponTypes.Pick); });
            _giveTakeZoneUIFilter.Get1(0).AddList_Button(ToolWeaponTypes.Sword, delegate { ToggleToolWeapon(ToolWeaponTypes.Sword); });
            _giveTakeZoneUIFilter.Get1(0).AddList_Button(ToolWeaponTypes.Shield, delegate { ToggleToolWeapon(ToolWeaponTypes.Shield); });


            _friendZoneFilt.Get2(0).AddListenerReady(ReadyFriend);





            ref var buildLeftZoneViewUICom = ref _buildLeftZoneViewUICom.Get1(0);

            buildLeftZoneViewUICom.AddListenerToMelt(delegate { MeltOre(); });

            buildLeftZoneViewUICom.AddListBuildUpgrade(BuildingTypes.Farm, delegate { UpgradeBuilding(BuildingTypes.Farm); });
            buildLeftZoneViewUICom.AddListBuildUpgrade(BuildingTypes.Woodcutter, delegate { UpgradeBuilding(BuildingTypes.Woodcutter); });
            buildLeftZoneViewUICom.AddListBuildUpgrade(BuildingTypes.Mine, delegate { UpgradeBuilding(BuildingTypes.Mine); });





            _hintUIFilt.Get2(0).AddListHint_But(ExecuteHint);
        }



        private void Ready() => RpcSys.ReadyToMaster();

        private void GetUnit(UnitTypes unitType)
        {
            ref var selCom = ref _selectorFilter.Get1(0);
            ref var invUnitCom = ref _invUnitsFilt.Get1(0);
            ref var takerUnitDatCom = ref _takerUIFilter.Get1(0);

            selCom.DefCellClickType();
            selCom.IdxCurCell = default;
            selCom.IdxPreVisionCell = default;
            selCom.DefSelectedCell();
            takerUnitDatCom.ResetCurTimer(unitType);

            if (WhoseMoveCom.IsMyOnlineMove || GameModesCom.IsOfflineMode)
            {
                if (invUnitCom.HaveUnitInInv(WhoseMoveCom.CurPlayer, unitType, LevelUnitTypes.Iron))
                {
                    selCom.SelUnitType = unitType;
                    selCom.LevelSelUnitType = LevelUnitTypes.Iron;
                }
                else if (invUnitCom.HaveUnitInInv(WhoseMoveCom.CurPlayer, unitType, LevelUnitTypes.Wood))
                {
                    selCom.SelUnitType = unitType;
                    selCom.LevelSelUnitType = LevelUnitTypes.Wood;
                }
                else
                {
                    takerUnitDatCom.ActiveNeedCreateButton(unitType, true);
                }

            }
        }

        internal void ReadyFriend()
        {
            _friendZoneFilt.Get1(0).IsActiveFriendZone = false;
        }

        private void Done()
        {
            if (!_invUnitsFilt.Get1(0).HaveUnitInInv(WhoseMoveCom.CurPlayer, UnitTypes.King, LevelUnitTypes.Wood))
            {
                RpcSys.DoneToMaster();
            }
            else
            {
                _soundEffFilt.Get1(0).Play(SoundEffectTypes.Mistake);
            }

            _selectorFilter.Get1(0).DefCellClickType();
            _selectorFilter.Get1(0).DefSelectedUnit();
        }

        private void EnvironmentInfo()
        {
            _envirZoneUIFilter.Get1(0).IsActivatedInfo = !_envirZoneUIFilter.Get1(0).IsActivatedInfo;
        }

        private void ConditionAbilityButton(CondUnitTypes conditionUnitType)
        {
            if (WhoseMoveCom.IsMyOnlineMove || GameModesCom.IsOfflineMode)
            {
                if (_cellUnitFilter.Get1(IdxSelectedCell).Is(conditionUnitType))
                {
                    RpcSys.ConditionUnitToMaster(CondUnitTypes.None, IdxSelectedCell);
                }
                else
                {
                    RpcSys.ConditionUnitToMaster(conditionUnitType, IdxSelectedCell);
                }
            }
        }

        private void CreateUnit(UnitTypes unitType)
        {
            _takerUIFilter.Get1(0).ResetCurTimer(unitType);

            if (WhoseMoveCom.IsMyOnlineMove || GameModesCom.IsOfflineMode) RpcSys.CreateUnitToMaster(unitType);
        }

        private void MeltOre()
        {
            if (WhoseMoveCom.IsMyOnlineMove || GameModesCom.IsOfflineMode) RpcSys.MeltOreToMaster();
        }

        private void UpgradeBuilding(BuildingTypes buildingType)
        {
            if (WhoseMoveCom.IsMyOnlineMove || GameModesCom.IsOfflineMode) RpcSys.UpgradeBuildingToMaster(buildingType);
        }

        private void ToggleToolWeapon(ToolWeaponTypes toolAndWeaponType)
        {
            if (WhoseMoveCom.IsMyOnlineMove || GameModesCom.IsOfflineMode)
            {
                ref var selCom = ref _selectorFilter.Get1(0);

                selCom.CellClickType = CellClickTypes.GiveTakeTW;

                if (toolAndWeaponType == ToolWeaponTypes.Shield)
                {
                    if (selCom.TWTypeForGive == toolAndWeaponType)
                    {
                        if (selCom.LevelTWType == LevelTWTypes.Wood) selCom.LevelTWType = LevelTWTypes.Iron;
                        else selCom.LevelTWType = LevelTWTypes.Wood;
                    }
                    else
                    {
                        selCom.TWTypeForGive = toolAndWeaponType;
                        selCom.LevelTWType = LevelTWTypes.Wood;
                    }
                }
                else
                {
                    selCom.TWTypeForGive = toolAndWeaponType;
                    selCom.LevelTWType = LevelTWTypes.Iron;
                }
            }
        }

        private void ExecuteHint()
        {
            ref var hintDataUICom = ref _hintUIFilt.Get1(0);
            ref var hintViewUICom = ref _hintUIFilt.Get2(0);


            hintDataUICom.CurNumber++;

            if (hintDataUICom.CurNumber < System.Enum.GetNames(typeof(VideoClipTypes)).Length)
            {
                hintViewUICom.SetVideoClip((VideoClipTypes)hintDataUICom.CurNumber);
                hintViewUICom.SetPos(new Vector3(Random.Range(-500f, 500f), Random.Range(-300f, 300f)));
            }
            else
            {
                hintViewUICom.SetActiveHintZone(false);
            }
        }

        private void ToggleUpgradeUnit() => _selectorFilter.Get1(0).CellClickType = CellClickTypes.UpgradeUnit;
    }
}