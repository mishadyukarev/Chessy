using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Supports;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.Event
{
    internal sealed class EnventRightBuildZoneSys : IEcsInitSystem
    {
        private EcsFilter<SelectorComponent> _selectorFilter = default;
        private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;
        private EcsFilter<UnitZoneViewUICom> _unitZoneUIFilter = default;

        private EcsFilter<CellUnitDataComponent, OwnerOnlineComp, OwnerBotComponent> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataComponent, OwnerOnlineComp, OwnerBotComponent> _cellBuildFilter = default;

        public void Init()
        {
            _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.First, delegate { ExecuteButton(BuildingButtonTypes.First); });
            _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.Second, delegate { ExecuteButton(BuildingButtonTypes.Second); });
            _unitZoneUIFilter.Get1(0).AddListenerToBuildButton(BuildingButtonTypes.Third, delegate { ExecuteButton(BuildingButtonTypes.Third); });
        }

        private void ExecuteButton(BuildingButtonTypes buildButtonType)
        {
            if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient))
            {
                var idxSelCell = _selectorFilter.Get1(0).IdxSelCell;

                if (buildButtonType == BuildingButtonTypes.First)
                {
                    RpcSys.BuildToMaster(idxSelCell, BuildingTypes.Farm);
                }

                else if (buildButtonType == BuildingButtonTypes.Second)
                {
                    RpcSys.BuildToMaster(idxSelCell, BuildingTypes.Mine);
                }

                else
                {
                    ref var selUnitDataCom = ref _cellUnitFilter.Get1(idxSelCell);
                    ref var selOwnerUnitCom = ref _cellUnitFilter.Get2(idxSelCell);
                    ref var selBotUnitCom = ref _cellUnitFilter.Get3(idxSelCell);

                    ref var selBuildDataCom = ref _cellBuildFilter.Get1(idxSelCell);
                    ref var selOwnerBuildCom = ref _cellBuildFilter.Get2(idxSelCell);
                    ref var selBotBuildCom = ref _cellBuildFilter.Get3(idxSelCell);


                    if (_selectorFilter.Get1(0).IsSelectedCell)

                        if (selUnitDataCom.HaveUnit)

                            if (selOwnerUnitCom.HaveOwner)

                                if (selOwnerUnitCom.IsMine)

                                    if (selUnitDataCom.IsUnitType(new[] { UnitTypes.Pawn }))

                                        if (selBuildDataCom.HaveBuild)
                                        {
                                            if (selOwnerBuildCom.HaveOwner)
                                            {
                                                if (selOwnerBuildCom.IsMine)
                                                {
                                                    if (!selBuildDataCom.IsBuildType(BuildingTypes.City))

                                                        RpcSys.DestroyBuildingToMaster(idxSelCell);

                                                }

                                                else
                                                {
                                                    RpcSys.DestroyBuildingToMaster(idxSelCell);
                                                }
                                            }

                                            else if (selBotBuildCom.IsBot)
                                            {
                                                if (selBuildDataCom.IsBuildType(BuildingTypes.City))
                                                {
                                                    RpcSys.DestroyBuildingToMaster(idxSelCell);
                                                }
                                            }
                                        }

                                        else
                                        {
                                            if (!_cellBuildFilter.IsSettedCity(PhotonNetwork.IsMasterClient))
                                            {
                                                RpcSys.BuildToMaster(idxSelCell, BuildingTypes.City);
                                            }
                                        }




                }
            }
        }
    }
}

