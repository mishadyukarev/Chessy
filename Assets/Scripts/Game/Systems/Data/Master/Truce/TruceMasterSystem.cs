using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using UnityEngine;

namespace Scripts.Game
{
    public sealed class TruceMasterSystem : IEcsRunSystem
    {
        private EcsFilter<InventorUnitsC> _inventorUnitsFilter = default;
        private EcsFilter<InventorTWCom> _invTWFilt = default;

        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellBuildDataC> _cellBuildFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilter = default;
        private EcsFilter<CellFireDataC> _cellFireFilter = default;
        private EcsFilter<CellDataC> _cellDataFilt = default;

        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitFilter = default;

        public void Run()
        {
            ref var invUnitsCom = ref _inventorUnitsFilter.Get1(0);

            int random;

            foreach (byte idx_0 in _xyCellFilter)
            {
                ref var unitC_0 = ref _cellUnitFilter.Get1(idx_0);
                ref var levUnitC_0 = ref _cellUnitFilter.Get2(idx_0);
                ref var ownUnitC_0 = ref _cellUnitFilter.Get3(idx_0);

                ref var curBuildDatCom = ref _cellBuildFilter.Get1(idx_0);
                ref var curEnvDatCom = ref _cellEnvFilter.Get1(idx_0);
                ref var curFireCom = ref _cellFireFilter.Get1(idx_0);


                curFireCom.DisableFire();


                if (_cellDataFilt.Get1(idx_0).IsActiveCell)
                {
                    if (unitC_0.HaveUnit)
                    {
                        if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                        {
                            if (ownUnitC_0.Is(PlayerTypes.First))
                            {
                                InventorUnitsC.AddUnit(ownUnitC_0.Owner, unitC_0.Unit, levUnitC_0.Level);
                                unitC_0.NoneUnit();
                            }
                        }
                        else
                        {
                            InventorUnitsC.AddUnit(ownUnitC_0.Owner, unitC_0.Unit, levUnitC_0.Level);
                            unitC_0.NoneUnit();
                        }
                    }


                    if (curBuildDatCom.HaveBuild)
                    {

                    }

                    else
                    {
                        if (curEnvDatCom.Have(EnvTypes.YoungForest))
                        {
                            curEnvDatCom.Reset(EnvTypes.YoungForest);
                            WhereEnvC.Remove(EnvTypes.YoungForest, idx_0);

                            curEnvDatCom.SetNew(EnvTypes.AdultForest);
                            WhereEnvC.Add(EnvTypes.AdultForest, idx_0);
                        }

                        if (!curEnvDatCom.Have(EnvTypes.Fertilizer)
                            && !curEnvDatCom.Have(EnvTypes.Mountain)
                            && !curEnvDatCom.Have(EnvTypes.AdultForest))
                        {
                            random = Random.Range(0, 100);

                            if (random <= 3)
                            {
                                curEnvDatCom.SetNew(EnvTypes.Fertilizer);
                                WhereEnvC.Add(EnvTypes.Fertilizer, idx_0);
                            }
                        }
                    }
                }
            }

            RpcSys.ActiveAmountMotionUIToGeneral(RpcTarget.All);
        }
    }
}