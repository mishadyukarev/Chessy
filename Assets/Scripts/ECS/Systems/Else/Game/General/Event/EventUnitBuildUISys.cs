using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Assets.Scripts.Supports;
using Leopotam.Ecs;
using Photon.Pun;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.Event
{
    internal sealed class EventUnitBuildUISys : IEcsInitSystem
    {
        private EcsFilter<SelectorCom> _selFilt = default;
        private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;
        private EcsFilter<BuildAbilitUICom> _buildAbilUIFilt = default;

        private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataComponent, OwnerOnlineComp, OwnerBotComponent> _cellBuildFilter = default;

        public void Init()
        {
            _buildAbilUIFilt.Get1(0).AddListener_Button(BuildButtonTypes.First, delegate { ExecuteButton(BuildButtonTypes.First); });
            _buildAbilUIFilt.Get1(0).AddListener_Button(BuildButtonTypes.Second, delegate { ExecuteButton(BuildButtonTypes.Second); });
            _buildAbilUIFilt.Get1(0).AddListener_Button(BuildButtonTypes.Third, delegate { ExecuteButton(BuildButtonTypes.Third); });
        }

        private void ExecuteButton(BuildButtonTypes buildButtonType)
        {
            if (!_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient))
            {
                var idxSelCell = _selFilt.Get1(0).IdxSelCell;

                if (buildButtonType == BuildButtonTypes.First)
                {
                    RpcSys.BuildToMaster(idxSelCell, BuildingTypes.Farm);
                }

                else if (buildButtonType == BuildButtonTypes.Second)
                {
                    RpcSys.BuildToMaster(idxSelCell, BuildingTypes.Mine);
                }

                else
                {
                    ref var selUnitDataCom = ref _cellUnitFilter.Get1(idxSelCell);
                    ref var selOnUnitCom = ref _cellUnitFilter.Get2(idxSelCell);
                    ref var selOffUnitCom = ref _cellUnitFilter.Get3(idxSelCell);
                    ref var selBotUnitCom = ref _cellUnitFilter.Get4(idxSelCell);

                    ref var selBuildDataCom = ref _cellBuildFilter.Get1(idxSelCell);
                    ref var selOnBuildCom = ref _cellBuildFilter.Get2(idxSelCell);
                    ref var selBotBuildCom = ref _cellBuildFilter.Get3(idxSelCell);


                    if (_selFilt.Get1(0).IsSelectedCell)

                        if (selUnitDataCom.HaveUnit)
                        {
                            var canCome = false;

                            if (selOnUnitCom.HaveOwner)
                            {
                                if (selOnUnitCom.IsMine) canCome = true;
                            }
                            else if (selOffUnitCom.HaveLocPlayer)
                            {
                                if (selOffUnitCom.IsMine) canCome = true;
                            }


                            if (canCome)

                                if (selUnitDataCom.Is(new[] { UnitTypes.Pawn }))

                                    if (selBuildDataCom.HaveBuild)
                                    {
                                        if (selOnBuildCom.HaveOwner)
                                        {
                                            if (selOnBuildCom.IsMine)
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
}

