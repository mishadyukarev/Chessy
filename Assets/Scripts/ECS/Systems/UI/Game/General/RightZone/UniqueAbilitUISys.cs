using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.View.UI.Game.General.Right;
using Leopotam.Ecs;
using UnityEngine;

internal sealed class UniqueAbilitUISys : IEcsRunSystem
{
    private EcsFilter<SelectorCom> _selectorFilter = default;
    private EcsFilter<UniqueAbiltUICom> _unitZoneUIFilter = default;

    private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
    private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

    public void Run()
    {
        var idxSelCell = _selectorFilter.Get1(0).IdxSelCell;

        ref var unitZoneViewCom = ref _unitZoneUIFilter.Get1(0);

        ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);
        ref var selOwnUnitCom = ref _cellUnitFilter.Get2(idxSelCell);

        ref var selEnvDataCom = ref _cellEnvFilter.Get1(idxSelCell);

        ref var selFireDatCom = ref _cellFireFilter.Get1(idxSelCell);


        unitZoneViewCom.SetTextInfo(LanguageComCom.GetText(GameLanguageTypes.UniqueAbilities));



        var activeFirst = false;
        var activeSecond = false;

        if (selUnitDatCom.HaveUnit)
        {
            if (selOwnUnitCom.IsPlayerType(WhoseMoveCom.CurPlayer))
            {
                if (selUnitDatCom.IsUnit(UnitTypes.King))
                {
                    activeFirst = true;
                    unitZoneViewCom.SetText_Button(UniqueButtonTypes.First, LanguageComCom.GetText(GameLanguageTypes.CircularAttack));
                    unitZoneViewCom.SetColor_Button(UniqueButtonTypes.First, new Color(1, 0.5f, 0.5f, 1));
                }
                else
                {
                    if (selUnitDatCom.IsMelee)
                    {
                        activeFirst = true;

                        if (selEnvDataCom.HaveEnvir(EnvirTypes.AdultForest))
                        {
                            if (selFireDatCom.HaveFire)
                            {
                                unitZoneViewCom.SetText_Button(UniqueButtonTypes.First, LanguageComCom.GetText(GameLanguageTypes.PutOutFire));
                            }
                            else
                            {
                                unitZoneViewCom.SetText_Button(UniqueButtonTypes.First, LanguageComCom.GetText(GameLanguageTypes.FireForest));
                            }
                            unitZoneViewCom.SetColor_Button(UniqueButtonTypes.First, new Color(1, 0.5f, 0.5f, 1));
                        }
                        else
                        {
                            unitZoneViewCom.SetText_Button(UniqueButtonTypes.First, LanguageComCom.GetText(GameLanguageTypes.SeedForest));
                            unitZoneViewCom.SetColor_Button(UniqueButtonTypes.First, new Color(0.5f, 1, 0.5f, 1));
                        }
                    }

                    else
                    {
                        activeFirst = true;
                        unitZoneViewCom.SetColor_Button(UniqueButtonTypes.First, new Color(1, 0.5f, 0.5f, 1));
                        unitZoneViewCom.SetText_Button(UniqueButtonTypes.First, LanguageComCom.GetText(GameLanguageTypes.FireForest));
                    }
                }
            }
        }

        unitZoneViewCom.SetActive_Button(UniqueButtonTypes.First, activeFirst);
        unitZoneViewCom.SetActive_Button(UniqueButtonTypes.Second, activeSecond);
    }


}
