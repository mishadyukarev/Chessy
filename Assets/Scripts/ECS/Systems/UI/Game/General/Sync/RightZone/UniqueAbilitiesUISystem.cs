using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class UniqueAbilitiesUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;
    private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;

    private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
    private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

    private byte IdxSelCell => _selectorFilter.Get1(0).IdxSelectedCell;

    public void Run()
    {
        ref var selCellUnitDataCom = ref _cellUnitFilter.Get1(IdxSelCell);
        ref var selOwnerCellUnitCom = ref _cellUnitFilter.Get2(IdxSelCell);
        ref var selBotOnwerCellUnitCom = ref _cellUnitFilter.Get3(IdxSelCell);

        ref var selCellEnvDataCom = ref _cellEnvFilter.Get1(IdxSelCell);

        ref var selCellFireDataCom = ref _cellFireFilter.Get1(IdxSelCell);


        if (selCellUnitDataCom.HaveUnit)
        {
            if (selOwnerCellUnitCom.HaveOwner)
            {
                if (selOwnerCellUnitCom.IsMine)
                {
                    _unitZoneUIFilter.Get1(0).RemoveAllListenersInUniqueButton(UniqueButtonTypes.First);
                    _unitZoneUIFilter.Get1(0).RemoveAllListenersInUniqueButton(UniqueButtonTypes.Second);
                    _unitZoneUIFilter.Get1(0).RemoveAllListenersInUniqueButton(UniqueButtonTypes.Third);

                    _unitZoneUIFilter.Get1(0).SetActiveUniqeButton(UniqueButtonTypes.Second, false);
                    _unitZoneUIFilter.Get1(0).SetActiveUniqeButton(UniqueButtonTypes.Third, false);


                    if (selCellUnitDataCom.IsUnitType(UnitTypes.King))
                    {
                        _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Unique, true);
                        _unitZoneUIFilter.Get1(0).SetActiveUniqeButton(UniqueButtonTypes.First, true);

                        _unitZoneUIFilter.Get1(0).AddListenerToUniqueButton(UniqueButtonTypes.First, CircularAttackKing);
                    }
                    else
                    {
                        if (selCellUnitDataCom.IsMelee)
                        {
                            _unitZoneUIFilter.Get1(0).SetActiveUniqeButton(UniqueButtonTypes.First, true);

                            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Unique, true);
                            _unitZoneUIFilter.Get1(0).SetActiveUniqeButton(UniqueButtonTypes.First, true);

                            if (selCellEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                            {
                                _unitZoneUIFilter.Get1(0).AddListenerToUniqueButton(UniqueButtonTypes.First, delegate { Fire(IdxSelCell, IdxSelCell); });
                                if (selCellFireDataCom.HaveFire)
                                {
                                    _unitZoneUIFilter.Get1(0).SetTextToUnique(UniqueButtonTypes.First, "Put Out FIRE");
                                }
                                else
                                {
                                    _unitZoneUIFilter.Get1(0).SetTextToUnique(UniqueButtonTypes.First, "Fire forest");
                                }

                            }
                            else
                            {
                                _unitZoneUIFilter.Get1(0).AddListenerToUniqueButton(UniqueButtonTypes.First, delegate { SeedEnvironment(EnvironmentTypes.YoungForest); });
                                _unitZoneUIFilter.Get1(0).SetTextToUnique(UniqueButtonTypes.First, "Seed Forest");
                            }
                        }

                        else
                        {
                            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Unique, true);
                            _unitZoneUIFilter.Get1(0).SetActiveUniqeButton(UniqueButtonTypes.First, true);
                            _unitZoneUIFilter.Get1(0).AddListenerToUniqueButton(UniqueButtonTypes.First, ActiveFireSelector);
                        }
                    }
                }

                else
                {
                    _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Unique, false);
                }
            }
            else if (selBotOnwerCellUnitCom.IsBot)
            {
                _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Unique, false);
            }
        }

        else
        {
            _unitZoneUIFilter.Get1(0).SetActiveUnitZone(UnitUIZoneTypes.Unique, false);
        }
    }

    private void SeedEnvironment(EnvironmentTypes environmentType)
    {
        if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.SeedEnvironmentToMaster(IdxSelCell, environmentType);
    }

    private void Fire(byte fromIdx, byte toIdx)
    {
        if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RPCGameSystem.FireToMaster(fromIdx, toIdx);
    }

    private void CircularAttackKing()
    {
        RPCGameSystem.CircularAttackKingToMaster(IdxSelCell);
    }

    private void ActiveFireSelector()
    {
        if (_selectorFilter.Get1(0).CellClickType == CellClickTypes.PickFire)
        {
            _selectorFilter.Get1(0).CellClickType = CellClickTypes.Start;
        }
        else
        {
            _selectorFilter.Get1(0).CellClickType = CellClickTypes.PickFire;
        }
    }
}
