using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class SyncCellBuildViewSystem : IEcsRunSystem
    {
        private EcsFilter<BuildVC> _cellBuildViewFilt = default;

        public void Run()
        {
            foreach (byte idx in _cellBuildViewFilt)
            {
                ref var curBuildDatCom = ref EntityDataPool.GetBuildCellC<BuildC>(idx);
                ref var curOwnBuildCom = ref EntityDataPool.GetBuildCellC<OwnerC>(idx);
                ref var curVisBuildCom = ref EntityDataPool.GetBuildCellC<VisibleC>(idx);

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
