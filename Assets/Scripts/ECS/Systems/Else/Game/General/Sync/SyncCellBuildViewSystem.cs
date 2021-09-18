using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Systems.SyncCellVision
{
    internal sealed class SyncCellBuildViewSystem : IEcsRunSystem
    {
        private EcsFilter<CellBuildDataComponent, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellBuildFilter = default;
        private EcsFilter<CellBuildViewComponent> _cellBuildViewFilt = default;

        public void Run()
        {
            foreach (byte idx in _cellBuildFilter)
            {
                ref var curBuildDatCom = ref _cellBuildFilter.Get1(idx);
                ref var curOnBuildCom = ref _cellBuildFilter.Get2(idx);
                ref var curOffBuildCom = ref _cellBuildFilter.Get3(idx);
                ref var curBotBuildCom = ref _cellBuildFilter.Get4(idx);          

                ref var curBuildViewCom = ref _cellBuildViewFilt.Get1(idx);


                if (curBuildDatCom.HaveBuild)
                {
                    curBuildViewCom.SetSpriteFront(curBuildDatCom.BuildType);
                    curBuildViewCom.EnableFrontSR();

                    curBuildViewCom.EnableBackSR();
                    curBuildViewCom.SetSpriteBack(curBuildDatCom.BuildType);

                    if (curOnBuildCom.HaveOwner)
                    {
                        if (curOnBuildCom.IsMasterClient)
                        {
                            curBuildViewCom.SetBackColor(Color.blue);
                        }

                        else
                        {
                            curBuildViewCom.SetBackColor(Color.red);
                        }
                    }

                    else if (curOffBuildCom.HaveLocalPlayer)
                    {
                        if (curOffBuildCom.IsMainMaster)
                        {
                            curBuildViewCom.SetBackColor(Color.blue);
                        }
                        else
                        {
                            curBuildViewCom.SetBackColor(Color.red);
                        }

                    }

                    else if (curBotBuildCom.IsBot)
                    {
                        curBuildViewCom.SetBackColor(Color.red);
                    }
                }
                else
                {
                    curBuildViewCom.DisableFrontSR();
                    curBuildViewCom.DisableBackSR();
                }
            }
        }
    }
}
