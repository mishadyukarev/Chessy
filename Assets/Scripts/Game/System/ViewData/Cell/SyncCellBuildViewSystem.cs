using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class SyncCellBuildViewSystem : IEcsRunSystem
    {
        private EcsFilter<BuildC, OwnerC, VisibleC> _cellBuildFilter = default;
        private EcsFilter<BuildVC> _cellBuildViewFilt = default;

        public void Run()
        {
            foreach (byte idx in _cellBuildFilter)
            {
                ref var curBuildDatCom = ref _cellBuildFilter.Get1(idx);
                ref var curOwnBuildCom = ref _cellBuildFilter.Get2(idx);
                ref var curVisBuildCom = ref _cellBuildFilter.Get3(idx);

                ref var curBuildViewCom = ref _cellBuildViewFilt.Get1(idx);


                if (curBuildDatCom.Have)
                {
                    if (curVisBuildCom.IsVisibled(WhoseMoveC.CurPlayerI))
                    {
                        curBuildViewCom.SetSpriteFront(curBuildDatCom.Build);
                        curBuildViewCom.EnableFrontSR();

                        curBuildViewCom.EnableBackSR();
                        curBuildViewCom.SetSpriteBack(curBuildDatCom.Build);

                        curBuildViewCom.SetAlpha(curVisBuildCom.IsVisibled(WhoseMoveC.NextPlayerFrom(WhoseMoveC.CurPlayerI)));
                        curBuildViewCom.SetBackColor(curOwnBuildCom.Owner);
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
