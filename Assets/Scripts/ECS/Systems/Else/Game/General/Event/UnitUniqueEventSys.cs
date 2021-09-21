using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Components.View.UI.Game.General.Right;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.Event
{
    internal sealed class UnitUniqueEventSys : IEcsInitSystem
    {
        private EcsFilter<SelectorCom> _selectorFilter = default;
        private EcsFilter<UniqueAbiltUICom> _uniqueAbilUIFilt = default;
        private EcsFilter<DonerDataUIComponent> _donerUIFilt = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;


        public void Init()
        {
            _uniqueAbilUIFilt.Get1(0).AddListener_Button(UniqueButtonTypes.First, delegate { ExecuteUniqueButton(UniqueButtonTypes.First); });
        }

        private void ExecuteUniqueButton(UniqueButtonTypes uniqueButtonType)
        {

            if (uniqueButtonType == UniqueButtonTypes.First)
            {
                var idxSelCell = _selectorFilter.Get1(0).IdxSelCell;

                ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);
                ref var selOnUnitCom = ref _cellUnitFilter.Get2(idxSelCell);

                ref var selEnvDataCom = ref _cellEnvFilter.Get1(idxSelCell);

                if (selUnitDatCom.HaveUnit)
                {
                    if (selOnUnitCom.IsMine)
                    {
                        if (selUnitDatCom.IsUnit(UnitTypes.King))
                        {
                            RpcSys.CircularAttackKingToMaster(idxSelCell);
                        }
                        else
                        {
                            if (selUnitDatCom.IsMelee)
                            {
                                if (selEnvDataCom.HaveEnvir(EnvirTypes.AdultForest))
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
                                _selectorFilter.Get1(0).CellClickType = CellClickTypes.PickFire;
                            }
                        }
                    }
                }
            }
        }
    }
}