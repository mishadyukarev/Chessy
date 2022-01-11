using Photon.Pun;
using static Game.Game.EntityCellPool;
using static Game.Game.EntityCellUnitPool;
using static Game.Game.EntityCellBuildPool;
using static Game.Game.EntityCellEnvPool;

namespace Game.Game
{
    struct DestroyMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            IdxDoingMC.Get(out var idx_0);

            ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);
            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);

            ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
            ref var buildC_0 = ref Build<BuildC>(idx_0);


            if (stepUnit_0.HaveMin)
            {
                EntityPool.Rpc<RpcC>().SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                if (buildC_0.Is(BuildTypes.City))
                {
                    PlayerWinnerC.PlayerWinner = ownUnit_0.Owner;
                }
                stepUnit_0.TakeMin();

                if (buildC_0.Is(BuildTypes.Farm))
                {
                    Environment<EnvCellEC>(EnvTypes.Fertilizer, idx_0).Remove();
                }

                buildCell_0.Remove();
            }
            else
            {
                EntityPool.Rpc<RpcC>().SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}