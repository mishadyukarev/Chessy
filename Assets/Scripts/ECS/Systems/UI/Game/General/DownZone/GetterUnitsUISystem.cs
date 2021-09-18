using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Leopotam.Ecs;
using Photon.Pun;
using System;
using UnityEngine;

internal sealed class GetterUnitsUISystem : IEcsRunSystem
{
    private EcsFilter<GetterUnitsDataUICom, GetterUnitsViewUICom> _takerUnitsUIFilter = default;

    private EcsFilter<InventorUnitsComponent> _inventUnitsFilter = default;
    private EcsFilter<WhoseMoveCom> _whoseMoveFilter = default;

    private const float NEEDED_TIME = 1;

    public void Run()
    {
        ref var getUnitDatCom = ref _takerUnitsUIFilter.Get1(0);
        ref var getUnitViewCom = ref _takerUnitsUIFilter.Get2(0);
        ref var invUnitCom = ref _inventUnitsFilter.Get1(0);



        getUnitViewCom.SetTextUnit(UnitTypes.Pawn, LanguageComComp.GetText(GameLanguageTypes.Pawn));
        getUnitViewCom.SetTextUnit(UnitTypes.Rook, LanguageComComp.GetText(GameLanguageTypes.Rook));
        getUnitViewCom.SetTextUnit(UnitTypes.Bishop, LanguageComComp.GetText(GameLanguageTypes.Bishop));

        for (UnitTypes curUnitType = 0; curUnitType < (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length; curUnitType++)
        {
            if (curUnitType == UnitTypes.Pawn || curUnitType == UnitTypes.Rook || curUnitType == UnitTypes.Bishop)
            {
                getUnitViewCom.SetTextCreate(curUnitType, LanguageComComp.GetText(GameLanguageTypes.Create));


                if (getUnitDatCom.IsActivatedButton(curUnitType))
                {
                    getUnitViewCom.SetActiveCreateButton(curUnitType, true);
                    getUnitDatCom.AddTimer(curUnitType, Time.deltaTime);

                    if (getUnitDatCom.GetTimer(curUnitType) >= NEEDED_TIME)
                    {
                        getUnitViewCom.SetActiveCreateButton(curUnitType, false);
                        getUnitDatCom.ActiveNeedCreateButton(curUnitType, false);
                        getUnitDatCom.ResetCurTimer(curUnitType);
                    }
                }

                else
                {
                    getUnitViewCom.SetActiveCreateButton(curUnitType, false);
                }
            }
        }

        if (PhotonNetwork.OfflineMode)
        {
            var isMainMove = WhoseMoveCom.IsMainMove;

            getUnitViewCom.SetTextToAmountUnits(UnitTypes.Pawn, invUnitCom.AmountUnitsInInv(UnitTypes.Pawn, isMainMove).ToString());
            getUnitViewCom.SetTextToAmountUnits(UnitTypes.Rook, invUnitCom.AmountUnitsInInv(UnitTypes.Rook, isMainMove).ToString());
            getUnitViewCom.SetTextToAmountUnits(UnitTypes.Bishop, invUnitCom.AmountUnitsInInv(UnitTypes.Bishop, isMainMove).ToString());
        }

        else
        {
            getUnitViewCom.SetTextToAmountUnits(UnitTypes.Pawn, invUnitCom.AmountUnitsInInv(UnitTypes.Pawn, PhotonNetwork.IsMasterClient).ToString());
            getUnitViewCom.SetTextToAmountUnits(UnitTypes.Rook, invUnitCom.AmountUnitsInInv(UnitTypes.Rook, PhotonNetwork.IsMasterClient).ToString());
            getUnitViewCom.SetTextToAmountUnits(UnitTypes.Bishop, invUnitCom.AmountUnitsInInv(UnitTypes.Bishop, PhotonNetwork.IsMasterClient).ToString());
        }


    }
}
