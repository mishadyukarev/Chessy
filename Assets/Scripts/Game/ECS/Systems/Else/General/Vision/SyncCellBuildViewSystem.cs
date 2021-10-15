﻿using Leopotam.Ecs;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class SyncCellBuildViewSystem : IEcsRunSystem
    {
        private EcsFilter<CellBuildDataComponent, OwnerCom> _cellBuildFilter = default;
        private EcsFilter<CellBuildViewComponent> _cellBuildViewFilt = default;

        public void Run()
        {
            foreach (byte idx in _cellBuildFilter)
            {
                ref var curBuildDatCom = ref _cellBuildFilter.Get1(idx);
                ref var curOwnBuildCom = ref _cellBuildFilter.Get2(idx);

                ref var curBuildViewCom = ref _cellBuildViewFilt.Get1(idx);


                if (curBuildDatCom.HaveBuild)
                {
                    curBuildViewCom.SetSpriteFront(curBuildDatCom.BuildType);
                    curBuildViewCom.EnableFrontSR();

                    curBuildViewCom.EnableBackSR();
                    curBuildViewCom.SetSpriteBack(curBuildDatCom.BuildType);


                    if (curOwnBuildCom.IsPlayerType(PlayerTypes.First))
                    {
                        curBuildViewCom.SetBackColor(Color.blue);
                    }

                    else
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