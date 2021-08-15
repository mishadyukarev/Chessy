﻿using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.Cell;
using Assets.Scripts.Abstractions.Enums.Cell.Pawn;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Assets.Scripts
{
    internal sealed class StaticEventsGameSys : IEcsInitSystem
    {
        private EcsFilter<CellUnitDataComponent> _cellUnitFilter = default;

        private EcsFilter<ReadyDataUICom, ReadyViewUICom> _readyFilter = default;
        private EcsFilter<SelectorComponent> _selectorFilter = default;
        private EcsFilter<LeaveViewUIComponent> _leaveUIFilter = default;
        private EcsFilter<GetterUnitsDataUICom, GetterUnitsViewUICom> _takerUIFilter = default;
        private EcsFilter<DonerDataUIComponent, DonerViewUIComponent> _donerUIFilter = default;
        private EcsFilter<EnvirZoneDataUICom, EnvirZoneViewUICom> _envirZoneUIFilter = default;
        private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;
        private EcsFilter<BuildLeftZoneViewUICom> _buildLeftZoneViewUICom = default;

        private EcsFilter<InventorUnitsComponent> _inventorUnitsFilter = default;

        private byte IdxSelectedCell => _selectorFilter.Get1(0).IdxSelectedCell;
        private bool IsDoned(bool key) => _donerUIFilter.Get1(0).IsDoned(key);

        public void Init()
        {
            _readyFilter.Get2(0).AddListenerToReadyButton(Ready);

            _takerUIFilter.Get2(0).AddListener(UnitTypes.King, delegate { GetUnit(UnitTypes.King); });
            _takerUIFilter.Get2(0).AddListener(UnitTypes.Pawn, delegate { GetUnit(UnitTypes.Pawn); });
            _takerUIFilter.Get2(0).AddListener(UnitTypes.Rook, delegate { GetUnit(UnitTypes.Rook); });
            _takerUIFilter.Get2(0).AddListener(UnitTypes.Bishop, delegate { GetUnit(UnitTypes.Bishop); });

            _takerUIFilter.Get2(0).AddListenerToCreateUnit(UnitTypes.Pawn, delegate { CreateUnit(UnitTypes.Pawn); });
            _takerUIFilter.Get2(0).AddListenerToCreateUnit(UnitTypes.Rook, delegate { CreateUnit(UnitTypes.Rook); });
            _takerUIFilter.Get2(0).AddListenerToCreateUnit(UnitTypes.Bishop, delegate { CreateUnit(UnitTypes.Bishop); });

            _donerUIFilter.Get2(0).AddListener(delegate { Done(); });

            _envirZoneUIFilter.Get2(0).AddListenerToEnvInfo(EnvironmentInfo);

            _leaveUIFilter.Get1(0).AddListener(PhotonSceneGameGeneralSystem.LeaveRoom);

            _unitZoneUIFilter.Get1(0).AddListenerToCondtionButton(ConditionUnitTypes.Protected, delegate { ConditionAbilityButton(ConditionUnitTypes.Protected); });
            _unitZoneUIFilter.Get1(0).AddListenerToCondtionButton(ConditionUnitTypes.Relaxed, delegate { ConditionAbilityButton(ConditionUnitTypes.Relaxed); });



            _buildLeftZoneViewUICom.Get1(0).AddListenerToMelt(delegate { MeltOre(); });

            _buildLeftZoneViewUICom.Get1(0).AddListenerToTakePawnTool(delegate { SetCellClickType(CellClickTypes.TakeExtraThing); });

            //_buildLeftZoneViewUICom.Get1(0).AddListenerToGiveTool(PawnToolTypes.Hoe, delegate { SetCellClickSelector(PawnToolTypes.Hoe); });
            _buildLeftZoneViewUICom.Get1(0).AddListenerToGiveTool(PawnExtraToolTypes.Pick, delegate { SetCellClickSelector(PawnExtraToolTypes.Pick); });
            _buildLeftZoneViewUICom.Get1(0).AddListenerToGiveWeapon(delegate { SetCellClickSelector(PawnExtraWeaponTypes.Sword); });

            _buildLeftZoneViewUICom.Get1(0).AddListenerToBuildUpgrade(BuildingTypes.Farm, delegate { UpgradeBuilding(BuildingTypes.Farm); });
            _buildLeftZoneViewUICom.Get1(0).AddListenerToBuildUpgrade(BuildingTypes.Woodcutter, delegate { UpgradeBuilding(BuildingTypes.Woodcutter); });
            _buildLeftZoneViewUICom.Get1(0).AddListenerToBuildUpgrade(BuildingTypes.Mine, delegate { UpgradeBuilding(BuildingTypes.Mine); });
        }



        private void Ready() => RPCGameSystem.ReadyToMaster(!_readyFilter.Get1(0).IsReady(PhotonNetwork.IsMasterClient));

        private void GetUnit(UnitTypes unitType)
        {
            _selectorFilter.Get1(0).IdxCurrentCell = default;
            _selectorFilter.Get1(0).IdxPreviousVisionCell = default;
            _selectorFilter.Get1(0).ResetSelectedCell();
            _takerUIFilter.Get1(0).ResetCurTimer(unitType);

            if (!IsDoned(PhotonNetwork.IsMasterClient))
            {
                if(_inventorUnitsFilter.Get1(0).HaveUnitInInventor(unitType, PhotonNetwork.IsMasterClient))
                {
                    RPCGameSystem.GetUnitToMaster(unitType);
                }
                else
                {
                    _takerUIFilter.Get1(0).ActiveNeedCreateButton(unitType, PhotonNetwork.IsMasterClient);
                }
            }
        }

        private void Done()
        {
            switch (SaverComponent.StepModeType)
            {
                case StepModeTypes.None:
                    throw new Exception();

                case StepModeTypes.ByQueue:
                    if (!IsDoned(PhotonNetwork.IsMasterClient))
                        RPCGameSystem.DoneToMaster(!IsDoned(PhotonNetwork.IsMasterClient));
                    break;

                case StepModeTypes.Together:
                    RPCGameSystem.DoneToMaster(!IsDoned(PhotonNetwork.IsMasterClient));
                    break;

                default:
                    throw new Exception();
            }
        }

        private void EnvironmentInfo()
        {
            _envirZoneUIFilter.Get1(0).IsActivatedInfo = !_envirZoneUIFilter.Get1(0).IsActivatedInfo;
        }

        private void ConditionAbilityButton(ConditionUnitTypes conditionUnitType)
        {
            if (!IsDoned(PhotonNetwork.IsMasterClient))
            {
                if (_cellUnitFilter.Get1(IdxSelectedCell).IsConditionType(conditionUnitType))
                {
                    RPCGameSystem.ConditionUnitToMaster(ConditionUnitTypes.None, IdxSelectedCell);
                }
                else
                {
                    RPCGameSystem.ConditionUnitToMaster(conditionUnitType, IdxSelectedCell);
                }
            }
        }

        private void SetCellClickSelector(PawnExtraToolTypes toolType)
        {
            _selectorFilter.Get1(0).PawnExtraToolTypeForGive = toolType;
            _selectorFilter.Get1(0).CellClickType = CellClickTypes.GiveExtraThing;
        }
        private void SetCellClickSelector(PawnExtraWeaponTypes weaponType)
        {
            _selectorFilter.Get1(0).PawnExtraWeaponTypeForGive = weaponType;
            _selectorFilter.Get1(0).CellClickType = CellClickTypes.TakeExtraThing;
        }

        private void SetCellClickType(CellClickTypes cellClickType) => _selectorFilter.Get1(0).CellClickType = cellClickType;

        private void CreateUnit(UnitTypes unitType)
        {
            _takerUIFilter.Get1(0).ResetCurTimer(unitType);

            if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.CreateUnitToMaster(unitType);
        }

        private void MeltOre()
        {
            if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.MeltOreToMaster();
        }

        private void UpgradeBuilding(BuildingTypes buildingType)
        {
            if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.UpgradeBuildingToMaster(buildingType);
        }
    }
}