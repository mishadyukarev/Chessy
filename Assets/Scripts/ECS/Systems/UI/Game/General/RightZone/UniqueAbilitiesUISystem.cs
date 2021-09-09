using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

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
        ref var unitZoneViewCom = ref _unitZoneUIFilter.Get1(0);

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
                    unitZoneViewCom.RemoveAllListenersInUniqueButton(UniqueButtonTypes.First);
                    unitZoneViewCom.RemoveAllListenersInUniqueButton(UniqueButtonTypes.Second);
                    unitZoneViewCom.RemoveAllListenersInUniqueButton(UniqueButtonTypes.Third);

                    unitZoneViewCom.SetActiveUniqeButton(UniqueButtonTypes.Second, false);
                    unitZoneViewCom.SetActiveUniqeButton(UniqueButtonTypes.Third, false);


                    if (selCellUnitDataCom.IsUnitType(UnitTypes.King))
                    {
                        unitZoneViewCom.SetActiveUnitZone(UnitUIZoneTypes.Unique, true);
                        unitZoneViewCom.SetActiveUniqeButton(UniqueButtonTypes.First, true);

                        unitZoneViewCom.AddListenerToUniqueButton(UniqueButtonTypes.First, CircularAttackKing);

                        unitZoneViewCom.SetTextToUnique(UniqueButtonTypes.First, "Circular Attack");

                        unitZoneViewCom.SetColoToUniqueAbilityButton(UniqueButtonTypes.First, new Color(1, 0.5f, 0.5f, 1));
                    }
                    else
                    {
                        if (selCellUnitDataCom.IsMelee)
                        {
                            unitZoneViewCom.SetActiveUniqeButton(UniqueButtonTypes.First, true);

                            unitZoneViewCom.SetActiveUnitZone(UnitUIZoneTypes.Unique, true);
                            unitZoneViewCom.SetActiveUniqeButton(UniqueButtonTypes.First, true);

                            if (selCellEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                            {
                                unitZoneViewCom.AddListenerToUniqueButton(UniqueButtonTypes.First, delegate { Fire(IdxSelCell, IdxSelCell); });
                                if (selCellFireDataCom.HaveFire)
                                {
                                    unitZoneViewCom.SetTextToUnique(UniqueButtonTypes.First, "Put Out FIRE");
                                }
                                else
                                {
                                    unitZoneViewCom.SetTextToUnique(UniqueButtonTypes.First, "Fire forest");
                                }
                                unitZoneViewCom.SetColoToUniqueAbilityButton(UniqueButtonTypes.First, new Color(1, 0.5f, 0.5f, 1));
                            }
                            else
                            {
                                unitZoneViewCom.AddListenerToUniqueButton(UniqueButtonTypes.First, delegate { SeedEnvironment(EnvironmentTypes.YoungForest); });
                                unitZoneViewCom.SetTextToUnique(UniqueButtonTypes.First, "Seed Forest");
                                unitZoneViewCom.SetColoToUniqueAbilityButton(UniqueButtonTypes.First, new Color(0.5f, 1, 0.5f, 1));
                            }
                        }

                        else
                        {
                            unitZoneViewCom.SetActiveUnitZone(UnitUIZoneTypes.Unique, true);
                            unitZoneViewCom.SetActiveUniqeButton(UniqueButtonTypes.First, true);
                            unitZoneViewCom.AddListenerToUniqueButton(UniqueButtonTypes.First, ActiveFireSelector);
                            unitZoneViewCom.SetColoToUniqueAbilityButton(UniqueButtonTypes.First, new Color(1, 0.5f, 0.5f, 1));
                            unitZoneViewCom.SetTextToUnique(UniqueButtonTypes.First, "Fire forest");
                        }
                    }
                }

                else
                {
                    unitZoneViewCom.SetActiveUnitZone(UnitUIZoneTypes.Unique, false);
                }
            }

            else if (selBotOnwerCellUnitCom.IsBot)
            {
                unitZoneViewCom.SetActiveUnitZone(UnitUIZoneTypes.Unique, false);
            }
        }

        else
        {
            unitZoneViewCom.SetActiveUnitZone(UnitUIZoneTypes.Unique, false);
        }
    }

    private void SeedEnvironment(EnvironmentTypes environmentType)
    {
        if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RpcGeneralSystem.SeedEnvironmentToMaster(IdxSelCell, environmentType);
    }

    private void Fire(byte fromIdx, byte toIdx)
    {
        if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RpcGeneralSystem.FireToMaster(fromIdx, toIdx);
    }

    private void CircularAttackKing()
    {
        RpcGeneralSystem.CircularAttackKingToMaster(IdxSelCell);
    }

    private void ActiveFireSelector()
    {
        _selectorFilter.Get1(0).CellClickType = CellClickTypes.PickFire;
    }
}
