using Leopotam.Ecs;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class SyncCellBuildViewSystem : IEcsRunSystem
    {
        private EcsFilter<CellBuildDataCom, OwnerCom, VisibleCom> _cellBuildFilter = default;
        private EcsFilter<CellBuildViewComponent> _cellBuildViewFilt = default;

        public void Run()
        {
            foreach (byte idx in _cellBuildFilter)
            {
                ref var curBuildDatCom = ref _cellBuildFilter.Get1(idx);
                ref var curOwnBuildCom = ref _cellBuildFilter.Get2(idx);
                ref var curVisBuildCom = ref _cellBuildFilter.Get3(idx);

                ref var curBuildViewCom = ref _cellBuildViewFilt.Get1(idx);


                if (curBuildDatCom.HaveBuild)
                {
                    if (curVisBuildCom.IsVisibled(WhoseMoveCom.CurPlayer))
                    {
                        curBuildViewCom.SetSpriteFront(curBuildDatCom.BuildType);
                        curBuildViewCom.EnableFrontSR();

                        curBuildViewCom.EnableBackSR();
                        curBuildViewCom.SetSpriteBack(curBuildDatCom.BuildType);

                        curBuildViewCom.SetAlpha(curVisBuildCom.IsVisibled(WhoseMoveCom.NextPlayerFrom(WhoseMoveCom.CurPlayer)));
                        curBuildViewCom.SetBackColor(curOwnBuildCom.PlayerType);
                    }
                    else
                    {
                        curBuildViewCom.DisableFrontSR();
                        curBuildViewCom.DisableBackSR();
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
