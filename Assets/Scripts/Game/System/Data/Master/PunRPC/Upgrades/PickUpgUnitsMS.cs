using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class PickUpgUnitsMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            UnitDoingMC.Get(out var unit);

            var whoseMove = WhoseMoveC.WhoseMove;

            UnitDamageUpgC.AddUpg(whoseMove, unit, 0.2f);

            PickUpgC.SetHaveUpgrade(whoseMove, false);
            RpcSys.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}