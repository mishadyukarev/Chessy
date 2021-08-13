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
    private EcsFilter<TakerUnitsDataUICom, TakerUnitsViewUICom> _takerUnitsUIFilter = default;


    private const float NEEDED_TIME = 1;

    public void Run()
    {
        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);
        ref var takerUnitsDataUICom = ref _takerUnitsUIFilter.Get1(0);
        ref var takerUnitsViewUICom = ref _takerUnitsUIFilter.Get2(0);

        for (UnitTypes curUnitType = 0; curUnitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; curUnitType++)
        {
            if (curUnitType == UnitTypes.Pawn_Axe || curUnitType == UnitTypes.Rook_Bow || curUnitType == UnitTypes.Bishop_Bow)
            {
                if (takerUnitsDataUICom.IsActivatedButton(curUnitType))
                {
                    takerUnitsViewUICom.SetActiveCreateButton(curUnitType, true);
                    takerUnitsDataUICom.AddTimer(curUnitType, Time.deltaTime);

                    if (takerUnitsDataUICom.GetTimer(curUnitType) >= NEEDED_TIME)
                    {
                        takerUnitsViewUICom.SetActiveCreateButton(curUnitType, false);
                        takerUnitsDataUICom.ActiveNeedCreateButton(curUnitType, false);
                        takerUnitsDataUICom.ResetCurTimer(curUnitType);
                    }
                }

                else
                {
                    takerUnitsViewUICom.SetActiveCreateButton(curUnitType, false);
                }
            }
        }


        if (xyUnitsCom.IsSettedKing(PhotonNetwork.IsMasterClient))
            takerUnitsViewUICom.SetActiveButton(UnitTypes.King, false);
        else takerUnitsViewUICom.SetActiveButton(UnitTypes.King, true);
    }
}
