using Photon.Pun;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class DestroyMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            IdxDoingMC.Get(out var idx_0);

            ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);
            ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);

            ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
            ref var buildC_0 = ref Build<BuildC>(idx_0);
            ref var envCell_0 = ref Environment<EnvCellEC>(idx_0);


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
                    envCell_0.Remove(EnvTypes.Fertilizer);
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