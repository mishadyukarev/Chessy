using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using System;

namespace Scripts.Game
{
    internal sealed class EventUnitBuildUISys : IEcsInitSystem
    {
        private EcsFilter<CellBuildDataCom, OwnerCom> _cellBuildFilter = default;


        public void Init()
        {
            BuildAbilitUIC.AddListener_Button(BuildButtonTypes.First, delegate { ExecuteButton(BuildButtonTypes.First); });
            BuildAbilitUIC.AddListener_Button(BuildButtonTypes.Second, delegate { ExecuteButton(BuildButtonTypes.Second); });
            BuildAbilitUIC.AddListener_Button(BuildButtonTypes.Third, delegate { ExecuteButton(BuildButtonTypes.Third); });
        }

        private void ExecuteButton(BuildButtonTypes buildButtonType)
        {
            if (WhoseMoveC.IsMyMove)
            {
                switch (buildButtonType)
                {
                    case BuildButtonTypes.None:
                        throw new Exception();

                    case BuildButtonTypes.First:
                        RpcSys.BuildToMaster(SelectorC.IdxSelCell, BuildingTypes.Farm);
                        break;

                    case BuildButtonTypes.Second:
                        RpcSys.BuildToMaster(SelectorC.IdxSelCell, BuildingTypes.Mine);
                        break;

                    case BuildButtonTypes.Third:
                        {
                            ref var selBuildDataCom = ref _cellBuildFilter.Get1(SelectorC.IdxSelCell);

                            if (selBuildDataCom.HaveBuild)
                            {
                                RpcSys.DestroyBuildingToMaster(SelectorC.IdxSelCell);
                            }

                            else
                            {
                                RpcSys.BuildToMaster(SelectorC.IdxSelCell, BuildingTypes.City);
                            }
                        }
                        break;

                    default:
                        throw new Exception();
                }
            }
        }
    }
}

