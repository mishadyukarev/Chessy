using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

internal sealed class GetterUnitsUISystem : IEcsRunSystem
{
    private EcsFilter<UnitsInGameInfoComponent> _xyUnitsFilter = default;
    private EcsFilter<GetterUnitsDataUICom, GetterUnitsViewUICom> _takerUnitsUIFilter = default;

    private EcsFilter<InventorUnitsComponent> _inventUnitsFilter = default;

    private const float NEEDED_TIME = 1;

    public void Run()
    {
        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);
        ref var getterUnitsDataUICom = ref _takerUnitsUIFilter.Get1(0);
        ref var getterUnitsViewUICom = ref _takerUnitsUIFilter.Get2(0);
        ref var inventUnitsComp = ref _inventUnitsFilter.Get1(0);

        for (UnitTypes curUnitType = 0; curUnitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; curUnitType++)
        {
            if (curUnitType == UnitTypes.Pawn_Axe || curUnitType == UnitTypes.Rook_Bow || curUnitType == UnitTypes.Bishop_Bow)
            {
                if (getterUnitsDataUICom.IsActivatedButton(curUnitType))
                {
                    getterUnitsViewUICom.SetActiveCreateButton(curUnitType, true);
                    getterUnitsDataUICom.AddTimer(curUnitType, Time.deltaTime);

                    if (getterUnitsDataUICom.GetTimer(curUnitType) >= NEEDED_TIME)
                    {
                        getterUnitsViewUICom.SetActiveCreateButton(curUnitType, false);
                        getterUnitsDataUICom.ActiveNeedCreateButton(curUnitType, false);
                        getterUnitsDataUICom.ResetCurTimer(curUnitType);
                    }
                }

                else
                {
                    getterUnitsViewUICom.SetActiveCreateButton(curUnitType, false);
                }
            }
        }

        getterUnitsViewUICom.SetTextToAmountUnits(UnitTypes.Pawn_Axe, inventUnitsComp.AmountUnitsInInventor(UnitTypes.Pawn_Axe, PhotonNetwork.IsMasterClient).ToString());
        getterUnitsViewUICom.SetTextToAmountUnits(UnitTypes.Rook_Bow, inventUnitsComp.AmountUnitsInInventor(UnitTypes.Rook_Bow, PhotonNetwork.IsMasterClient).ToString());
        getterUnitsViewUICom.SetTextToAmountUnits(UnitTypes.Bishop_Bow, inventUnitsComp.AmountUnitsInInventor(UnitTypes.Bishop_Bow, PhotonNetwork.IsMasterClient).ToString());

        if (xyUnitsCom.IsSettedKing(PhotonNetwork.IsMasterClient))
            getterUnitsViewUICom.SetActiveButton(UnitTypes.King, false);
        else getterUnitsViewUICom.SetActiveButton(UnitTypes.King, true);
    }
}
