using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Systems.SyncCellVision
{
    internal sealed class SyncCellBuildViewSystem : IEcsRunSystem
    {
        private EcsFilter<CellBuildDataComponent, OwnerOnlineComp, OwnerBotComponent, CellBuildViewComponent> _cellBuildFilter;

        public void Run()
        {
            foreach (byte idx in _cellBuildFilter)
            {
                ref var cellBuildDataCom = ref _cellBuildFilter.Get1(idx);
                ref var ownerCellBuildCom = ref _cellBuildFilter.Get2(idx);
                ref var ownerBotCellBuildCom = ref _cellBuildFilter.Get3(idx);
                ref var cellBuildViewCom = ref _cellBuildFilter.Get4(idx);

                if (cellBuildDataCom.HaveBuild)
                {
                    cellBuildViewCom.SetSpriteFront(cellBuildDataCom.BuildType);
                    cellBuildViewCom.EnableFrontSR();

                    cellBuildViewCom.EnableBackSR();
                    cellBuildViewCom.SetSpriteBack(cellBuildDataCom.BuildType);

                    if (ownerCellBuildCom.HaveOwner)
                    {
                        if (ownerCellBuildCom.IsMasterClient)
                        {
                            cellBuildViewCom.SetBackColor(Color.blue);
                        }

                        else
                        {
                            cellBuildViewCom.SetBackColor(Color.red);
                        }
                    }

                    else if (ownerBotCellBuildCom.IsBot)
                    {
                        cellBuildViewCom.SetBackColor(Color.red);
                    }
                }
                else
                {
                    cellBuildViewCom.DisableFrontSR();
                    cellBuildViewCom.DisableBackSR();
                }
            }
        }
    }
}
