using Assets.Scripts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

internal sealed class TruceMasterSystem : IEcsRunSystem
{
    private EcsFilter<InventorUnitsComponent> _inventorUnitsFilter = default;
    private EcsFilter<MotionsDataUIComponent> _motionsUIFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;

    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;
    private EcsFilter<CellBuildDataComponent> _cellBuildFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
    private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

    public void Run()
    {
        ref var inventorUnitsCom = ref _inventorUnitsFilter.Get1(0);

        int random;

        foreach (byte curIdxCell in _xyCellFilter)
        {
            ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(curIdxCell);
            ref var curOwnerCellUnitDataCom = ref _cellUnitFilter.Get2(curIdxCell);

            ref var curBuildDataCom = ref _cellBuildFilter.Get1(curIdxCell);

            ref var curCellEnvDataCom = ref _cellEnvFilter.Get1(curIdxCell);

            ref var curCellFireCom = ref _cellFireFilter.Get1(curIdxCell);


            curCellFireCom.HaveFire = false;

            if (curCellUnitDataCom.HaveUnit)
            {
                if (curOwnerCellUnitDataCom.HaveOwner)
                {
                    inventorUnitsCom.AddUnitsInInventor(curCellUnitDataCom.UnitType, curOwnerCellUnitDataCom.IsMasterClient);

                    curCellUnitDataCom.ResetUnitType();
                }
            }


            if (curBuildDataCom.HaveBuild)
            {

            }

            else
            {
                if (curCellEnvDataCom.HaveEnvironment(EnvironmentTypes.YoungForest))
                {
                    curCellEnvDataCom.ResetEnvironment(EnvironmentTypes.YoungForest);
                    curCellEnvDataCom.SetNewEnvironment(EnvironmentTypes.AdultForest);
                }

                if (!curCellEnvDataCom.HaveEnvironment(EnvironmentTypes.Fertilizer)
                    && !curCellEnvDataCom.HaveEnvironment(EnvironmentTypes.Mountain)
                    && !curCellEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    random = Random.Range(0, 100);

                    if (random <= 20)
                    {
                        curCellEnvDataCom.SetNewEnvironment(EnvironmentTypes.Fertilizer);
                    }
                    else
                    {
                        random = Random.Range(0, 100);

                        if (random <= 5)
                        {
                            curCellEnvDataCom.SetNewEnvironment(EnvironmentTypes.AdultForest);
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