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

            UnitUpgC.AddUpg(UpgTypes.StartZone, UnitStatTypes.Damage, unit, LevelUnitTypes.First, whoseMove);
            UnitUpgC.AddUpg(UpgTypes.StartZone, UnitStatTypes.Damage, unit, LevelUnitTypes.Second, whoseMove);

            PickUpgC.SetHaveUpgrade(whoseMove, false);
            RpcSys.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}