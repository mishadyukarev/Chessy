using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.Else.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General.Center;
using Assets.Scripts.ECS.Component.View.UI.Game.General.Down;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.UI.Game.General.Center;
using Assets.Scripts.ECS.Components.View.UI.Game.General.Center;
using Assets.Scripts.ECS.Components.View.UI.Game.General.Right;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts
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
        private EcsFilter<GiveTakeZoneViewUIComp> _giveTakeZoneUIFilter = default;
        private EcsFilter<SoundEffectsComp> _soundEffFilt = default;
        private EcsFilter<FriendZoneDataUICom, FriendZoneViewUICom> _friendZoneFilt = default;

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



            _giveTakeZoneUIFilter.Get1(0).AddListenerToGive_Button(ActiveGiveTakeButton);

            _giveTakeZoneUIFilter.Get1(0).AddListener_Button(ToolWeaponTypes.Axe, delegate { ToggleToolWeapon(ToolWeaponTypes.Axe); });
            _giveTakeZoneUIFilter.Get1(0).AddListener_Button(ToolWeaponTypes.Pick, delegate { ToggleToolWeapon(ToolWeaponTypes.Pick); });
            _giveTakeZoneUIFilter.Get1(0).AddListener_Button(ToolWeaponTypes.Sword, delegate { ToggleToolWeapon(ToolWeaponTypes.Sword); });
            _giveTakeZoneUIFilter.Get1(0).AddListener_Button(ToolWeaponTypes.Crossbow, delegate { ToggleToolWeapon(ToolWeaponTypes.Crossbow); });


            _friendZoneFilt.Get2(0).AddListenerReady(ReadyFriend);





            ref var buildLeftZoneViewUIComp = ref _buildLeftZoneViewUICom.Get1(0);

            buildLeftZoneViewUIComp.AddListenerToMelt(delegate { MeltOre(); });

            buildLeftZoneViewUIComp.AddListenerToBuildUpgrade(BuildingTypes.Farm, delegate { UpgradeBuilding(BuildingTypes.Farm); });
            buildLeftZoneViewUIComp.AddListenerToBuildUpgrade(BuildingTypes.Woodcutter, delegate { UpgradeBuilding(BuildingTypes.Woodcutter); });
            buildLeftZoneViewUIComp.AddListenerToBuildUpgrade(BuildingTypes.Mine, delegate { UpgradeBuilding(BuildingTypes.Mine); });
        }



        private void Ready() => RpcSys.ReadyToMaster(!_readyFilter.Get1(0).IsReady(PhotonNetwork.IsMasterClient));

        private void GetUnit(UnitTypes unitType)
        {
            ref var selCom = ref _selectorFilter.Get1(0);
            ref var invUnitCom = ref _invUnitsFilt.Get1(0);
            ref var takerUnitDatCom = ref _takerUIFilter.Get1(0);

            selCom.DefCellClickType();
            selCom.IdxCurCell = default;
            selCom.IdxPreviousVisionCell = default;
            selCom.DefSelectedCell();
            takerUnitDatCom.ResetCurTimer(unitType);

            if (WhoseMoveCom.IsMyOnlineMove || GameModesCom.IsOfflineMode)
            {
                if (invUnitCom.HaveUnitInInv(WhoseMoveCom.CurPlayer, unitType))
                {
                    selCom.SelUnitType = unitType;
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
            if (!_invUnitsFilt.Get1(0).HaveUnitInInv(WhoseMoveCom.CurPlayer, UnitTypes.King))
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
                if (_cellUnitFilter.Get1(IdxSelectedCell).IsCondType(conditionUnitType))
                {
                    RpcSys.ConditionUnitToMaster(CondUnitTypes.None, IdxSelectedCell);
                }
                else
                {
                    RpcSys.ConditionUnitToMaster(conditionUnitType, IdxSelectedCell);
                }
            }
        }

        private void ActiveGiveTakeButton()
        {
            if (WhoseMoveCom.IsMyOnlineMove || GameModesCom.IsOfflineMode)
            {
                ref var selecComp = ref _selectorFilter.Get1(0);

                if (selecComp.CellClickType == CellClickTypes.GiveTakeTW)
                {
                    selecComp.CellClickType = default;
                }

                else
                {
                    selecComp.CellClickType = CellClickTypes.GiveTakeTW;
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
            _selectorFilter.Get1(0).ToolWeaponTypeForGiveTake = toolAndWeaponType;
        }
    }
}