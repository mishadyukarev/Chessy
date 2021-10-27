using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class EventsGameSys : IEcsInitSystem
    {
        private EcsFilter<CellUnitDataCom> _cellUnitFilter = default;

        private EcsFilter<LeaveViewUIC> _leaveUIFilter = default;
        private EcsFilter<DonerUICom> _donerUIFilter = default;
        private EcsFilter<EnvirZoneDataUIC, EnvirZoneViewUICom> _envirZoneUIFilter = default;
        private EcsFilter<BuildLeftZoneViewUICom> _buildLeftZoneViewUICom = default;

        public void Init()
        {
            ReadyViewUIC.AddListenerToReadyButton(Ready);

            KingZoneViewUIC.AddListenerToSetKing_Button(delegate { GetUnit(UnitTypes.King); });
            GetterUnitsViewUIC.AddListener(UnitTypes.Pawn, delegate { GetUnit(UnitTypes.Pawn); });
            GetterUnitsViewUIC.AddListener(UnitTypes.Rook, delegate { GetUnit(UnitTypes.Rook); });
            GetterUnitsViewUIC.AddListener(UnitTypes.Bishop, delegate { GetUnit(UnitTypes.Bishop); });

            GetterUnitsViewUIC.AddListenerToCreateUnit(UnitTypes.Pawn, delegate { CreateUnit(UnitTypes.Pawn); });
            GetterUnitsViewUIC.AddListenerToCreateUnit(UnitTypes.Rook, delegate { CreateUnit(UnitTypes.Rook); });
            GetterUnitsViewUIC.AddListenerToCreateUnit(UnitTypes.Bishop, delegate { CreateUnit(UnitTypes.Bishop); });

            _donerUIFilter.Get1(0).AddListener(Done);

            _envirZoneUIFilter.Get2(0).AddListenerToEnvInfo(EnvironmentInfo);

            _leaveUIFilter.Get1(0).AddListener(delegate { PhotonNetwork.LeaveRoom(); });

            CondUnitUIC.AddListener(CondUnitTypes.Protected, delegate { ConditionAbilityButton(CondUnitTypes.Protected); });
            CondUnitUIC.AddListener(CondUnitTypes.Relaxed, delegate { ConditionAbilityButton(CondUnitTypes.Relaxed); });


            GiveTakeViewUIC.AddListUpgradeButton(ToggleUpgradeUnit);
            GiveTakeViewUIC.AddList_Button(ToolWeaponTypes.Pick, delegate { ToggleToolWeapon(ToolWeaponTypes.Pick); });
            GiveTakeViewUIC.AddList_Button(ToolWeaponTypes.Sword, delegate { ToggleToolWeapon(ToolWeaponTypes.Sword); });
            GiveTakeViewUIC.AddList_Button(ToolWeaponTypes.Shield, delegate { ToggleToolWeapon(ToolWeaponTypes.Shield); });


            FriendZoneViewUIC.AddListenerReady(ReadyFriend);





            ref var buildLeftZoneViewUICom = ref _buildLeftZoneViewUICom.Get1(0);

            buildLeftZoneViewUICom.AddListenerToMelt(delegate { MeltOre(); });

            buildLeftZoneViewUICom.AddListBuildUpgrade(BuildingTypes.Farm, delegate { UpgradeBuilding(BuildingTypes.Farm); });
            buildLeftZoneViewUICom.AddListBuildUpgrade(BuildingTypes.Woodcutter, delegate { UpgradeBuilding(BuildingTypes.Woodcutter); });
            buildLeftZoneViewUICom.AddListBuildUpgrade(BuildingTypes.Mine, delegate { UpgradeBuilding(BuildingTypes.Mine); });





            HintViewUIC.AddListHint_But(ExecuteHint);
        }



        private void Ready() => RpcSys.ReadyToMaster();

        private void GetUnit(UnitTypes unitType)
        {
            SelectorC.DefCellClickType();
            SelectorC.IdxCurCell = default;
            SelectorC.IdxPreVisionCell = default;
            SelectorC.DefSelectedCell();
            GetterUnitsDataUIC.ResetCurTimer(unitType);

            if (WhoseMoveC.IsMyMove)
            {
                if (InventorUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayer, unitType, LevelUnitTypes.Iron))
                {
                    SelectorC.SelUnitType = unitType;
                    SelectorC.LevelSelUnitType = LevelUnitTypes.Iron;
                }
                else if (InventorUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayer, unitType, LevelUnitTypes.Wood))
                {
                    SelectorC.SelUnitType = unitType;
                    SelectorC.LevelSelUnitType = LevelUnitTypes.Wood;
                }
                else
                {
                    GetterUnitsDataUIC.ActiveNeedCreateButton(unitType, true);
                }

            }
        }

        internal void ReadyFriend()
        {
            FriendZoneDataUIC.IsActiveFriendZone = false;
        }

        private void Done()
        {
            if (!InventorUnitsC.HaveUnitInInv(WhoseMoveC.CurPlayer, UnitTypes.King, LevelUnitTypes.Wood))
            {
                RpcSys.DoneToMaster();
            }
            else
            {
                SoundEffectC.Play(SoundEffectTypes.Mistake);
            }

            SelectorC.DefCellClickType();
            SelectorC.DefSelectedUnit();
        }

        private void EnvironmentInfo()
        {
            EnvirZoneDataUIC.IsActivatedInfo = !EnvirZoneDataUIC.IsActivatedInfo;
        }

        private void ConditionAbilityButton(CondUnitTypes conditionUnitType)
        {
            if (WhoseMoveC.IsMyMove)
            {
                if (_cellUnitFilter.Get1(SelectorC.IdxSelCell).Is(conditionUnitType))
                {
                    RpcSys.ConditionUnitToMaster(CondUnitTypes.None, SelectorC.IdxSelCell);
                }
                else
                {
                    RpcSys.ConditionUnitToMaster(conditionUnitType, SelectorC.IdxSelCell);
                }
            }
        }

        private void CreateUnit(UnitTypes unitType)
        {
            GetterUnitsDataUIC.ResetCurTimer(unitType);

            if (WhoseMoveC.IsMyMove) RpcSys.CreateUnitToMaster(unitType);
        }

        private void MeltOre()
        {
            if (WhoseMoveC.IsMyMove) RpcSys.MeltOreToMaster();
        }

        private void UpgradeBuilding(BuildingTypes buildingType)
        {
            if (WhoseMoveC.IsMyMove) RpcSys.UpgradeBuildingToMaster(buildingType);
        }

        private void ToggleToolWeapon(ToolWeaponTypes tWType)
        {
            if (WhoseMoveC.IsMyMove)
            {
                if (SelectorC.IsCellClickType(CellClickTypes.GiveTakeTW))
                {
                    if (tWType == ToolWeaponTypes.Shield)
                    {
                        if (SelectorC.TWTypeForGive == tWType)
                        {
                            if (SelectorC.LevelTWType == LevelTWTypes.Wood) SelectorC.LevelTWType = LevelTWTypes.Iron;
                            else SelectorC.LevelTWType = LevelTWTypes.Wood;
                        }
                        else
                        {
                            SelectorC.TWTypeForGive = tWType;
                            SelectorC.LevelTWType = LevelTWTypes.Wood;
                        }
                    }
                    else
                    {
                        SelectorC.TWTypeForGive = tWType;
                        SelectorC.LevelTWType = LevelTWTypes.Iron;
                    }
                }
                else
                {
                    SelectorC.CellClickType = CellClickTypes.GiveTakeTW;
                    SelectorC.TWTypeForGive = tWType;

                    if (tWType == ToolWeaponTypes.Shield) SelectorC.LevelTWType = LevelTWTypes.Wood;
                    else SelectorC.LevelTWType = LevelTWTypes.Iron; 
                }
            }
        }

        private void ExecuteHint()
        {
            HintDataUIC.CurNumber++;

            if (HintDataUIC.CurNumber < System.Enum.GetNames(typeof(VideoClipTypes)).Length)
            {
                HintViewUIC.SetVideoClip((VideoClipTypes)HintDataUIC.CurNumber);
                HintViewUIC.SetPos(new Vector3(Random.Range(-500f, 500f), Random.Range(-300f, 300f)));
            }
            else
            {
                HintViewUIC.SetActiveHintZone(false);
            }
        }

        private void ToggleUpgradeUnit() => SelectorC.CellClickType = CellClickTypes.UpgradeUnit;
    }
}