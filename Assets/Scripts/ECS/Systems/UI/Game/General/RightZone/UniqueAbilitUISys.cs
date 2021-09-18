using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.View.UI.Game.General.Right;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

internal sealed class UniqueAbilitUISys : IEcsRunSystem
{
    private EcsFilter<SelectorCom> _selectorFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;
    private EcsFilter<UniqueAbiltUICom> _unitZoneUIFilter = default;

    private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
    private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

    private byte IdxSelCell => _selectorFilter.Get1(0).IdxSelCell;

    public void Run()
    {
        ref var unitZoneViewCom = ref _unitZoneUIFilter.Get1(0);

        ref var selUnitDatCom = ref _cellUnitFilter.Get1(IdxSelCell);
        ref var selOnUnitCom = ref _cellUnitFilter.Get2(IdxSelCell);
        ref var selOffUnitCom = ref _cellUnitFilter.Get3(IdxSelCell);
        ref var selBotUnitCom = ref _cellUnitFilter.Get4(IdxSelCell);

        ref var selEnvDataCom = ref _cellEnvFilter.Get1(IdxSelCell);

        ref var selFireDatCom = ref _cellFireFilter.Get1(IdxSelCell);


        unitZoneViewCom.SetTextInfo(LanguageComComp.GetText(GameLanguageTypes.UniqueAbilities));


        if (selUnitDatCom.HaveUnit)
        {
            var canCome = false;

            if (selOnUnitCom.HaveOwner)
            {
                if (selOnUnitCom.IsMine)
                {
                    canCome = true;
                }
            }
            else if (selOffUnitCom.HaveLocPlayer)
            {
                if (selOffUnitCom.IsMine)
                {
                    canCome = true;
                }
            }

            if (canCome)
            {
                unitZoneViewCom.RemoveAllList(UniqueButtonTypes.First);
                unitZoneViewCom.RemoveAllList(UniqueButtonTypes.Second);
                unitZoneViewCom.RemoveAllList(UniqueButtonTypes.Third);

                unitZoneViewCom.SetActive_Button(UniqueButtonTypes.Second, false);
                unitZoneViewCom.SetActive_Button(UniqueButtonTypes.Third, false);


                if (selUnitDatCom.Is(UnitTypes.King))
                {
                    unitZoneViewCom.SetActive_Button(UniqueButtonTypes.First, true);

                    unitZoneViewCom.AddListener_Button(UniqueButtonTypes.First, CircularAttackKing);

                    unitZoneViewCom.SetText_Button(UniqueButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.CircularAttack));

                    unitZoneViewCom.SetColor_Button(UniqueButtonTypes.First, new Color(1, 0.5f, 0.5f, 1));
                }
                else
                {
                    if (selUnitDatCom.IsMelee)
                    {
                        unitZoneViewCom.SetActive_Button(UniqueButtonTypes.First, true);

                        unitZoneViewCom.SetActive_Button(UniqueButtonTypes.First, true);

                        if (selEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                        {
                            unitZoneViewCom.AddListener_Button(UniqueButtonTypes.First, delegate { Fire(IdxSelCell, IdxSelCell); });
                            if (selFireDatCom.HaveFire)
                            {
                                unitZoneViewCom.SetText_Button(UniqueButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.PutOutFire));
                            }
                            else
                            {
                                unitZoneViewCom.SetText_Button(UniqueButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.FireForest));
                            }
                            unitZoneViewCom.SetColor_Button(UniqueButtonTypes.First, new Color(1, 0.5f, 0.5f, 1));
                        }
                        else
                        {
                            unitZoneViewCom.AddListener_Button(UniqueButtonTypes.First, delegate { SeedEnvironment(EnvironmentTypes.YoungForest); });
                            unitZoneViewCom.SetText_Button(UniqueButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.SeedForest));
                            unitZoneViewCom.SetColor_Button(UniqueButtonTypes.First, new Color(0.5f, 1, 0.5f, 1));
                        }
                    }

                    else
                    {
                        unitZoneViewCom.SetActive_Button(UniqueButtonTypes.First, true);
                        unitZoneViewCom.AddListener_Button(UniqueButtonTypes.First, ActiveFireSelector);
                        unitZoneViewCom.SetColor_Button(UniqueButtonTypes.First, new Color(1, 0.5f, 0.5f, 1));
                        unitZoneViewCom.SetText_Button(UniqueButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.FireForest));
                    }
                }
            }
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
