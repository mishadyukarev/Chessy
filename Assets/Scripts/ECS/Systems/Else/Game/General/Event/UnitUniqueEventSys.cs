using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.View.UI.Game.General.Right;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.Event
{
    internal sealed class UnitUniqueEventSys : IEcsInitSystem
    {
        private EcsFilter<SelectorCom> _selectorFilter = default;
        private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;
        private EcsFilter<UniqueAbiltUICom> _uniqueAbilUIFilt = default;

        private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter = default;
        private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
        private EcsFilter<CellFireDataComponent> _cellFireFilter = default;


        public void Init()
        {
            _uniqueAbilUIFilt.Get1(0).AddListener_Button(UniqueButtonTypes.First, delegate { ExecuteUniqueButton(UniqueButtonTypes.First); });
        }

        private void ExecuteUniqueButton(UniqueButtonTypes uniqueButtonType)
        {
            if (uniqueButtonType == UniqueButtonTypes.First)
            {
                var idxSelCell = _selectorFilter.Get1(0).IdxSelCell;

                ref var unitZoneViewCom = ref _uniqueAbilUIFilt.Get1(0);

                ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);
                ref var selOnUnitCom = ref _cellUnitFilter.Get2(idxSelCell);
                ref var selOffUnitCom = ref _cellUnitFilter.Get3(idxSelCell);
                ref var selBotUnitCom = ref _cellUnitFilter.Get4(idxSelCell);

                ref var selEnvDataCom = ref _cellEnvFilter.Get1(idxSelCell);
                ref var selFireDatCom = ref _cellFireFilter.Get1(idxSelCell);



                if (selUnitDatCom.HaveUnit)
                {
                    var canCome = false;

                    if (selOnUnitCom.HaveOwner)
                    {
                        if (selOnUnitCom.IsMine)
                        {
                            canCome = true;
                        }
                    }
                    else if (selOffUnitCom.HaveLocalPlayer)
                    {
                        if (selOffUnitCom.IsMine)
                        {
                            canCome = true;
                        }
                    }

                    if (canCome)
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