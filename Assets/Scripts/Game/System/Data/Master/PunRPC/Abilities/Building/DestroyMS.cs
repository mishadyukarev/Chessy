using Leopotam.Ecs;
using Photon.Pun;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class DestroyMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            IdxDoingMC.Get(out var idx_0);

            ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);
            ref var stepUnit_0 = ref Unit<StepUnitWC>(idx_0);

            ref var buildCell_0 = ref Build<BuildCellC>(idx_0);
            ref var buildC_0 = ref Build<BuildC>(idx_0);
            ref var envCell_0 = ref Environment<EnvCellC>(idx_0);


            if (stepUnit_0.HaveMin)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, ClipTypes.Destroy);

                if (buildC_0.Is(BuildTypes.City))
                {
                    PlyerWinnerC.PlayerWinner = ownUnit_0.Owner;
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
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
        }
    }
}