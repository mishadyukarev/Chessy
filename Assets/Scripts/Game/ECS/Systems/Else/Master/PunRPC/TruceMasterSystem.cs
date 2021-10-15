using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class TruceMasterSystem : IEcsRunSystem
    {
        private EcsFilter<InventorUnitsComponent> _inventorUnitsFilter = default;
        private EcsFilter<InventorTWCom> _invTWFilt = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataComponent> _cellBuildFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;
        private EcsFilter<CellViewComponent> _cellViewFilt = default;

        public void Run()
        {
            ref var invUnitsCom = ref _inventorUnitsFilter.Get1(0);

            int random;


            _invTWFilt.Get1(0).SetAmountTW(PlayerTypes.First, ToolWeaponTypes.Pick, 0);
            _invTWFilt.Get1(0).SetAmountTW(PlayerTypes.Second, ToolWeaponTypes.Pick, 0);

            _invTWFilt.Get1(0).SetAmountTW(PlayerTypes.First, ToolWeaponTypes.Sword, 0);
            _invTWFilt.Get1(0).SetAmountTW(PlayerTypes.Second, ToolWeaponTypes.Sword, 0);

            _invTWFilt.Get1(0).SetAmountTW(PlayerTypes.First, ToolWeaponTypes.Shield, 0);
            _invTWFilt.Get1(0).SetAmountTW(PlayerTypes.Second, ToolWeaponTypes.Shield, 0);


            foreach (byte curIdxCell in _xyCellFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdxCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get2(curIdxCell);

                ref var curBuildDatCom = ref _cellBuildFilter.Get1(curIdxCell);
                ref var curEnvDatCom = ref _cellEnvFilter.Get1(curIdxCell);
                ref var curFireCom = ref _cellFireFilter.Get1(curIdxCell);


                curFireCom.DisableFire();


                if (_cellViewFilt.Get1(curIdxCell).IsActiveParent)
                {
                    if (curUnitDatCom.HaveUnit)
                    {
                        if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                        {
                            if (curOwnUnitCom.IsPlayerType(PlayerTypes.First))
                            {
                                invUnitsCom.AddUnitsInInventor(curOwnUnitCom.PlayerType, curUnitDatCom.UnitType);
                                curUnitDatCom.DefUnitType();
                            }
                        }
                        else
                        {
                            invUnitsCom.AddUnitsInInventor(curOwnUnitCom.PlayerType, curUnitDatCom.UnitType);
                            curUnitDatCom.DefUnitType();
                        }
                    }


                    if (curBuildDatCom.HaveBuild)
                    {

                    }

                    else
                    {
                        if (curEnvDatCom.HaveEnvir(EnvirTypes.YoungForest))
                        {
                            curEnvDatCom.ResetEnvironment(EnvirTypes.YoungForest);
                            curEnvDatCom.SetNewEnvir(EnvirTypes.AdultForest);
                        }

                        if (!curEnvDatCom.HaveEnvir(EnvirTypes.Fertilizer)
                            && !curEnvDatCom.HaveEnvir(EnvirTypes.Mountain)
                            && !curEnvDatCom.HaveEnvir(EnvirTypes.AdultForest))
                        {
                            random = Random.Range(0, 100);

                            if (random <= 3)
                            {
                                curEnvDatCom.SetNewEnvir(EnvirTypes.Fertilizer);
                            }
                            else
                            {
                                random = Random.Range(0, 100);

                                if (random <= 30)
                                {
                                    curEnvDatCom.SetNewEnvir(EnvirTypes.AdultForest);
                                }
                            }
                        }
                    }
                }
            }

            RpcSys.ActiveAmountMotionUIToGeneral(RpcTarget.All);
        }
    }
}