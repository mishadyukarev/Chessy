using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;
using Photon.Pun;
using System;
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
            if (curUnitType == UnitTypes.Pawn || curUnitType == UnitTypes.Rook || curUnitType == UnitTypes.Bishop)
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

        getterUnitsViewUICom.SetTextToAmountUnits(UnitTypes.Pawn, inventUnitsComp.AmountUnitsInInventor(UnitTypes.Pawn, PhotonNetwork.IsMasterClient).ToString());
        getterUnitsViewUICom.SetTextToAmountUnits(UnitTypes.Rook, inventUnitsComp.AmountUnitsInInventor(UnitTypes.Rook, PhotonNetwork.IsMasterClient).ToString());
        getterUnitsViewUICom.SetTextToAmountUnits(UnitTypes.Bishop, inventUnitsComp.AmountUnitsInInventor(UnitTypes.Bishop, PhotonNetwork.IsMasterClient).ToString());

        //if (xyUnitsCom.IsSettedKing(PhotonNetwork.IsMasterClient))
        //    getterUnitsViewUICom.SetActiveButton(UnitTypes.King, false);
        //else getterUnitsViewUICom.SetActiveButton(UnitTypes.King, true);
    }
}
