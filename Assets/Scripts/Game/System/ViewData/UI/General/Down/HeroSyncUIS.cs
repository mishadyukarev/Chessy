using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class HeroSyncUIS : IEcsRunSystem
    {
        public void Run()
        {
            HeroDownUIC.SetActiveScout(InvUnitsC.Have(WhoseMoveC.CurPlayerI, UnitTypes.Elfemale, LevelUnitTypes.First), ScoutHeroCooldownC.Cooldown(WhoseMoveC.CurPlayerI, UnitTypes.Elfemale));
        }
    }
}