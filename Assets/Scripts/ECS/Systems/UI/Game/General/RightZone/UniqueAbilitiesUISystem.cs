using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Common;
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

        ref var selUnitDatCom = ref _cellUnitFilter.Get1(IdxSelCell);
        ref var selOwnUnitCom = ref _cellUnitFilter.Get2(IdxSelCell);
        ref var selBotUnitCom = ref _cellUnitFilter.Get3(IdxSelCell);

        ref var selEnvDataCom = ref _cellEnvFilter.Get1(IdxSelCell);

        ref var selFireDatCom = ref _cellFireFilter.Get1(IdxSelCell);


        unitZoneViewCom.SetTextUniqueInfo(LanguageComComp.GetText(GameLanguageTypes.UniqueAbilities));

        if (selUnitDatCom.HaveUnit)
        {
            if (selOwnUnitCom.HaveOwner)
            {
                if (selOwnUnitCom.IsMine)
                {
                    unitZoneViewCom.RemoveAllListenersInUniqueButton(UniqueButtonTypes.First);
                    unitZoneViewCom.RemoveAllListenersInUniqueButton(UniqueButtonTypes.Second);
                    unitZoneViewCom.RemoveAllListenersInUniqueButton(UniqueButtonTypes.Third);

                    unitZoneViewCom.SetActiveUniqeButton(UniqueButtonTypes.Second, false);
                    unitZoneViewCom.SetActiveUniqeButton(UniqueButtonTypes.Third, false);


                    if (selUnitDatCom.IsUnitType(UnitTypes.King))
                    {
                        unitZoneViewCom.SetActiveUnitZone(UnitUIZoneTypes.Unique, true);
                        unitZoneViewCom.SetActiveUniqeButton(UniqueButtonTypes.First, true);

                        unitZoneViewCom.AddListenerToUniqueButton(UniqueButtonTypes.First, CircularAttackKing);

                        unitZoneViewCom.SetTextToUnique(UniqueButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.CircularAttack));

                        unitZoneViewCom.SetColoToUniqueAbilityButton(UniqueButtonTypes.First, new Color(1, 0.5f, 0.5f, 1));
                    }
                    else
                    {
                        if (selUnitDatCom.IsMelee)
                        {
                            unitZoneViewCom.SetActiveUniqeButton(UniqueButtonTypes.First, true);

                            unitZoneViewCom.SetActiveUnitZone(UnitUIZoneTypes.Unique, true);
                            unitZoneViewCom.SetActiveUniqeButton(UniqueButtonTypes.First, true);

                            if (selEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                            {
                                unitZoneViewCom.AddListenerToUniqueButton(UniqueButtonTypes.First, delegate { Fire(IdxSelCell, IdxSelCell); });
                                if (selFireDatCom.HaveFire)
                                {
                                    unitZoneViewCom.SetTextToUnique(UniqueButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.PutOutFire));
                                }
                                else
                                {
                                    unitZoneViewCom.SetTextToUnique(UniqueButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.FireForest));
                                }
                                unitZoneViewCom.SetColoToUniqueAbilityButton(UniqueButtonTypes.First, new Color(1, 0.5f, 0.5f, 1));
                            }
                            else
                            {
                                unitZoneViewCom.AddListenerToUniqueButton(UniqueButtonTypes.First, delegate { SeedEnvironment(EnvironmentTypes.YoungForest); });
                                unitZoneViewCom.SetTextToUnique(UniqueButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.SeedForest));
                                unitZoneViewCom.SetColoToUniqueAbilityButton(UniqueButtonTypes.First, new Color(0.5f, 1, 0.5f, 1));
                            }
                        }

                        else
                        {
                            unitZoneViewCom.SetActiveUnitZone(UnitUIZoneTypes.Unique, true);
                            unitZoneViewCom.SetActiveUniqeButton(UniqueButtonTypes.First, true);
                            unitZoneViewCom.AddListenerToUniqueButton(UniqueButtonTypes.First, ActiveFireSelector);
                            unitZoneViewCom.SetColoToUniqueAbilityButton(UniqueButtonTypes.First, new Color(1, 0.5f, 0.5f, 1));
                            unitZoneViewCom.SetTextToUnique(UniqueButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.FireForest));
                        }
                    }
                }

                else
                {
                    unitZoneViewCom.SetActiveUnitZone(UnitUIZoneTypes.Unique, false);
                }
            }

            else if (selBotUnitCom.IsBot)
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
        if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RpcSys.SeedEnvironmentToMaster(IdxSelCell, environmentType);
    }

    private void Fire(byte fromIdx, byte toIdx)
    {
        if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RpcSys.FireToMaster(fromIdx, toIdx);
    }

    private void CircularAttackKing()
    {
        if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) RpcSys.CircularAttackKingToMaster(IdxSelCell);
    }

    private void ActiveFireSelector()
    {
        if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient)) _selectorFilter.Get1(0).CellClickType = CellClickTypes.PickFire;
    }
}
