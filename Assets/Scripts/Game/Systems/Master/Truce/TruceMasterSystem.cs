using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class TruceMasterSystem : IEcsRunSystem
    {
        private EcsFilter<InventorUnitsC> _inventorUnitsFilter = default;
        private EcsFilter<InventorTWCom> _invTWFilt = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataCom> _cellBuildFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;
        private EcsFilter<CellDataC> _cellDataFilt = default;

        public void Run()
        {
            ref var invUnitsCom = ref _inventorUnitsFilter.Get1(0);

            int random;


            //_invTWFilt.Get1(0).SetAmountTW(PlayerTypes.First, ToolWeaponTypes.Pick, 0);
            //_invTWFilt.Get1(0).SetAmountTW(PlayerTypes.Second, ToolWeaponTypes.Pick, 0);

            //_invTWFilt.Get1(0).SetAmountTW(PlayerTypes.First, ToolWeaponTypes.Sword, 0);
            //_invTWFilt.Get1(0).SetAmountTW(PlayerTypes.Second, ToolWeaponTypes.Sword, 0);

            //_invTWFilt.Get1(0).SetAmountTW(PlayerTypes.First, ToolWeaponTypes.Shield, 0);
            //_invTWFilt.Get1(0).SetAmountTW(PlayerTypes.Second, ToolWeaponTypes.Shield, 0);


            foreach (byte curIdxCell in _xyCellFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);

                ref var curBuildDatCom = ref _cellBuildFilter.Get1(curIdxCell);
                ref var curEnvDatCom = ref _cellEnvFilter.Get1(curIdxCell);
                ref var curFireCom = ref _cellFireFilter.Get1(curIdxCell);


                curFireCom.DisableFire();


                if (_cellDataFilt.Get1(curIdxCell).IsActiveCell)
                {
                    if (curUnitDatCom.HaveUnit)
                    {
                        if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                        {
                            if (curOwnUnitCom.Is(PlayerTypes.First))
                            {
                                InventorUnitsC.AddUnitsInInventor(curOwnUnitCom.PlayerType, curUnitDatCom.UnitType, curUnitDatCom.LevelUnitType);
                                curUnitDatCom.DefUnitType();
                            }
                        }
                        else
                        {
                            InventorUnitsC.AddUnitsInInventor(curOwnUnitCom.PlayerType, curUnitDatCom.UnitType, curUnitDatCom.LevelUnitType);
                            curUnitDatCom.DefUnitType();
                        }
                    }


                    if (curBuildDatCom.HaveBuild)
                    {

                    }

                    else
                    {
                        if (curEnvDatCom.Have(EnvirTypes.YoungForest))
                        {
                            curEnvDatCom.Reset(EnvirTypes.YoungForest);
                            WhereEnvironmentC.Remove(EnvirTypes.YoungForest, curIdxCell);

                            curEnvDatCom.SetNew(EnvirTypes.AdultForest);
                            WhereEnvironmentC.Add(EnvirTypes.AdultForest, curIdxCell);
                        }

                        if (!curEnvDatCom.Have(EnvirTypes.Fertilizer)
                            && !curEnvDatCom.Have(EnvirTypes.Mountain)
                            && !curEnvDatCom.Have(EnvirTypes.AdultForest))
                        {
                            random = Random.Range(0, 100);

                            if (random <= 3)
                            {
                                curEnvDatCom.SetNew(EnvirTypes.Fertilizer);
                                WhereEnvironmentC.Add(EnvirTypes.Fertilizer, curIdxCell);
                            }
                        }
                    }
                }
            }

            RpcSys.ActiveAmountMotionUIToGeneral(RpcTarget.All);
        }
    }
}