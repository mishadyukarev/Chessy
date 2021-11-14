using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class ScoutSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            var curPlayer = WhoseMoveC.CurPlayerI;

            GetScoutUIC.SetActiveScout(InvUnitsC.Have(curPlayer, UnitTypes.Scout, LevelUnitTypes.First), ScoutHeroCooldownC.Cooldown(curPlayer, UnitTypes.Scout));
        }
    }
}