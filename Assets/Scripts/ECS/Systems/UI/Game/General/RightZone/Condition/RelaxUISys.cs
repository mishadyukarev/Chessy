using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.View.UI.Game.General.Right;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using UnityEngine;

internal sealed class RelaxUISys : IEcsRunSystem
{
    private EcsFilter<CondUnitUICom> _condUIFilt = default;
    private EcsFilter<SelectorCom> _selectorFilter = default;

    private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter = default;


    public void Run()
    {
        var idxSelCell = _selectorFilter.Get1(0).IdxSelCell;
        ref var condUnitUICom = ref _condUIFilt.Get1(0);

        ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);
        ref var selOnUnitCom = ref _cellUnitFilter.Get2(idxSelCell);
        ref var selOffUnitCom = ref _cellUnitFilter.Get3(idxSelCell);
        ref var selBotUnitCom = ref _cellUnitFilter.Get4(idxSelCell);


        condUnitUICom.SetText_Info(LanguageComComp.GetText(GameLanguageTypes.ConditAbilities));


        var activeButt = false;

        if (selUnitDatCom.HaveUnit)
        {
            if (selUnitDatCom.IsUnit(UnitTypes.King))
            {
                condUnitUICom.SetText_Button(CondUnitTypes.Relaxed, LanguageComComp.GetText(GameLanguageTypes.Relax));
            }

            else if (selUnitDatCom.IsUnit(UnitTypes.Pawn))
            {
                if (selUnitDatCom.HaveMaxAmountHealth)
                {
                    condUnitUICom.SetText_Button(CondUnitTypes.Relaxed, LanguageComComp.GetText(GameLanguageTypes.Extract));
                }
                else
                {
                    condUnitUICom.SetText_Button(CondUnitTypes.Relaxed, LanguageComComp.GetText(GameLanguageTypes.Relax));
                }
            }

            else
            {
                condUnitUICom.SetText_Button(CondUnitTypes.Relaxed, LanguageComComp.GetText(GameLanguageTypes.Relax));
            }

            if (selOnUnitCom.HaveOwner)
            {
                if (selOnUnitCom.IsMine)
                {
                    activeButt = true;

                    if (selUnitDatCom.IsCondType(CondUnitTypes.Protected))
                    {
                        condUnitUICom.SetColor(CondUnitTypes.Protected, Color.yellow);
                    }

                    else
                    {
                        condUnitUICom.SetColor(CondUnitTypes.Protected, Color.white);
                    }

                    if (selUnitDatCom.IsCondType(CondUnitTypes.Relaxed))
                    {
                        condUnitUICom.SetColor(CondUnitTypes.Relaxed, Color.green);
                    }
                    else
                    {
                        condUnitUICom.SetColor(CondUnitTypes.Relaxed, Color.white);
                    }
                }
            }

            else if (selOffUnitCom.HaveLocalPlayer)
            {
                if (selOffUnitCom.IsMine)
                {
                    activeButt = true;

                    if (selUnitDatCom.IsCondType(CondUnitTypes.Protected))
                    {
                        condUnitUICom.SetColor(CondUnitTypes.Protected, Color.yellow);
                    }

                    else
                    {
                        condUnitUICom.SetColor(CondUnitTypes.Protected, Color.white);
                    }

                    if (selUnitDatCom.IsCondType(CondUnitTypes.Relaxed))
                    {
                        condUnitUICom.SetColor(CondUnitTypes.Relaxed, Color.green);
                    }
                    else
                    {
                        condUnitUICom.SetColor(CondUnitTypes.Relaxed, Color.white);
                    }
                }
            }
        }


        condUnitUICom.SetActive(CondUnitTypes.Relaxed, activeButt);
    }
}
