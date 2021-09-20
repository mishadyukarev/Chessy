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
        private EcsFilter<CellBuildDataComponent, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellBuildFilter = default;

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
                    ref var selBuildDataCom = ref _cellBuildFilter.Get1(idxSelCell);

                    if (selBuildDataCom.IsBuildType(BuildingTypes.City))
                    {
                        RpcSys.DestroyBuildingToMaster(idxSelCell);
                    }
                    else
                    {
                        RpcSys.BuildToMaster(idxSelCell, BuildingTypes.City);
                    }
                }
            }
        }
    }
}

