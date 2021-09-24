using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.View.UI.Game.General;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.General.Event
{
    internal sealed class EventUnitBuildUISys : IEcsInitSystem
    {
        private EcsFilter<SelectorCom> _selFilt = default;
        private EcsFilter<BuildAbilitUICom> _buildAbilUIFilt = default;

        private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellBuildDataComponent, OwnerCom> _cellBuildFilter = default;

        public void Init()
        {
            _buildAbilUIFilt.Get1(0).AddListener_Button(BuildButtonTypes.First, delegate { ExecuteButton(BuildButtonTypes.First); });
            _buildAbilUIFilt.Get1(0).AddListener_Button(BuildButtonTypes.Second, delegate { ExecuteButton(BuildButtonTypes.Second); });
            _buildAbilUIFilt.Get1(0).AddListener_Button(BuildButtonTypes.Third, delegate { ExecuteButton(BuildButtonTypes.Third); });
        }

        private void ExecuteButton(BuildButtonTypes buildButtonType)
        {
            if (WhoseMoveCom.IsMyOnlineMove || GameModesCom.IsOfflineMode)
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

                    if (selBuildDataCom.HaveBuild)
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

