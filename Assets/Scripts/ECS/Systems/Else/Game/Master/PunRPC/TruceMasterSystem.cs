using Assets.Scripts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

internal sealed class TruceMasterSystem : IEcsRunSystem
{
    private EcsFilter<InventorUnitsComponent> _inventorUnitsFilter = default;
    private EcsFilter<InventorWeaponsComp> _inventorWeapFilter = default;
    private EcsFilter<MotionsDataUIComponent> _motionsUIFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;

    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom> _cellUnitFilter = default;
    private EcsFilter<CellBuildDataComponent> _cellBuildFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
    private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

    public void Run()
    {
        ref var invUnitsCom = ref _inventorUnitsFilter.Get1(0);

        int random;

        foreach (byte curIdxCell in _xyCellFilter)
        {
            ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
            ref var curOnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);
            ref var curOffUnitCom = ref _cellUnitFilter.Get3(curIdxCell);

            ref var curBuildDataCom = ref _cellBuildFilter.Get1(curIdxCell);

            ref var curCellEnvDataCom = ref _cellEnvFilter.Get1(curIdxCell);

            ref var curCellFireCom = ref _cellFireFilter.Get1(curIdxCell);


            curCellFireCom.HaveFire = false;

            if (curUnitDatCom.HaveUnit)
            {
                if (curOnUnitCom.HaveOwner)
                {
                    invUnitsCom.AddUnitsInInventor(curUnitDatCom.UnitType, curOnUnitCom.IsMasterClient);

                    //if (curUnitDatCom.HaveArcherWeapon)
                    //{
                    //    _inventorWeapFilter.Get1(0).AddAmountWeapons(curOwnUnitDatCom.IsMasterClient, curUnitDatCom.ArcherWeaponType);
                    //}

                    //else if (curUnitDatCom.HaveExtraToolWeaponPawn)
                    //{
                    //    if(curUnitDatCom.ExtraTWPawnType != ToolWeaponTypes.Axe)
                    //    {
                    //        _inventorWeapFilter.Get1(0).AddAmountWeapons(curOwnUnitDatCom.IsMasterClient, curUnitDatCom.ExtraTWPawnType);
                    //    }
                    //}


                    curUnitDatCom.ResetUnitType();
                }

                else if (curOffUnitCom.HaveLocalPlayer)
                {
                    invUnitsCom.AddUnitsInInventor(curUnitDatCom.UnitType, curOffUnitCom.IsMainMaster);

                    curUnitDatCom.ResetUnitType();
                }
            }


            if (curBuildDataCom.HaveBuild)
            {

            }

            else
            {
                if (curCellEnvDataCom.HaveEnvir(EnvirTypes.YoungForest))
                {
                    curCellEnvDataCom.ResetEnvironment(EnvirTypes.YoungForest);
                    curCellEnvDataCom.SetNewEnvir(EnvirTypes.AdultForest);
                }

                if (!curCellEnvDataCom.HaveEnvir(EnvirTypes.Fertilizer)
                    && !curCellEnvDataCom.HaveEnvir(EnvirTypes.Mountain)
                    && !curCellEnvDataCom.HaveEnvir(EnvirTypes.AdultForest))
                {
                    random = Random.Range(0, 100);

                    if (random <= 20)
                    {
                        curCellEnvDataCom.SetNewEnvir(EnvirTypes.Fertilizer);
                    }
                    else
                    {
                        random = Random.Range(0, 100);

                        if (random <= 10)
                        {
                            curCellEnvDataCom.SetNewEnvir(EnvirTypes.AdultForest);
                        }
                    }
                }
            }
        }

        //if (unitsInGameCom.GetAmountUnitsInGame(UnitTypes.Pawn, true) <= 0
        //    && unitsInGameCom.GetAmountUnitsInGame(UnitTypes.Pawn, true) <= 0)
        //{
        //    inventorUnitsCom.AddUnitsInInventor(UnitTypes.Pawn, true);
        //}

        //if (unitsInGameCom.GetAmountUnitsInGame(UnitTypes.Pawn, false) <= 0
        //    && unitsInGameCom.GetAmountUnitsInGame(UnitTypes.Pawn, false) <= 0)
        //{
        //    inventorUnitsCom.AddUnitsInInventor(UnitTypes.Pawn, false);
        //}

        RpcSys.ActiveAmountMotionUIToGeneral(RpcTarget.All);

        _donerUIFilter.Get1(0).SetDoned(true, default);
        _donerUIFilter.Get1(0).SetDoned(false, default);
    }
}