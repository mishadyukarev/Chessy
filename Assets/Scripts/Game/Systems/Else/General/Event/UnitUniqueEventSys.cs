using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    internal sealed class UnitUniqueEventSys : IEcsInitSystem
    {
        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvironmentDataC> _cellEnvFilter = default;


        public void Init()
        {
            RightUniqueViewUIC.AddListener_Button(UniqueButtonTypes.First, delegate { ExecuteUniqueButton(UniqueButtonTypes.First); });
        }

        private void ExecuteUniqueButton(UniqueButtonTypes uniqueButtonType)
        {
            if (WhoseMoveC.IsMyMove/* IsMyOnlineMove || GameModesCom.IsOfflineMode*/)
            {
                if (uniqueButtonType == UniqueButtonTypes.First)
                {
                    var idxSelCell = SelectorC.IdxSelCell;

                    ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);
                    ref var selOnUnitCom = ref _cellUnitFilter.Get2(idxSelCell);

                    ref var selEnvDataCom = ref _cellEnvFilter.Get1(idxSelCell);

                    if (selUnitDatCom.HaveUnit)
                    {
                        if (selOnUnitCom.IsMine)
                        {
                            if (selUnitDatCom.Is(UnitTypes.King))
                            {
                                RpcSys.CircularAttackKingToMaster(idxSelCell);
                            }
                            else
                            {
                                if (selUnitDatCom.IsMelee)
                                {
                                    if (selEnvDataCom.Have(EnvirTypes.AdultForest))
                                    {
                                        RpcSys.FireToMaster(idxSelCell, idxSelCell);
                                    }
                                    else
                                    {
                                        RpcSys.SeedEnvironmentToMaster(idxSelCell, EnvirTypes.YoungForest);
                                    }
                                }

                                else
                                {
                                    SelectorC.CellClickType = CellClickTypes.PickFire;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}