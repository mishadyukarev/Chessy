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

    public void Run()
    {
        var idxSelCell = _selectorFilter.Get1(0).IdxSelCell;

        ref var unitZoneViewCom = ref _unitZoneUIFilter.Get1(0);

        ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);
        ref var selOnUnitCom = ref _cellUnitFilter.Get2(idxSelCell);
        ref var selOffUnitCom = ref _cellUnitFilter.Get3(idxSelCell);
        ref var selBotUnitCom = ref _cellUnitFilter.Get4(idxSelCell);

        ref var selEnvDataCom = ref _cellEnvFilter.Get1(idxSelCell);

        ref var selFireDatCom = ref _cellFireFilter.Get1(idxSelCell);


        unitZoneViewCom.SetTextInfo(LanguageComComp.GetText(GameLanguageTypes.UniqueAbilities));

        unitZoneViewCom.SetActive_Button(UniqueButtonTypes.First, false);
        unitZoneViewCom.SetActive_Button(UniqueButtonTypes.Second, false);
       


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
            else if (selOffUnitCom.HaveLocalPlayer)
            {
                if (selOffUnitCom.IsMine)
                {
                    canCome = true;
                }
            }

            if (canCome)
            {
                if (selUnitDatCom.IsUnit(UnitTypes.King))
                {
                    unitZoneViewCom.SetActive_Button(UniqueButtonTypes.First, true);
                    unitZoneViewCom.SetText_Button(UniqueButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.CircularAttack));
                    unitZoneViewCom.SetColor_Button(UniqueButtonTypes.First, new Color(1, 0.5f, 0.5f, 1));
                }
                else
                {
                    if (selUnitDatCom.IsMelee)
                    {
                        unitZoneViewCom.SetActive_Button(UniqueButtonTypes.First, true);

                        unitZoneViewCom.SetActive_Button(UniqueButtonTypes.First, true);

                        if (selEnvDataCom.HaveEnvir(EnvirTypes.AdultForest))
                        {
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
                            unitZoneViewCom.SetText_Button(UniqueButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.SeedForest));
                            unitZoneViewCom.SetColor_Button(UniqueButtonTypes.First, new Color(0.5f, 1, 0.5f, 1));
                        }
                    }

                    else
                    {
                        unitZoneViewCom.SetActive_Button(UniqueButtonTypes.First, true);
                        unitZoneViewCom.SetColor_Button(UniqueButtonTypes.First, new Color(1, 0.5f, 0.5f, 1));
                        unitZoneViewCom.SetText_Button(UniqueButtonTypes.First, LanguageComComp.GetText(GameLanguageTypes.FireForest));
                    }
                }
            }
        }
    }


}
