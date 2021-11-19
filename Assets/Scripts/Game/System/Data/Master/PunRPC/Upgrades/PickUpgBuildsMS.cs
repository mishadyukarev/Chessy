using Leopotam.Ecs;

namespace Game.Game
{
    public class PickUpgBuildsMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            BuildDoingMC.Get(out var build);

            var whoseMove = WhoseMoveC.WhoseMove;

            BuildsUpgC.AddUpgrade(whoseMove, build);

            PickUpgC.SetHaveUpgrade(whoseMove, false);
            RpcSys.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}